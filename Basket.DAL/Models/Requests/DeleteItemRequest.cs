using Basket.DAL.Enums;

namespace Basket.DAL.Requests
{
    /// <summary>
    /// Class DeleteItemRequest.
    /// </summary>
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

        public BasketError Validate()
        {
            if (string.IsNullOrEmpty(ClientId)) return BasketError.ClientIdCantBeNull;
            if (string.IsNullOrEmpty(SKU)) return BasketError.SKUCantBeNull;
            return BasketError.NoError;
        }
    }
}
