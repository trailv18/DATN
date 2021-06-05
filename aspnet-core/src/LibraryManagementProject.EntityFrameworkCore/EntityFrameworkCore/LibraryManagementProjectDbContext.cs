using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using LibraryManagementProject.Authorization.Roles;
using LibraryManagementProject.Authorization.Users;
using LibraryManagementProject.MultiTenancy;
using LibraryManagementProject.Entity.Categories;
using LibraryManagementProject.Entity.Authors;
using LibraryManagementProject.Entity.Publishers;
using LibraryManagementProject.Entity.Books;
using LibraryManagementProject.Entity.BorrowBookDetails;

namespace LibraryManagementProject.EntityFrameworkCore
{
    public class LibraryManagementProjectDbContext : AbpZeroDbContext<Tenant, Role, User, LibraryManagementProjectDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<Category> Categories { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BorrowBookDetail> BorrowBookDetails { get; set; }
        public LibraryManagementProjectDbContext(DbContextOptions<LibraryManagementProjectDbContext> options)
            : base(options)
        {
        }
    }
}
