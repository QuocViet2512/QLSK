using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QLEventKhoa.Models;
using System.Web.Security;
namespace QLEventKhoa.ViewModels
{
    public class RoleProvider : System.Web.Security.RoleProvider
    {
        public RoleProvider()
        {

        }
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
            
        }

        public override string[] GetRolesForUser(string username)
        {
            // throw new NotImplementedException();       
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return null;
            }

            var userRoles = new string[] { };

            using (dbEvent dbContext = new dbEvent())
            {
                var selectedUser = (from us in dbContext.tbAdmins
                                    where string.Compare(us.userAdmin, username, StringComparison.OrdinalIgnoreCase) == 0
                                    select us).FirstOrDefault();
                if (selectedUser != null)
                {
                    userRoles = new[] { selectedUser.tbLoaiAdmin.tenLoaiAD.ToString() };
                }

                return userRoles.ToArray();
            }
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            // throw new NotImplementedException();
            var userRole = GetRolesForUser(username);
            return userRole.Contains(roleName);
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}