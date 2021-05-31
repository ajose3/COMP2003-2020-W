using AdminInterface.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;

namespace AdminInterface.Controllers
{
    public class HomeController : Controller
    {
        private readonly COMP2003_WContext _context;

        public HomeController(COMP2003_WContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            // get orders
            List<Orders> orders = await _context.Orders.ToListAsync();
            // sort orders so that first is most recent
            orders = orders.OrderByDescending(o => o.TimeOrdered.Date).ThenByDescending(o => o.TimeOrdered.TimeOfDay).ToList();
            // make sorted orders available to view
            ViewData["Orders"] = orders;

            return View(await _context.Products.ToListAsync());
        }


        /*private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }*/

        /*public IActionResult Index()
        {
            return View();
        }*/

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
