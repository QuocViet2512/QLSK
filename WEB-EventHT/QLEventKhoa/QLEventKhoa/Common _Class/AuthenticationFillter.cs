using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLEventKhoa.Models;
using System.Web.Routing;
using System.Web.Mvc.Filters;

namespace QLEventKhoa.ViewModels
{
    public class AuthenticationFillter : ActionFilterAttribute, IAuthenticationFilter
    {
       
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            if (string.IsNullOrEmpty(Convert.ToString(filterContext.HttpContext.Session["UserID"])))
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            //throw new NotImplementedException();
            if(filterContext.Result==null || filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary
                {
                     { "Controller", "AccountAD" },
                     { "Action", "LoginAD" }
                });
            }
        }
    }
}