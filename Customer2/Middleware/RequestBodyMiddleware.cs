namespace Customer2.Middleware;


using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;
using System.Threading.Tasks;

 
    public class RequestBodyModificationMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestBodyModificationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {

        // Read the request body
        var originalRequestBody = context.Request.Body;
        using (var memoryStream = new MemoryStream())
        {
            await originalRequestBody.CopyToAsync(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);

            // Replace the request body with the modified one
            context.Request.Body = ModifyBody(memoryStream);

            // Call the next middleware in the pipeline
          //  await _next(context);

            // Restore the original request body
            //    context.Request.Body = originalRequestBody;
        }




        var originalResponseBodyStream = context.Response.Body;

        using (var responseBody = new MemoryStream())
        {
            context.Response.Body = responseBody;

            await _next(context);

            responseBody.Seek(0, SeekOrigin.Begin);

            // Read the modified response body
            var modifiedResponseBody = new StreamReader(responseBody).ReadToEnd();

            // Modify the response body here as needed
            var modifiedContent = "Modified content: " + modifiedResponseBody;

            // Write the modified content to the original response body
            var buffer = Encoding.UTF8.GetBytes(modifiedContent);
            await originalResponseBodyStream.WriteAsync(buffer, 0, buffer.Length);
        }

    }

        private static Stream ModifyBody(Stream originalBody)
        {
            // Example modification: convert the request body to uppercase
            var requestBodyString = new StreamReader(originalBody).ReadToEnd();
            var modifiedBodyString = "\"tttttt\""; //requestBodyString.ToUpper() ;
            var modifiedBodyBytes = Encoding.UTF8.GetBytes(modifiedBodyString);
            return new MemoryStream(modifiedBodyBytes);
        }
    }

    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestBodyModificationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestBodyModificationMiddleware>();
        }
    }
 
