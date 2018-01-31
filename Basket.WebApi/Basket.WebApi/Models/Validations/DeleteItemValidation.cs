using Basket.WebApi.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.WebApi.Models.Validations
{
    public class DeleteItemValidation : AbstractValidator<DeleteItemRequest>
    {
        public DeleteItemValidation()
        {
            RuleFor(b => b.ClientId).NotNull().WithErrorCode(((int)BasketError.ClientIdCantBeNull).ToString());
            RuleFor(b => b.SKU).NotNull().WithErrorCode(((int)BasketError.SKUCantBeNull).ToString());
        }
    }
}
