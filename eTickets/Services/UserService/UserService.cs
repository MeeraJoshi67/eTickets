using System.Security.Claims;

namespace eTickets.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string GetMyName()
        {
            //throw new NotImplementedException();
            
           
             var result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            if (result == null)
            {
                
                throw new Exception("not authorized");
            }
            return result;


        }
    }
}
