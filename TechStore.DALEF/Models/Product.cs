using System;
using System.Collections.Generic;

namespace TechStore.DALEF.Models;

public partial class Product
{
    public int ProductID { get; set; }

    public string ProductName { get; set; } = null!;

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public int? CategoryID { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual Category? Category { get; set; }
}
