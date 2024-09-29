using E_CommerceProject.Entities.Models;
using E_CommerceProject.Entities.ViewModels;
using E_CommerceProject.Repositories.Implementations;
using E_CommerceProject.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace E_CommerceProject.Controllers
{
    public class ProductController(IBaseRepository<Product> productRepository, IUploadFile uploadFile, IBaseRepository<Category> categoryRepository, IToastNotification toastNotification) : Controller
    {
        private readonly IBaseRepository<Product> _productRepository = productRepository;
        private readonly IBaseRepository<Category> _categoryRepository = categoryRepository;
        private readonly IUploadFile _uploadFile = uploadFile;
        private readonly IToastNotification _toastNotification = toastNotification;

        public async Task<IActionResult> List()
        {
            var products = await _productRepository.GetAll(null, ["Category"]);

            return View(products);
        }

        public async Task<IActionResult> Details(int productId)
        {
            var product = await _productRepository.GetById(productId);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        public async Task<IActionResult> Create()
        {
            var categories = await _categoryRepository.GetAll();

            var productViewModel = new ProductViewModel
            {
                Product = new Product(),
                Categories = categories,
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
                        CategoryId = model.Product.CategoryId
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
            var product = await _productRepository.GetById(productId);
            if (product == null)
            {
                return NotFound();
            }
            var categories = await _categoryRepository.GetAll();
            var productViewModel = new ProductViewModel
            {
                Product = product,
                Categories = categories,
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
                        CategoryId = model.Product.CategoryId
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
                await _productRepository.DeleteItem(id);

                return Ok();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        public async Task<IActionResult> Search(string searchQuery)
        {
            IEnumerable<Product> products;

            if (searchQuery != null)
            {
                ViewBag.SearchQuery = searchQuery;
                products = await _productRepository.GetAll(p => p.Name.Contains(searchQuery), ["Category"]);
            }
            else
            {
                products = await _productRepository.GetAll(null, ["Category"]);
            }

            return PartialView("_ProductCard", products);
        }
    }
}
