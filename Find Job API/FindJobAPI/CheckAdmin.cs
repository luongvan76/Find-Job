using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FindJobAPI
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method)]
    public class CheckAdmin : Attribute, IAuthorizationFilter
    {
        private readonly string _claimsAdmin;
        private readonly string _claimsValue;

        public CheckAdmin(string claimsAdmin, string claimsValue)
        {
            this._claimsAdmin = claimsAdmin;
            this._claimsValue = claimsValue;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if(!context.HttpContext.User.HasClaim(_claimsAdmin, _claimsValue))
            {
                context.Result = new UnauthorizedObjectResult(new {Message = "Không có quyền sử dụng"});
            }
        }
    }
}
