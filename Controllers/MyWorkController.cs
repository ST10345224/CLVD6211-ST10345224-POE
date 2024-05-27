using KhumaloCraftPOE.Areas.Identity.Data;
using KhumaloCraftPOE.Areas.Identity.Pages.Account;
using KhumaloCraftPOE.Data;
using KhumaloCraftPOE.Models;
using KhumaloCraftPOE.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace KhumaloCraftPOE.Controllers
{
    [Authorize]
    public class MyWorkController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IWebHostEnvironment _webHostEnvironment; // For accessing web root
        private readonly KhumaloCraftPOEDbContext _context; // Injected DbContext instance
        private readonly IOrderRepository _orderRepository; // Injected Order Repository
        //private readonly UserManager<IdentityUser> _userManager; // Injected UserManager instance

        public MyWorkController(IProductRepository productRepository, IWebHostEnvironment webHostEnvironment, KhumaloCraftPOEDbContext context, IOrderRepository orderRepository) // Inject DbContext
        {
            _productRepository = productRepository;
            _webHostEnvironment = webHostEnvironment;
            _context = context; // Store the injected DbContext instance
            _orderRepository = orderRepository;
            
        }

        // GET: MyWork/Index
        public IActionResult Index()
        {
            var products = _productRepository.GetAllProducts();
            var viewModel = new ProductsViewModel
            {
                Products = products
            };
            return View(viewModel);
        }

        // GET: MyWork/Details/5
        public IActionResult Details(int id)
        {
            var product = _productRepository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // GET: MyWork/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MyWork/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, and validate them. See https://go.microsoft.com/fwlink/?LinkId=317598 for details.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product, [FromForm(Name = "image")] IFormFile image)
        {
            //product.ImageUrl = "/Images/ProductImages/placeholder.png"; // Set default image URL
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    try
                    {
                        // Generate a unique file name and path
                        string fileName = Path.GetFileNameWithoutExtension(image.FileName) +
                                          Guid.NewGuid().ToString() +
                                          Path.GetExtension(image.FileName);
                        string uploads = Path.Combine(_webHostEnvironment.WebRootPath, "Images/ProductImages");
                        string filePath = Path.Combine(uploads, fileName);

                        // Save the uploaded image file
                        await image.CopyToAsync(new FileStream(filePath, FileMode.Create));

                        // Set the ImageUrl property on the product object
                        product.ImageUrl = Path.Combine("/Images/ProductImages/", fileName);
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", "Error saving image: " + ex.Message);
                        return View(product);
                    }
                }
                // Add product to repository and save
                _productRepository.AddProduct(product);
                
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            else 
            {
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        Console.WriteLine(error.ErrorMessage); // Log error message
                    }
                }

                return View(product);  // Return view with validation errors if any
            }
        }

        public IActionResult Login()
        {
            return View("./Areas/Identity/Pages/Account/Login.cshtml");
        }
        
        [HttpPost]
        public IActionResult AddToOrder(int ProductId)
        {
            // Get current user
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);


            // Get product from repository
            var product = _productRepository.GetProductById(ProductId);

            // Check if user has an open order
            var openOrder = _orderRepository.GetUserOpenOrder(userId);

            // Create new order if user doesn't have one
            if (openOrder == null)
            {
                openOrder = new Order { UserId = userId, CreatedAt = DateTime.Now, Status = "Pending", OrderItems = new List<OrderItem>() }; // Assuming UserId property in Order
                _orderRepository.Add(openOrder);
                _orderRepository.Save();
            }

            // Create new order item
            var orderItem = new OrderItem
            {
                OrderId = openOrder.OrderId,
                ProductId = ProductId,
                Quantity = 1 // Initially 1 item
            };
            
            _context.SaveChanges();
            // Add order item to the order
            openOrder.OrderItems.Add(orderItem);

            // Save changes to database
            _orderRepository.Save();

            // Optional: Confirmation logic (redirect, partial view)
            return RedirectToAction("Index"); // Redirect back to product listing
        }







        // GET: MyWork/Edit/5
        //public async Task<IActionResult> Edit()
        //{
        //    var products = _productRepository.GetAllProducts(); // Get all products

        //    ViewData["products"] = products.ToList(); // Store in ViewData

        //    return View(); // Use the same view for initial selection
        //}

        //public async Task<IActionResult> Edit(EditProductViewModel editViewModel)
        //{
        //    if (ModelState.IsValid) // Check for validation errors
        //    {
        //        var product = _productRepository.GetProductById(editViewModel.ProductId);
        //        if (product == null)
        //        {
        //            return NotFound();
        //        }

        //        // Update product details with editViewModel values
        //        product.Name = editViewModel.Name;
        //        product.Category = editViewModel.Category;
        //        // ... update other product properties

        //         _productRepository.UpdateProduct(product); // Update product in repository

        //        return RedirectToAction("Index", "Products"); // Redirect to product list or success page
        //    }

        //    return View(editViewModel); // Return the view with validation errors
        //}

        //// POST: MyWork/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, and validate them. See https://go.microsoft.com/fwlink/?LinkId=317598 for details.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit(int id, Product product)
        //{
        //    if (id != product.ProductId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        _productRepository.UpdateProduct(product);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(product);
        //}

        // GET: MyWork/Delete/5
        public IActionResult Delete(int id)
        {
            var product = _productRepository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: MyWork/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _productRepository.DeleteProduct(id);
            return RedirectToAction(nameof(Index));
        }
    }

}
