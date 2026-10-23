using Demo_ASPMVC_05__Auth.BLL.Interfaces;
using Demo_ASPMVC_05__Auth.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo_ASPMVC_05__Auth.BLL.Services
{
    public class ProductService : IProductService
    {
        #region Fake Data
        private static List<ProductModel> _Products = [
            new ProductModel { Id = 1, Name = "Pomme", Price = 4.99},
            new ProductModel { Id = 2, Name = "Poire", Price = 1.99}
        ];
        #endregion

        public IEnumerable<ProductModel> GetAll()
        {
            return _Products.AsReadOnly();
        }
    }
}
