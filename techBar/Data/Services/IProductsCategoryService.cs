using techBar.Data.Base;
using techBar.Data.Enums;
using techBar.Data.ViewModels;
using techBar.Models;

namespace techBar.Data.Services
{
    public interface IProductsCategoryService : IEntityBaseRepository<ProductsCategory>
    {
        Task<ProductsCategory> GetProductsCategoryIdAsysnc(int id);
        Task<NewProductsDropdownVM> GetNewProductsDropdownsValues();
    }
}
