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
    public class CartController : Controller
    {
        private AppDbContext context { get; set; }

        public CartController(AppDbContext ctx)
        {
            context = ctx;
        }

        public IActionResult Index()
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

        [HttpPost]
        public IActionResult Add(int productId)
        {
            var product = context.Products.Find(productId);
            if (product == null || product.IsSold)
            {
                return RedirectToAction("Index", "Home");
            }

            string cart = HttpContext.Session.GetString("CartProductIds") ?? "";
            List<string> ids = cart.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            if (!ids.Contains(productId.ToString()))
            {
                ids.Add(productId.ToString());
                cart = string.Join(",", ids);
                HttpContext.Session.SetString("CartProductIds", cart);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Remove(int productId)
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

            return RedirectToAction("Index");
        }
    }
}
