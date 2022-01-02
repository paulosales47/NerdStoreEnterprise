﻿using Microsoft.Extensions.DependencyInjection;

namespace NSE.MessageBus
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddMessageBus(this IServiceCollection services, string connection) 
        {
            if(string.IsNullOrWhiteSpace(connection))
                throw new ArgumentNullException(nameof(connection)); 

            services.AddSingleton<IMessageBus>(new MessageBus(connection));   

            return services;
        }
    }
}
