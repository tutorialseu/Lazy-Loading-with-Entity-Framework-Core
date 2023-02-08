using AutoFixture.Xunit2;
using LazyLoading;
using Microsoft.EntityFrameworkCore;

namespace EfLazyLoading;

public class When_lazy_loading_is_on
{
    [Theory, AutoData]
    public void Related_data_loads_no_need_to_include_it_explicitly(Customer customerInDb)
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
            var loadedCustomer = context.Customers.First();

            //Assert
            Assert.Equivalent(customerInDb.Orders, loadedCustomer.Orders);
        }
    }


    readonly DbContextOptions<CommerceContext> options = new DbContextOptionsBuilder<CommerceContext>()
                                .UseLazyLoadingProxies()
                                .UseSqlServer("Data Source = localhost, 1433; TrustServerCertificate=True; Encrypt=True; Database=Test2;User id = SA; Password=1234!Secret;")
                                .Options;
    //Before Each
    public When_lazy_loading_is_on()
    {
        var context = new CommerceContext(options);
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();        
    }
}