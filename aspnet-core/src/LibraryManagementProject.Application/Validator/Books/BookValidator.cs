using FluentValidation;
using LibraryManagementProject.Entity.Books;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagementProject.Validator.Books
{
    public class BookValidator : AbstractValidator<Book>
    {
        public BookValidator()
        {
            RuleFor(value => value.Id).NotNull();
            RuleFor(value => value.Name).NotNull().Length(5, 150);
            RuleFor(value => value.Stock).NotNull().LessThan(1500);
            RuleFor(value => value.PriceBorrow).NotNull();
            RuleFor(value => value.CategoryId).NotNull();
            RuleFor(value => value.AuthorId).NotNull();
            RuleFor(value => value.PublisherId).NotNull();
            RuleFor(value => value.Description).NotNull();
            RuleFor(value => value.UrlImage).NotNull();
            RuleFor(value => value.Year).NotNull().Length(4);
        }
    }
}
