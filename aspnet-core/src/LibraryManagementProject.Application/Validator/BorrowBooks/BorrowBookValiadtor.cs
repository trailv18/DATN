using FluentValidation;
using LibraryManagementProject.Entity.BorrowBooks;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagementProject.Validator.BorrowBooks
{
    public class BorrowBookValiadtor : AbstractValidator<BorrowBook>
    {
        public BorrowBookValiadtor()
        {
            RuleFor(value => value.Id).NotNull();
            RuleFor(value => value.DateBorrow).NotNull();
            RuleFor(value => value.DateRepay).NotNull();
            RuleFor(value => value.Status).NotNull();
            RuleFor(value => value.UserId).NotNull();
            RuleFor(value => value.Total).NotNull();
        }
    }
}
