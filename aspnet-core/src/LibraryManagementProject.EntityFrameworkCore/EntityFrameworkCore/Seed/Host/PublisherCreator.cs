using LibraryManagementProject.Entity.Publishers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryManagementProject.EntityFrameworkCore.Seed.Host
{
    public class PublisherCreator
    {
        private readonly LibraryManagementProjectDbContext _context;

        public PublisherCreator(LibraryManagementProjectDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreatePublisher();
        }
        private void CreatePublisher()
        {
            IList<Publisher> defaultPublishers = new List<Publisher>();

            defaultPublishers.Add(new Publisher() { Name = "Nhà xuất bản Kim Đồng", Email = "kimdong@hn.vnn.vn",Phone="0123456789", Address = "55 Quang Trung, Hai Bà Trưng, Hà Nội" });
            defaultPublishers.Add(new Publisher() { Name = "Nhà xuất bản giáo dục", Email = "ncbgd@hn.vn", Phone = "0123456789", Address = "81 Trần Hưng Đạo, Hà Nội" });
            defaultPublishers.Add(new Publisher() { Name = "Nhà xuất bản lao động", Email = "nxblaodong@yahoo.com", Phone = "0123456789", Address = "175 Giảng Võ, Đống Đa, Hà Nội" });
            defaultPublishers.Add(new Publisher() { Name = "Nhà xuất bản Đại học Quốc Gia Hà Nội", Email = "nxbdhqg@hn.vnn.vn", Phone = "0123456789", Address = "16 Hàng Chuối, Phạm Đình Hổ, Hai Bà Trưng, Hà Nội" });
            defaultPublishers.Add(new Publisher() { Name = "Nhà xuất bản giao thông vận tải", Email = "nxbgtvt@fpt.vn", Phone = "0123456789", Address = " 80B Trần Hưng Đạo, Hoàn Kiếm, Hà Nội" });

            foreach (var defaultPublisher in defaultPublishers)
            {
                var publisher = _context.Publishers
                        .Where(s => s.Name == defaultPublisher.Name)
                        .FirstOrDefault<Publisher>();

                if (publisher == null)
                {
                    _context.Publishers.AddRange(defaultPublisher);
                    _context.SaveChanges();
                }
            }

        }
    }
}
