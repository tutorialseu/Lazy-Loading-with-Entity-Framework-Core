using AutoFixture.Xunit2;
using LazyLoading;
using Microsoft.EntityFrameworkCore;

namespace EfLazyLoading;

public class When_lazy_loading_is_off
{
    [Theory, AutoData]
    public void Related_data_must_be_included_explicitly(Customer customerInDb)
    {
        //Arrange
        using (var context = new CommerceContext(options))
        {
            context.Customers.Add(customerInDb);
            context.SaveChanges();
        }

        using (var context = new CommerceContext(options))
        {

            //Act
            var loadedCustomer = context.Customers.Include(c => c.Orders).First();

            //Assert
            Assert.Equivalent(customerInDb.Orders, loadedCustomer.Orders);
        }
    }

    [Theory, AutoData]
    public void Related_data_doesnt_load_automatically(Customer customerInDb)
    {
        //Arrange
        using (var context = new CommerceContext(options))
        {
            context.Customers.Add(customerInDb);
            context.SaveChanges();
        }

        using (var context = new CommerceContext(options))
        {
            //Act
            var customer = context.Customers.First();

            //Assert
            Assert.Empty(customer.Orders);
        }
    }





    readonly DbContextOptions<CommerceContext> options = new DbContextOptionsBuilder<CommerceContext>()
                                .UseSqlServer("Data Source = localhost, 1433; TrustServerCertificate=True; Encrypt=True; Database=Test;User id = SA; Password=1234!Secret;")
                                .Options;
    //Before Each
    public When_lazy_loading_is_off()
    {
        var context = new CommerceContext(options);
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
    }
}