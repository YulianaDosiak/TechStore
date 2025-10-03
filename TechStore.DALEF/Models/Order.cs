using System;
using System.Collections.Generic;

namespace TechStore.DALEF.Models;

public partial class Order
{
    public int OrderID { get; set; }

    public int? UserID { get; set; }

    public DateTime? OrderDate { get; set; }

    public decimal? TotalAmount { get; set; }

    public virtual User? User { get; set; }
}
