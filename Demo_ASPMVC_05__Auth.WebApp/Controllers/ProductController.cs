using Demo_ASPMVC_05__Auth.BLL.Interfaces;
using Demo_ASPMVC_05__Auth.BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demo_ASPMVC_05__Auth.WebApp.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        public IActionResult Index()
        {
            // TODO Faire des ViewModel quand on a le temps :D
            IEnumerable<ProductModel> products = productService.GetAll();
            return View(products);
        }
    }
}
