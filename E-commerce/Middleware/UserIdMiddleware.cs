using System.Security.Claims;

namespace E_commerce.Middleware
{
    public class UserIdMiddleware
    {
        
        private readonly RequestDelegate _next;
        public UserIdMiddleware( RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            if (context.User.Identity?.IsAuthenticated == true)
            {
                var idclaim = context.User.FindFirst(ClaimTypes.NameIdentifier);

                if (idclaim != null)
                {
                    
                    if (int.TryParse(idclaim.Value, out var userId))
                    {
                        context.Items["UserId"] = userId;
                        
                    }
                }
               
            }
           
            await _next(context);
        }
    }
}
