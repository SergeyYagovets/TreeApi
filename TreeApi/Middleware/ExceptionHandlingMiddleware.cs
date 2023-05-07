using Newtonsoft.Json;
using TreeApi.DAL;
using TreeApi.Services.Implementation;

namespace TreeApi.Middleware
{
	public class ExceptionHandlingMiddleware
	{
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ILogger<ExceptionHandlingMiddleware> logger, TreeDbContext treeDbContext)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                string eventId = Guid.NewGuid().ToString("N");

                logger.LogError(ex, $"Exception caught with ID {eventId}");

                // Log SecureException to database
                var exceptionLog = new ExceptionLog
                {
                    EventId = eventId,
                    Timestamp = DateTime.UtcNow,
                    QueryParameters = context.Request.Query.ToString(),
                    BodyParameters = await GetRequestBody(context.Request),
                    StackTrace = ex.StackTrace
                };
               
                treeDbContext.ExceptionLogs.Add(exceptionLog);
                await treeDbContext.SaveChangesAsync();

                object response;
                if (ex is SecureException secureEx)
                {
                    // Return HTTP 500 response with SecureException details
                    response = new
                    {
                        type = "Secure",
                        id = eventId,
                        data = new { message = secureEx.Message }
                    };
                }
                else
                {
                    // Return HTTP 500 response with Exception details
                    response = new
                    {
                        type = "Exception",
                        id = eventId,
                        data = new { message = $"Internal server error ID = {eventId}" }
                    };
                }

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
            }
        }

        private async Task<string> GetRequestBody(HttpRequest request)
        {
            request.EnableBuffering();
            var bodyStream = request.Body;
            var reader = new StreamReader(bodyStream);
            var body = await reader.ReadToEndAsync();
            bodyStream.Seek(0, SeekOrigin.Begin);
            request.Body = bodyStream;
            return body;
        }
    }
}

