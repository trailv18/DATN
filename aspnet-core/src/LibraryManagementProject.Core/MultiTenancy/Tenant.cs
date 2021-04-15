using Abp.MultiTenancy;
using LibraryManagementProject.Authorization.Users;

namespace LibraryManagementProject.MultiTenancy
{
    public class Tenant : AbpTenant<User>
    {
        public Tenant()
        {            
        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }
    }
}
