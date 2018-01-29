using Basket.WebApi.Enums;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.WebApi.Models.Validations
{
    public class BasketModelValidation : AbstractValidator<BasketModel>
    {
        public BasketModelValidation()
        {
            RuleFor(b => b.ClientId).NotNull().WithErrorCode(((int)BasketError.ClientIdCantBeNull).ToString());
            RuleFor(b => b.SKU).NotNull().WithErrorCode(((int)BasketError.SKUCantBeNull).ToString());
        }
    }
}