using Demo_ASPMVC_05__Auth.BLL.Models;

namespace Demo_ASPMVC_05__Auth.BLL.Interfaces
{
    public interface IProductService
    {
        // Service métier pour l'exemple

        IEnumerable<ProductModel> GetAll();
    }
}
