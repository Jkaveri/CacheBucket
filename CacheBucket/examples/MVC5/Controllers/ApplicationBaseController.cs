using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    public abstract class ApplicationBaseController : Controller
    {
        protected int UserId { get; set; }
        protected ApplicationBaseController()
        {
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var userIdStr = filterContext.RequestContext.HttpContext.Request.QueryString.Get("user_id");
            if (int.TryParse(userIdStr, out var userId))
            {
                this.UserId = userId;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}