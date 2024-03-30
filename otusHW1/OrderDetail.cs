using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class OrderDetail
{
    [Key]
    public int OrderDetailID { get; set; }

    public int OrderID { get; set; }
    public int ProductID { get; set; }
    public int Quantity { get; set; }
    public decimal TotalCost { get; set; }

    [ForeignKey("OrderID")]
    public Order Order { get; set; }

    [ForeignKey("ProductID")]
    public Product Product { get; set; }
}
