using Polly;
using Polly.Retry;
using Polly.Extensions.Http;

namespace NSE.WebApp.MVC.Configuration
{
    public class PollyConfig
    {
        public static AsyncRetryPolicy<HttpResponseMessage> BuildPolicy() 
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(sleepDurations: new[]
                {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromSeconds(10),
                },
                onRetry: (outcome, timeSpan, retryCount, context) => { 
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"Tentativa: {retryCount}");
                    Console.ForegroundColor = ConsoleColor.White;
                });
        }
    }
}
