using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCSepet.Models
{
    public class CartItem
    {
        //bir sepetin .....'sı olur.
        public CartItem()
        {
            Quantity = 1;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal? Price { get; set; }

        public decimal? SubTotal
        {
            get
            {
                return Price * Quantity;
            }
        }
    }
}