using System;
using System.Collections.Generic;

namespace TechStore.DALEF.Models;

public partial class CartItem
{
    public int CartItemID { get; set; }

    public int? CartID { get; set; }

    public int? ProductID { get; set; }

    public int Quantity { get; set; }

    public virtual Cart? Cart { get; set; }

    public virtual Product? Product { get; set; }
}
