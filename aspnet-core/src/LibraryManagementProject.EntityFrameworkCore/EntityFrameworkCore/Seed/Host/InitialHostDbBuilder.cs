namespace LibraryManagementProject.EntityFrameworkCore.Seed.Host
{
    public class InitialHostDbBuilder
    {
        private readonly LibraryManagementProjectDbContext _context;

        public InitialHostDbBuilder(LibraryManagementProjectDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new DefaultEditionCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();
            new CaregoryCreator(_context).Create();
            new PublisherCreator(_context).Create();
            new AuthorCreator(_context).Create();

            _context.SaveChanges();
        }
    }
}
