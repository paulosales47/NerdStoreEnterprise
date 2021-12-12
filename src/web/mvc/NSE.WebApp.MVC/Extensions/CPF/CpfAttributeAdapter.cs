using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Localization;

namespace NSE.WebApp.MVC.Extensions.CPF
{
    public class CpfAttributeAdapter: AttributeAdapterBase<CpfAttribute>
    {
        public CpfAttributeAdapter(CpfAttribute attribute, IStringLocalizer stringLocalizer): base(attribute, stringLocalizer)  
        {}

        public override void AddValidation(ClientModelValidationContext context)
        {
            if(context == null)
                throw new ArgumentNullException(nameof(context));

            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-cpf", GetErrorMessage(context));
        }

        public override string GetErrorMessage(ModelValidationContextBase validationContext)
        {
            return "CPF em formato inválido";
        }

        protected override string GetErrorMessage(ModelMetadata modelMetadata, params object[] arguments)
        {
            return "CPF em formato inválido";
        }
    }
}
