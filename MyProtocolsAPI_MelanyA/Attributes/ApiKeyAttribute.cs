using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MyProtocolsAPI_MelanyA.Attributes
{

    // Esta clase ayuda a limitar la forma en que se puede consumir un recurso de controlador 
    // (end point). Basicamente vamos a crear una decoraci'on personalizada que inyecta cierta 
    // funcinalidad ya sea a todo un controller o a un end point particular.

    [AttributeUsage(validOn:AttributeTargets.All)]
    public sealed class ApiKeyAttribute : Attribute, IAsyncActionFilter
    {
        // Especificamos cual es el clave: Valor dentro de Appsettings que queremos usar como ApiKey
        private readonly string _apiKey = "Progra6ApiKey";

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next) 
        {
            // Aca validamos que en el body(en tipo Json) del request vaya la info del ApiKey
            // si no va la info presentamos un mensaje de error indicando que falta ApiKey y que no
            // se puede consumir el recurso.

            if (!context.HttpContext.Request.Headers.TryGetValue(_apiKey, out var ApiSalida))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "LLamada no contiene información de seguridad ... "
                };
                return;
                // Si no hay info de seguridad, sale la funci'on y muestra un mensaje de error

            }

            // Si viene info de seguridad, falta validar que sea la correcta, para esto lo primero es extraer
            // el valor de Progra6ApiKey dentro de appsettings .json para poder comparar contra lo que viene
            // en el request 

            var appSettings = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();

            var ApiKeyValue = appSettings.GetValue<string>(_apiKey);

            //queda comparar que las apikey sean iguales

            if (!ApiKeyValue.Equals(ApiSalida))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401, Content = "ApiKey Invalida"
                };
                return;
            }

            await next();
        }

    }
}
