using Newtonsoft.Json;
using System.Text;

namespace ApiGatewayNbg.Services;

public class TransformMiddleware
{
    private readonly RequestDelegate _next;

    public TransformMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {

        context.Request.Headers.Add("MyCustomReqHeader", "123");
         string requestBody;
            using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8))
             {
                requestBody = await reader.ReadToEndAsync();
            }
        dynamic requestData = JsonConvert.DeserializeObject(requestBody);


        context.Response.Headers.Add("MyCustomRspHeader", "456");

        //
        await _next(context);
    }
}
