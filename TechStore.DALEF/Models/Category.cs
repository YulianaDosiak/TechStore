using System;
using System.Collections.Generic;

namespace TechStore.DALEF.Models;

public partial class Category
{
    public int CategoryID { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
