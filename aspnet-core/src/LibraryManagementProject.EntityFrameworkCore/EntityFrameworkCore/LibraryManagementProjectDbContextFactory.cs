using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using LibraryManagementProject.Configuration;
using LibraryManagementProject.Web;

namespace LibraryManagementProject.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class LibraryManagementProjectDbContextFactory : IDesignTimeDbContextFactory<LibraryManagementProjectDbContext>
    {
        public LibraryManagementProjectDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<LibraryManagementProjectDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            LibraryManagementProjectDbContextConfigurer.Configure(builder, configuration.GetConnectionString(LibraryManagementProjectConsts.ConnectionStringName));

            return new LibraryManagementProjectDbContext(builder.Options);
        }
    }
}
