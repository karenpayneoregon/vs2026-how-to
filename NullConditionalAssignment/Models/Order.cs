namespace NullConditionalAssignment.Models;

#pragma warning disable CS8618
public class Order
{
    public int Id { get; set; }
    public List<OrderItem> OrderItems { get; set; }
}