using FluentValidation;
using LibraryManagementProject.Entity.BorrowBookDetails;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagementProject.Validator.BorrowBookDetails
{
    public class BorrowBookDetailValiadtor : AbstractValidator<BorrowBookDetail>
    {
        public BorrowBookDetailValiadtor()
        {
            RuleFor(value => value.Id).NotNull();
            RuleFor(value => value.BorrowBookId).NotEmpty();
            RuleFor(value => value.BookId).NotNull().NotEmpty();
            RuleFor(value => value.Qty).NotNull().LessThan(5);
            RuleFor(value => value.PriceBorrow).NotEmpty();
            RuleFor(value => value.Total).NotNull();
        }
    }
}
