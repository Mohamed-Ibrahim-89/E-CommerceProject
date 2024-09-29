using E_CommerceProject.Entities.Models;
using E_CommerceProject.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceProject.Controllers
{
    public class HomeController(IBaseRepository<Product> productRepository) : Controller
    {
        private readonly IBaseRepository<Product> _productRepository = productRepository;

        public async Task<IActionResult> Index()
        {
            var products = await _productRepository.GetAll(null, ["Category"]);

            return View(products);
        }

    }
}
