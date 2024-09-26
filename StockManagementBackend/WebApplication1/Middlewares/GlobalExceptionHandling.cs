using Service.dtos;

namespace WebApplication1.Middlewares
{
    
      public class GlobalExceptionHandling : IMiddleware
      {
            private readonly ILogger<GlobalExceptionHandling> _logger;
            public GlobalExceptionHandling(ILogger<GlobalExceptionHandling> logger)
            {
                _logger = logger;
            }
            public async Task InvokeAsync(HttpContext context, RequestDelegate next)
            {
                try
                {
                    await next(context);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);

                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsJsonAsync(new ServiceResponse<object>
                    {
                        Success = false,
                        Message = "Internal Server Error"
                    });
                }
            }

      }
    
}
