using LayeredArchitecture.Common.ApiResponse;
using static LayeredArchitecture.Common.ApiResponse.DefineResponse;

namespace LayeredArchitecture.API.Utils
{
    public class HandleErrorMiddleware
    {
        private readonly RequestDelegate _next;
        private ILogger<HandleErrorMiddleware> _logger;

        public HandleErrorMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<HandleErrorMiddleware>();

            HttpResponse httpResponse = context.Response;
            var originalResponseBody = httpResponse.Body;
            using var newResponseBody = new MemoryStream();
            httpResponse.Body = newResponseBody;

            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                if (!string.IsNullOrEmpty(error.Source) && error.Source.Contains("AWSSDK.Core"))
                {
                    _logger.LogError(error, error.Message);
                    httpResponse.StatusCode = StatusCodes.Status500InternalServerError;
                    var errorResponse = ApiResponse.GetResponseItem(EnumCodes.R_CMN_500_07);
                    await httpResponse.WriteAsJsonAsync(ApiResponse.Response(errorResponse));
                }
                else
                {
                    _logger.LogError(error, error.Message);
                    httpResponse.StatusCode = StatusCodes.Status500InternalServerError;
                    var errorResponse = ApiResponse.GetResponseItem(EnumCodes.R_CMN_500_01);
                    await httpResponse.WriteAsJsonAsync(ApiResponse.Response(errorResponse));
                }

            }

            newResponseBody.Seek(0, SeekOrigin.Begin);
            var responseBodyText = await new StreamReader(httpResponse.Body).ReadToEndAsync();

            newResponseBody.Seek(0, SeekOrigin.Begin);
            await newResponseBody.CopyToAsync(originalResponseBody);
        }
    }
}
