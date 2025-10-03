using System;
using System.Collections.Generic;

namespace TechStore.DALEF.Models;

public partial class Cart
{
    public int CartID { get; set; }

    public int? UserID { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual User? User { get; set; }
}
