using FluentValidation;
using LibraryManagementProject.Entity.Authors;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagementProject.Validator.Authors
{
    public class AuthorValidator : AbstractValidator<Author>
    {
        public AuthorValidator()
        {
            RuleFor(value => value.Id).NotNull();
            RuleFor(value => value.Name).NotNull().Length(5, 100);
            RuleFor(value => value.Address).NotNull().Length(10,150);
            RuleFor(value => value.YearOfBirth).NotNull().Length(4);
            RuleFor(value => value.Phone).NotNull().Length(10);
        }
    }
}
