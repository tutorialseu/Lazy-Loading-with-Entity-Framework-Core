namespace LazyLoading;
public record Customer
{


    public Customer(Guid id, string name, string email)
    {
        Id = id;
        Name = name;
        Email = email;
    }

    public Guid Id { get; }
    public string Name { get; }
    public string Email { get; }
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();



    [Obsolete("EF Requires it")]
    protected Customer() { }
}
