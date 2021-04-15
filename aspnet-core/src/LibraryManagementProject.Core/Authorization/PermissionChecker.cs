using Abp.Authorization;
using LibraryManagementProject.Authorization.Roles;
using LibraryManagementProject.Authorization.Users;

namespace LibraryManagementProject.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
