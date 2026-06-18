using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using project_18.Data;
using project_18.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace project_18.Controllers
{
    public class AdminController : Controller
    {
        private AppDbContext context { get; set; }

        public AdminController(AppDbContext ctx)
        {
            context = ctx;
        }

        public IActionResult Index()
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

            return RedirectToAction("Index");
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

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteProduct(int productId)
        {
            string role = HttpContext.Session.GetString("Role") ?? "";
            if (role != "Admin")
            {
                return RedirectToAction("Login", "Account");
            }

            var product = context.Products.FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                bool hasOrders = context.Orders.Any(o => o.ProductId == productId);
                if (hasOrders)
                {
                    TempData["ErrorMessage"] = "Cannot delete product because it has associated orders/sales logs.";
                }
                else
                {
                    context.Products.Remove(product);
                    context.SaveChanges();
                    TempData["SuccessMessage"] = "Product deleted successfully.";
                }
            }

            return RedirectToAction("Index");
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
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
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

            return RedirectToAction("Index");
        }
    }
}
