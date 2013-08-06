using HaberSitesi.Service;
using System.Web.Security;

namespace HaberSitesi.Web.Uygulama.Uyelik
{
    public class RolSaglayici : RoleProvider
    {
        private RolServis rolServis;

        public RolSaglayici()
        {
            this.rolServis = new RolServis();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            return rolServis.KullaniciRoldeMi(username, roleName);
        }

        public override string[] GetRolesForUser(string username)
        {
            return rolServis.KullaniciRolleri(username);
        }

        #region not implemented
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new System.NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new System.NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new System.NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new System.NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new System.NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new System.NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new System.NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}