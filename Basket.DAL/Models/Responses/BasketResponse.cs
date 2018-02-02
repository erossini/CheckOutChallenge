using Basket.DAL.Enums;

namespace Basket.DAL.Responses
{
    public class BasketResponse
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="BasketResponse"/> is success.
        /// </summary>
        /// <value><c>true</c> if success; otherwise, <c>false</c>.</value>
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets the success result.
        /// </summary>
        /// <value>The success result.</value>
        public BasketResult SuccessResult { get; set; }

        /// <summary>
        /// Gets or sets the error.
        /// </summary>
        /// <value>The error.</value>
        public BasketError Error { get; set; }
    }
}
