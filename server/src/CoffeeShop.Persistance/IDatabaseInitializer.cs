namespace CoffeeShop.Persistance
{
    using System.Threading.Tasks;

    public interface IDatabaseInitializer
    {
        Task SeedDatabase();
    }
}
