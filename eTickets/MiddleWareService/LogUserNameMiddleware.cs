
using eTickets.Extension;
using Serilog.Context;

public class LogUserNameMiddleware
{
    private readonly RequestDelegate next;

    public LogUserNameMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var result = context.User.GetUserName() ?? "System";

        LogContext.PushProperty("Username", result);

        await next(context);
        Console.WriteLine("Test Middleware");
    }

}
//using Serilog.Context;

//namespace eTickets.MiddleWareService
//{
//    public class CustomMiddleware
//    {
//        private readonly RequestDelegate next;

//        public CustomMiddleware(RequestDelegate next)
//        {
//            this.next = next;
//        }

//        public async Task Invoke(HttpContext context)
//        {

//            var name = context.User.Identity?.Name ?? "system";



//            LogContext.PushProperty("userName", name);

//            await next(context);
//        }

//    }
//}

//    //public class ClassWithNoImplementationMiddleware
//    //{
//    //    private readonly RequestDelegate _next;
//    //    public ClassWithNoImplementationMiddleware(RequestDelegate next)
//    //    {
//    //        _next = next;
//    //    }
//    //    public async Task InvokeAsync(HttpContext httpContext)
//    //    {
//    //        await httpContext.Response.WriteAsync("Hello Readers!, this from Customer Middleware...");
//    //    }
//    //}
//    //// Extension method used to add the middleware to the HTTP request pipeline.
//    //public static class ClassWithNoImplementationMiddlewareExtensions
//    //{
//    //    public static IApplicationBuilder UseClassWithNoImplementationMiddleware(this IApplicationBuilder builder)
//    //    {
//    //        return builder.UseMiddleware<ClassWithNoImplementationMiddleware>();
//    //    }
//    //}

