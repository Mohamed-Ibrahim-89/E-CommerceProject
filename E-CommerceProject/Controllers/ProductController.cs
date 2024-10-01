using E_CommerceProject.Entities.Models;
using E_CommerceProject.Entities.ViewModels;
using E_CommerceProject.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace E_CommerceProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController(IBaseRepository<Product> productRepository, IUploadFile uploadFile, IBaseRepository<Category> categoryRepository, IToastNotification toastNotification, IWebHostEnvironment webHostingEnvironment, IBaseRepository<Discount> discountRepository) : Controller
    {
        private readonly IBaseRepository<Product> _productRepository = productRepository;
        private readonly IBaseRepository<Category> _categoryRepository = categoryRepository;
        private readonly IBaseRepository<Discount> _discountRepository = discountRepository;
        private readonly IUploadFile _uploadFile = uploadFile;
        private readonly IToastNotification _toastNotification = toastNotification;
        private readonly IWebHostEnvironment _webHostingEnvironment = webHostingEnvironment;


        public async Task<IActionResult> List()
        {
            var products = await _productRepository.GetAll(null, ["Category", "Discount"]);

            return View(products);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int productId)
        {
            try
            {
                var product = await _productRepository.GetById(p => p.ProductId == productId, ["Category", "Discount"]);
                if (product == null)
                {
                    return NotFound();
                }
                return View(product);
            }
            catch (InvalidOperationException ex)
            {
                ViewData["Error"] = ex.Message;
            }

            return View();
        }

        public async Task<IActionResult> Create()
        {
            var categories = await _categoryRepository.GetAll();
            var discounts = await _discountRepository.GetAll();

            var productViewModel = new ProductViewModel
            {
                Product = new Product(),
                Categories = categories,
                Discounts = discounts
            };

            return View("ProductForm", productViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            try
            {
                if (model.File != null)
                {
                    string filePath = await _uploadFile.UploadFileAsync("\\Images\\Product\\", model.File);
                    model.Product.Cover = filePath;
                }
                if (ModelState.IsValid)
                {
                    var product = new Product
                    {
                        ProductId = model.Product.ProductId,
                        Name = model.Product.Name,
                        Description = model.Product.Description,
                        Price = model.Product.Price,
                        Cover = model.Product.Cover,
                        QuantityInStock = model.Product.QuantityInStock,
                        CategoryId = model.Product.CategoryId,
                        DiscountId = model.Product.DiscountId
                    };

                    await _productRepository.AddItem(product);

                    _toastNotification.AddSuccessToastMessage("Item added successfully");
                    return RedirectToAction("List");
                }

                return View("ProductForm");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("ProductForm");
            }
        }


        public async Task<IActionResult> Edit(int productId)
        {
            var product = await _productRepository.GetById(p => p.ProductId == productId, ["Category", "Discount"]);
            if (product == null)
            {
                return NotFound();
            }
            var categories = await _categoryRepository.GetAll();
            var discounts = await _discountRepository.GetAll();
            var productViewModel = new ProductViewModel
            {
                Product = product,
                Categories = categories,
                Discounts = discounts
            };
            return View("ProductForm", productViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductViewModel model)
        {
            try
            {
                if (model.File != null)
                {
                    string filePath = await _uploadFile.UploadFileAsync("\\Images\\Product\\", model.File);
                    model.Product.Cover = filePath;
                }
                if (ModelState.IsValid)
                {
                    var product = new Product
                    {
                        ProductId = model.Product.ProductId,
                        Name = model.Product.Name,
                        Description = model.Product.Description,
                        Price = model.Product.Price,
                        Cover = model.Product.Cover,
                        QuantityInStock = model.Product.QuantityInStock,
                        CategoryId = model.Product.CategoryId,
                        DiscountId = model.Product.DiscountId
                    };
                    await _productRepository.UpdateItem(product);
                    _toastNotification.AddSuccessToastMessage("Item updated successfully");
                    return RedirectToAction("List");
                }
                return View("ProductForm", model);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("ProductForm");
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var product = await _productRepository.GetById(p => p.ProductId == id);

                if (product == null)
                {
                    return NotFound();
                }
                string imagePath = product.Cover;

                if (!string.IsNullOrEmpty(imagePath))
                {
                    string fullPath = _webHostingEnvironment.WebRootPath + imagePath;

                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                }

                await _productRepository.DeleteItem(id);

                return Ok();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> Search(string searchQuery)
        {
            IEnumerable<Product> products;

            if (searchQuery != null)
            {
                ViewBag.SearchQuery = searchQuery;
                products = await _productRepository.GetAll(p => p.Name.Contains(searchQuery), ["Category", "Discount"]);
            }
            else
            {
                products = await _productRepository.GetAll(null, ["Category", "Discount"]);
            }

            return PartialView("_ProductCard", products);
        }
    }
}
