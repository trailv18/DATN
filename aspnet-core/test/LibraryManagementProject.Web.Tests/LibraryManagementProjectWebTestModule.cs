using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using LibraryManagementProject.EntityFrameworkCore;
using LibraryManagementProject.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace LibraryManagementProject.Web.Tests
{
    [DependsOn(
        typeof(LibraryManagementProjectWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class LibraryManagementProjectWebTestModule : AbpModule
    {
        public LibraryManagementProjectWebTestModule(LibraryManagementProjectEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(LibraryManagementProjectWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(LibraryManagementProjectWebMvcModule).Assembly);
        }
    }
}