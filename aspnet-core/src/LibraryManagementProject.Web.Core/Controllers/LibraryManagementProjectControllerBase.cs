using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace LibraryManagementProject.Controllers
{
    public abstract class LibraryManagementProjectControllerBase: AbpController
    {
        protected LibraryManagementProjectControllerBase()
        {
            LocalizationSourceName = LibraryManagementProjectConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
