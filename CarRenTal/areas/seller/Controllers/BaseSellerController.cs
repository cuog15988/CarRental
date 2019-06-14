using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace CarRenTal.Areas.seller.Controllers
{
    public class BaseSellerController : Controller
    {
       
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                if (Models.UserDao.name == null)
                {
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(new { Controller = "SellerLogin", Action = "index" }));
                }
                base.OnActionExecuting(filterContext);
            }
    }
}