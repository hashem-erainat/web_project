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
    }
}
