﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdminInterface.Models;
using Microsoft.AspNetCore.Authorization;

namespace AdminInterface.Controllers
{
    [Authorize]
    public class ReviewsController : Controller
    {
        private readonly COMP2003_WContext _context;

        public ReviewsController(COMP2003_WContext context)
        {
            _context = context;
        }

        // GET: Reviews
        public async Task<IActionResult> Index()
        {
            var cOMP2003_WContext = _context.Reviews.Include(r => r.Customer).Include(r => r.Product);
            return View(await cOMP2003_WContext.ToListAsync());
        }

        // GET: Reviews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reviews = await _context.Reviews
                .Include(r => r.Customer)
                .Include(r => r.Product)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (reviews == null)
            {
                return NotFound();
            }

            return View(reviews);
        }

        // GET: Reviews/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "EmailAddress");
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Category");
            return View();
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,CustomerId,Rating,Title,Description")] Reviews reviews)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reviews);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "EmailAddress", reviews.CustomerId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Category", reviews.ProductId);
            return View(reviews);
        }

       

        private bool ReviewsExists(int id)
        {
            return _context.Reviews.Any(e => e.ProductId == id);
        }
    }
}
