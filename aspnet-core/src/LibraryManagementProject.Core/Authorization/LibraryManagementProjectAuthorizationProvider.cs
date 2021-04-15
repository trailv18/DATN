using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace LibraryManagementProject.Authorization
{
    public class LibraryManagementProjectAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_Librarians, L("Librarians"));
            context.CreatePermission(PermissionNames.Pages_Readers, L("Readers"));
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, LibraryManagementProjectConsts.LocalizationSourceName);
        }
    }
}
