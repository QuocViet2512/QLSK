using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLEventKhoa.Models;
using System.Web.Routing;
using System.Security.Principal;
using QLEventKhoa.ViewModels;
using System.Security.Claims;

namespace System.Web.Mvc
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeRole : AuthorizeAttribute
    {
     

        public AuthorizeRole(params string[] roles) : base()
        {
            this.Roles = string.Join(",", roles);
         
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {

            return base.AuthorizeCore(httpContext);
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {         
            base.OnAuthorization(filterContext);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary
                {
                    {"Controller","HomeAD" },
                    {"action","Unauthorized" }
                });
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }


        //protected override bool AuthorizeCore(HttpContextBase httpContext)
        //{

        //    bool isauthorize = false;         
        //    //c1
        //    //var userID = Convert.ToString(httpContext.Session["UserID"]);
        //    //if (!string.IsNullOrEmpty(userID))
        //    //{
        //    //    var userRole = (from u in db.tbAdmins
        //    //                    join r in db.tbLoaiAdmins
        //    //                    on u.idloaiql equals r.idLoaiAD
        //    //                    where u.idAdmin.ToString() == userID
        //    //                    select new
        //    //                    {
        //    //                        r.tenLoaiAD
        //    //                    }).FirstOrDefault();
        //    //    foreach (var role in allowedroles)
        //    //    {
        //    //        if (role == userRole.tenLoaiAD) return true;
        //    //    }
        //    //}
        //    //c2
        //    foreach (var role in allowedroles)
        //    {
        //        var user = db.tbAdmins.Where(a => a.userAdmin == HttpContext.Current.User.Identity.Name && a.tbLoaiAdmin.tenLoaiAD == role
        //        && a.statusAd == true);
        //        if (user.Count() > 0)
        //        {
        //            isauthorize = true;
        //        }
        //    }
        //    return isauthorize;
        //}
    }
}