using Basket.WebApi.Models.Validations;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.WebApi.Models
{
    /// <summary>
    /// Class DeleteItemRequest.
    /// </summary>
    [Validator(typeof(DeleteItemValidation))]
    public class DeleteItemRequest
    {
        /// <summary>
        /// Gets or sets the client identifier.
        /// </summary>
        /// <value>The client identifier.</value>
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the sku.
        /// </summary>
        /// <value>The sku.</value>
        public string SKU { get; set; }

        public FluentValidation.Results.ValidationResult Validate()
        {
            var validator = new DeleteItemValidation();
            return validator.Validate(this);
        }
    }
}
