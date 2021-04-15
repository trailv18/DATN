using LibraryManagementProject.Entity.Authors;
using LibraryManagementProject.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryManagementProject.EntityFrameworkCore.Seed.Host
{
    public class AuthorCreator
    {
        private readonly LibraryManagementProjectDbContext _context;

        public AuthorCreator(LibraryManagementProjectDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateAuthor();
        }
        private void CreateAuthor()
        {
            IList<Author> defaultAuthors = new List<Author>();

            defaultAuthors.Add(new Author() { Name = "Nguyễn Nhật Ánh", Phone="0123456789", Address="11 Hải Châu, Đà Nẵng", YearOfBirth= "1955" });
            defaultAuthors.Add(new Author() { Name = "Trang Hạ", Phone = "0123456789", Address = "Tam Kỳ, Quảng Nam", YearOfBirth = "1975" });
            defaultAuthors.Add(new Author() { Name = "Nguyễn Phong Việt", Phone = "0123456789", Address = "Quận 1, Sài Gòn", YearOfBirth = "1980" });
            defaultAuthors.Add(new Author() { Name = "Anh Khang", Phone = "0123456789", Address = "Hoàn Kiếm, Hà Nội", YearOfBirth = "1987" });
            defaultAuthors.Add(new Author() { Name = "Hamlet Trương", Phone = "0123456789", Address = "Cao Bằng", YearOfBirth = "1988" });

            foreach (var defaultAuthor in defaultAuthors)
            {
                var author = _context.Authors
                        .Where(s => s.Name == defaultAuthor.Name)
                        .FirstOrDefault<Author>();

                if (author == null)
                {
                    _context.Authors.AddRange(defaultAuthor);
                    _context.SaveChanges();
                }
            }

        }
    }
}
