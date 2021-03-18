using Microsoft.AspNetCore.Http;

namespace DesafioWarren.Application.Services.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IdentityService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetRequestPath() => _httpContextAccessor.HttpContext.Request.Path;
    }
}