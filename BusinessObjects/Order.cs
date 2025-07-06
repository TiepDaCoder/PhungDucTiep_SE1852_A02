namespace BusinessObjects;

public partial class Order
{
    public int OrderID { get; set; }

    public int CustomerID { get; set; }

    public int EmployeeID { get; set; }

    public DateTime OrderDate { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}