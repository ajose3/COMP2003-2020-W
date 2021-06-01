using AdminInterface.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace AdminInterface.Controllers
{
    [Authorize]
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


            // get data for graph
            List<DashboardGraphNode> graphNodes = new List<DashboardGraphNode>();
            // get year of most recent order date
            var recentDate = orders[0].TimeOrdered;
            for (int i = 0; i < 5; i++)
            {
                // where for year and month:
                // get list of products for each of last 5 months
                List<Orders> monthOrders = orders.Where(o => (o.TimeOrdered.Year == recentDate.Year) && (o.TimeOrdered.Month == (recentDate.Month - 4 + i))).ToList();
                int totalSales = 0;
                int month = monthOrders[0].TimeOrdered.Month;
                foreach (var theOrder in monthOrders)
                {
                    totalSales += theOrder.Quantity;
                }
                graphNodes.Add(new DashboardGraphNode(totalSales, month));
            }
            ViewData["GraphNodes"] = graphNodes;


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
