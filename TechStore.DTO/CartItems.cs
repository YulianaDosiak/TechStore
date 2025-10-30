using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStore.DTO
{
    public class CartItems
    {
        public int CartItemID { get; set; } 
        public int CartID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }

        public override string ToString()
        {
            return $"CartItemID: {CartItemID}, Cart: {CartID}, Product: {ProductID}, Qty: {Quantity}";
        }
    }
}