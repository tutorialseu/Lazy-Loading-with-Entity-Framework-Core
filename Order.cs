namespace LazyLoading;

public class Order
{
    public Order(Guid id, DateTime orderDate)
    {
        Id = id;
        OrderDate = orderDate;
    }
    public Guid Id { get; }
    public DateTime OrderDate { get; }



    [Obsolete("EF Requires it")]
    public Order() { }
}