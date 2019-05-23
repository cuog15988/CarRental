using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRenTal.DAO.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace CarRenTal.Controllers
{
    public class BaseUserController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (CommonConstants.UserName == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new { Controller = "UserLogin", Action = "questionLogin" }));
            }
            base.OnActionExecuting(filterContext);
        }
    }
}