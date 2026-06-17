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
    public class OrderController : Controller
    {
        private AppDbContext context { get; set; }

        public OrderController(AppDbContext ctx)
        {
            context = ctx;
        }

        public IActionResult Index()
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
                return RedirectToAction("Index", "Cart");
            }

            var ids = cart.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                          .Select(int.Parse)
                          .ToList();

            var products = context.Products.Where(p => ids.Contains(p.ProductId)).ToList();

            if (products.Any(p => p.IsSold))
            {
                TempData["ErrorMessage"] = "Sorry, some items in your cart are already sold!";
                return RedirectToAction("Index", "Cart");
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

            return RedirectToAction("Index");
        }
    }
}
