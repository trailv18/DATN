using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementProject.EntityFrameworkCore
{
    public static class LibraryManagementProjectDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<LibraryManagementProjectDbContext> builder, string connectionString)
        {
            builder.UseMySql(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<LibraryManagementProjectDbContext> builder, DbConnection connection)
        {
            builder.UseMySql(connection);
        }
    }
}
