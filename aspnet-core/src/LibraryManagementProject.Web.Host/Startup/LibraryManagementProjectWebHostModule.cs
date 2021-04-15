using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using LibraryManagementProject.Configuration;

namespace LibraryManagementProject.Web.Host.Startup
{
    [DependsOn(
       typeof(LibraryManagementProjectWebCoreModule))]
    public class LibraryManagementProjectWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public LibraryManagementProjectWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(LibraryManagementProjectWebHostModule).GetAssembly());
        }
    }
}
