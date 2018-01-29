using Basket.WebApi.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.WebApi.Models
{
    public class BasketResponse
    {
        public bool Success { get; set; }
        public BasketResult SuccessResult { get; set; }
        public BasketError Error { get; set; }
    }
}
