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
            RuleFor(value => value.BookId).NotNull();
            RuleFor(value => value.DateBorrow).NotNull();
            RuleFor(value => value.Qty).NotNull();
            RuleFor(value => value.DateRepay).NotNull();
            RuleFor(value => value.UserId).NotEmpty();
        }
    }
}
