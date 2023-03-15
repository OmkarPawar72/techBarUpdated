using techBar.Data.Base;
using techBar.Models;

namespace techBar.Data.Services
{
    public class SellersService : EntityBaseRepository<Sellers>, ISellersService
    {
        public SellersService(EcomDbContext context) :base (context) 
        {

        }
    }
}
