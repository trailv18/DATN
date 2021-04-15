using Abp.Domain.Repositories;
using LibraryManagementProject.Entity.Categories;
using LibraryManagementProject.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManagementProject.EntityFrameworkCore.Seed.Host
{
    public class CaregoryCreator
    {
        private readonly LibraryManagementProjectDbContext _context;

        public CaregoryCreator(LibraryManagementProjectDbContext context){
            _context = context;
        }

        public void Create()
        {
            CreateCategory();
        }
        private void CreateCategory()
        {
            IList<Category> defaultCategories = new List<Category>();

            defaultCategories.Add(new Category() { Name = "Sách giáo khoa" });
            defaultCategories.Add(new Category() { Name = "Tiểu thuyết" });
            defaultCategories.Add(new Category() { Name = "Truyện tranh" });
            defaultCategories.Add(new Category() { Name = "Thơ Thơ" });
            defaultCategories.Add(new Category() { Name = "Từ điển" });

            foreach (var defaultCategory in defaultCategories)
            {
                var category = _context.Categories
                        .Where(s => s.Name == defaultCategory.Name)
                        .FirstOrDefault<Category>();

                if (category == null)
                {
                    _context.Categories.AddRange(defaultCategory);
                    _context.SaveChanges();
                }
            }
            
        }
    }
}
