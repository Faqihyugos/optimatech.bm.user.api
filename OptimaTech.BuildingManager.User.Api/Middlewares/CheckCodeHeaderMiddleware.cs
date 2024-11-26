namespace OptimaTech.BuildingManager.User.Api.Middlewares;

public class CheckCodeHeaderMiddleware
{
    private readonly RequestDelegate _next;

    public CheckCodeHeaderMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Check if the header exists
        if (context.Request.Headers.TryGetValue("Tenant-Code", out var tenantHeaderValue))
        {
            // You can also check the value of the header here if needed
            if (Guid.TryParse(tenantHeaderValue, out var tenantId))
            {
                if (context.Request.Headers.TryGetValue("User-Code", out var userHeaderValue))
                {
                    // You can also check the value of the header here if needed
                    if (Guid.TryParse(userHeaderValue, out var userId))
                    {
                        // Proceed to the next middleware or endpoint
                        await _next(context);
                    }
                    else
                    {
                        // Return a 400 Bad Request if the header value is incorrect
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                        await context.Response.WriteAsync("Invalid User-Code value.");
                    }
                }
                else
                {
                    // Return a 400 Bad Request if the header is missing
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync("Missing User-Code Header");
                }
            }
            else
            {
                // Return a 400 Bad Request if the header value is incorrect
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Invalid Tenant-Code value.");
            }
        }
        else
        {
            // Return a 400 Bad Request if the header is missing
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync("Missing Tenant-Code Header");
        }
    }
}