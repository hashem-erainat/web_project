using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using project_18.Data;
using project_18.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;

namespace project_18.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext context { get; set; }

        public HomeController(AppDbContext ctx)
        {
            context = ctx;
        }

        public IActionResult Index(int? categoryId, string? search)
        {
            var categories = context.Categories.OrderBy(c => c.Name).ToList();
            ViewBag.Categories = categories;
            ViewBag.SelectedCategory = categoryId ?? 0;
            ViewBag.SearchQuery = search ?? "";

            var query = context.Products.Include(p => p.Category).AsQueryable();

            if (categoryId.HasValue && categoryId.Value > 0)
            {
                query = query.Where(p => p.CategoryId == categoryId.Value);
            }

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.Name.Contains(search) || p.Description.Contains(search));
            }

            var products = query.OrderBy(p => p.IsSold)
                                .ThenByDescending(p => p.CreatedAt)
                                .ToList();

            return View(products);
        }

        public IActionResult Details(int id)
        {
            var product = context.Products.Include(p => p.Category)
                                          .FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        public IActionResult Cart()
        {
            string cart = HttpContext.Session.GetString("CartProductIds") ?? "";
            var products = new List<Product>();

            if (!string.IsNullOrEmpty(cart))
            {
                var ids = cart.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                              .Select(int.Parse)
                              .ToList();

                products = context.Products.Include(p => p.Category)
                                           .Where(p => ids.Contains(p.ProductId) && !p.IsSold)
                                           .ToList();
            }

            return View(products);
        }

        public IActionResult MyOrders()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
            {
                return RedirectToAction("Login", "Account");
            }

            var orders = context.Orders.Include(o => o.Product)
                                       .Where(o => o.UserId == userId.Value)
                                       .OrderByDescending(o => o.PurchaseDate)
                                       .ToList();

            return View(orders);
        }

        public IActionResult AdminDashboard()
        {
            string role = HttpContext.Session.GetString("Role") ?? "";
            if (role != "Admin")
            {
                return RedirectToAction("Login", "Account");
            }

            ViewBag.TotalSales = context.Orders.Where(o => o.Status == "Completed").Sum(o => o.TotalPrice);
            ViewBag.AvailableCount = context.Products.Count(p => !p.IsSold);
            ViewBag.SoldCount = context.Products.Count(p => p.IsSold);
            ViewBag.Categories = context.Categories.OrderBy(c => c.Name).ToList();

            var products = context.Products.Include(p => p.Category)
                                           .OrderByDescending(p => p.CreatedAt)
                                           .ToList();

            ViewBag.AllOrders = context.Orders.Include(o => o.Product)
                                              .Include(o => o.User)
                                              .OrderByDescending(o => o.PurchaseDate)
                                              .ToList();

            return View(products);
        }

        [HttpPost]
        public IActionResult AddToCart(int productId)
        {
            var product = context.Products.Find(productId);
            if (product == null || product.IsSold)
            {
                return RedirectToAction("Index");
            }

            string cart = HttpContext.Session.GetString("CartProductIds") ?? "";
            List<string> ids = cart.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            if (!ids.Contains(productId.ToString()))
            {
                ids.Add(productId.ToString());
                cart = string.Join(",", ids);
                HttpContext.Session.SetString("CartProductIds", cart);
            }

            return RedirectToAction("Cart");
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int productId)
        {
            string cart = HttpContext.Session.GetString("CartProductIds") ?? "";
            List<string> ids = cart.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            if (ids.Contains(productId.ToString()))
            {
                ids.Remove(productId.ToString());
                cart = string.Join(",", ids);
                if (string.IsNullOrEmpty(cart))
                {
                    HttpContext.Session.Remove("CartProductIds");
                }
                else
                {
                    HttpContext.Session.SetString("CartProductIds", cart);
                }
            }

            return RedirectToAction("Cart");
        }

        [HttpPost]
        public IActionResult Checkout()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
            {
                return RedirectToAction("Login", "Account");
            }

            string cart = HttpContext.Session.GetString("CartProductIds") ?? "";
            if (string.IsNullOrEmpty(cart))
            {
                return RedirectToAction("Cart");
            }

            var ids = cart.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                          .Select(int.Parse)
                          .ToList();

            var products = context.Products.Where(p => ids.Contains(p.ProductId)).ToList();

            if (products.Any(p => p.IsSold))
            {
                TempData["ErrorMessage"] = "Sorry, some items in your cart are already sold!";
                return RedirectToAction("Cart");
            }

            foreach (var product in products)
            {
                var order = new Order
                {
                    UserId = userId.Value,
                    ProductId = product.ProductId,
                    PurchaseDate = DateTime.UtcNow,
                    TotalPrice = product.Price,
                    Status = "Pending"
                };
                context.Orders.Add(order);
                product.IsSold = true;
            }

            context.SaveChanges();
            HttpContext.Session.Remove("CartProductIds");
            TempData["SuccessMessage"] = "Purchase requested successfully! Waiting for admin approval.";

            return RedirectToAction("MyOrders");
        }

        [HttpPost]
        public IActionResult ApproveOrder(int orderId)
        {
            string role = HttpContext.Session.GetString("Role") ?? "";
            if (role != "Admin")
            {
                return RedirectToAction("Login", "Account");
            }

            var order = context.Orders.Include(o => o.Product).FirstOrDefault(o => o.OrderId == orderId);
            if (order != null && order.Status == "Pending")
            {
                order.Status = "Completed";
                if (order.Product != null)
                {
                    order.Product.IsSold = true;
                }
                context.SaveChanges();
            }

            return RedirectToAction("AdminDashboard");
        }

        [HttpPost]
        public IActionResult RejectOrder(int orderId)
        {
            string role = HttpContext.Session.GetString("Role") ?? "";
            if (role != "Admin")
            {
                return RedirectToAction("Login", "Account");
            }

            var order = context.Orders.Include(o => o.Product).FirstOrDefault(o => o.OrderId == orderId);
            if (order != null && order.Status == "Pending")
            {
                order.Status = "Rejected";
                if (order.Product != null)
                {
                    order.Product.IsSold = false;
                }
                context.SaveChanges();
            }

            return RedirectToAction("AdminDashboard");
        }

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            string role = HttpContext.Session.GetString("Role") ?? "";
            if (role != "Admin")
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                product.CreatedAt = DateTime.UtcNow;
                product.IsSold = false;
                if (string.IsNullOrEmpty(product.ImageUrl))
                {
                    product.ImageUrl = "/images/placeholder.png";
                }

                context.Products.Add(product);
                context.SaveChanges();
                return RedirectToAction("AdminDashboard");
            }

            return RedirectToAction("AdminDashboard");
        }

        [HttpPost]
        public IActionResult AddCategory(string name)
        {
            string role = HttpContext.Session.GetString("Role") ?? "";
            if (role != "Admin")
            {
                return RedirectToAction("Login", "Account");
            }

            if (!string.IsNullOrEmpty(name))
            {
                var category = new Category { Name = name };
                context.Categories.Add(category);
                context.SaveChanges();
            }

            return RedirectToAction("AdminDashboard");
        }
    }
}




