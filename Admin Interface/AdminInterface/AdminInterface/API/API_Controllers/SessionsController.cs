using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web_API.Models;
using AdminInterface.Models;

// API controllers have to be in AdminInterface namespace to be initialised with context
namespace AdminInterface.API_Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionsController : ControllerBase
    {
        private readonly COMP2003_WContext _context;

        public SessionsController(COMP2003_WContext context)
        {
            _context = context;
        }

        // GET: api/Sessions
        // - Validate Session -
        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<bool>> GetSession(API_Session session)
        {
            return Ok(true);
        }

        // GET: Sessions
        /* public async Task<IActionResult> Index()
         {
             var cOMP2003_WContext = _context.Sessions.Include(s => s.Customer);
             return View(await cOMP2003_WContext.ToListAsync());
         }

         // GET: Sessions/Details/5
         public async Task<IActionResult> Details(int? id)
         {
             if (id == null)
             {
                 return NotFound();
             }

             var session = await _context.Sessions
                 .Include(s => s.Customer)
                 .FirstOrDefaultAsync(m => m.SessionId == id);
             if (session == null)
             {
                 return NotFound();
             }

             return View(session);
         }

         // GET: Sessions/Create
         public IActionResult Create()
         {
             ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "EmailAddress");
             return View();
         }

         // POST: Sessions/Create
         // To protect from overposting attacks, enable the specific properties you want to bind to, for 
         // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
         [HttpPost]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> Create([Bind("SessionId,CustomerId,Token,ExpiryTime")] Session session)
         {
             if (ModelState.IsValid)
             {
                 _context.Add(session);
                 await _context.SaveChangesAsync();
                 return RedirectToAction(nameof(Index));
             }
             ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "EmailAddress", session.CustomerId);
             return View(session);
         }

         // GET: Sessions/Edit/5
         public async Task<IActionResult> Edit(int? id)
         {
             if (id == null)
             {
                 return NotFound();
             }

             var session = await _context.Sessions.FindAsync(id);
             if (session == null)
             {
                 return NotFound();
             }
             ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "EmailAddress", session.CustomerId);
             return View(session);
         }

         // POST: Sessions/Edit/5
         // To protect from overposting attacks, enable the specific properties you want to bind to, for 
         // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
         [HttpPost]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> Edit(int id, [Bind("SessionId,CustomerId,Token,ExpiryTime")] Session session)
         {
             if (id != session.SessionId)
             {
                 return NotFound();
             }

             if (ModelState.IsValid)
             {
                 try
                 {
                     _context.Update(session);
                     await _context.SaveChangesAsync();
                 }
                 catch (DbUpdateConcurrencyException)
                 {
                     if (!SessionExists(session.SessionId))
                     {
                         return NotFound();
                     }
                     else
                     {
                         throw;
                     }
                 }
                 return RedirectToAction(nameof(Index));
             }
             ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "EmailAddress", session.CustomerId);
             return View(session);
         }

         // GET: Sessions/Delete/5
         public async Task<IActionResult> Delete(int? id)
         {
             if (id == null)
             {
                 return NotFound();
             }

             var session = await _context.Sessions
                 .Include(s => s.Customer)
                 .FirstOrDefaultAsync(m => m.SessionId == id);
             if (session == null)
             {
                 return NotFound();
             }

             return View(session);
         }

         // POST: Sessions/Delete/5
         [HttpPost, ActionName("Delete")]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> DeleteConfirmed(int id)
         {
             var session = await _context.Sessions.FindAsync(id);
             _context.Sessions.Remove(session);
             await _context.SaveChangesAsync();
             return RedirectToAction(nameof(Index));
         }

         private bool SessionExists(int id)
         {
             return _context.Sessions.Any(e => e.SessionId == id);
         }*/
    }
}
