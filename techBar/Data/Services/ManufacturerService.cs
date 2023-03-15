using techBar.Data.Base;
using techBar.Models;

namespace techBar.Data.Services
{
    public class ManufacturerService : EntityBaseRepository <Manufacturer>,IManufacturerService
    {
        public ManufacturerService(EcomDbContext context): base(context)
        {

        }
    }
}
