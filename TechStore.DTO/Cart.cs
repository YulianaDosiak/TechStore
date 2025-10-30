using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStore.DTO
{
    public class Cart
    {
        public int CartID { get; set; }
        public int UserID { get; set; }

        public override string ToString()
        {
            return $"CartID: {CartID}, UserID: {UserID}";
        }
    }
}