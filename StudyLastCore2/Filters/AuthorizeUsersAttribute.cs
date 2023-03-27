using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace StudyLastCore.Filters {
    public class AuthorizeUsersAttribute : AuthorizeAttribute, IAuthorizationFilter {

        public void OnAuthorization(AuthorizationFilterContext context) {
            var user = context.HttpContext.User;
            if (user.Identity.IsAuthenticated == false) {
                context.Result = this.GetRoute("Managed", "LogIn");
            } else {
                if (user.IsInRole("admin") == false && user.IsInRole("cliente") == false) {
                    context.Result = this.GetRoute("Managed", "AccesoDenegado");
                }
            }
        }

        private RedirectToRouteResult GetRoute(string controller, string action) {
            RouteValueDictionary ruta = new RouteValueDictionary(
                new {
                    controller = controller,
                    action = action
                }
            );
            RedirectToRouteResult result = new RedirectToRouteResult(ruta);
            return result;
        }
    }
}
