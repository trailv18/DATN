using FluentValidation;
using LibraryManagementProject.Entity.Categories;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagementProject.Validator.Categories
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(c => c.Id).NotNull();
            RuleFor(c => c.Name).NotNull().Length(5, 100);
        }
    }
}
