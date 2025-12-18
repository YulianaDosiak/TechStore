using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStore.DTO
{
    public class Order
    {
        public int OrderID { get; set; }
        public int UserID { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalPrice { get; set; }
        public override string ToString()
        {
            return $"{OrderID}: User {UserID}, Date: {OrderDate.ToShortDateString()}, Total: {TotalAmount:C}";
        }
    }
}