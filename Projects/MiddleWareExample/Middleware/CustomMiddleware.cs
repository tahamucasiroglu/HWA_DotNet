namespace MiddleWareExample.Middleware
{
    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string message = "Veriler\n";
            message += $"Date = {DateTime.Now}" + "\n";
            message += $" Host = {context.Request.Host.Host} \n";
            message += $" Port = {context.Request.Host.Port} \n";
            message += $" Response Code = {context.Response.StatusCode} \n";
            message += $" Port = {context.Connection.LocalPort} \n";
            message += $" User Identity = {context.User.Identity} \n";
            message += $" User Identity = {context.User.Identities.Select(u => u.Name + " - " + u.Label + " - " + u.NameClaimType.ToString())} \n";
            message += $"\n";
            Console.WriteLine(message);
            Console.WriteLine($"İstek Geldi. İstek Adresi: {context.Request.Path}");

            await _next(context);

            Console.WriteLine($"İstek Soncu Dönen kod: {context.Response.StatusCode}");
        }
    }
}
