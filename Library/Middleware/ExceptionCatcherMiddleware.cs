using Library.Exceptions;

namespace Library.Middleware;

public class ExceptionCatcherMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionCatcherMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (BusinessLogicException ex)
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsJsonAsync(new {error = ex.Message});
        }
        catch (Exception)
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsJsonAsync(new {error = "Неизвестная ошибка"});
        }
    }
}