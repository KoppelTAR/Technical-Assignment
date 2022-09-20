using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TechnicalAssignment.Data;
using TechnicalAssignment.Models;

namespace TechnicalAssignment.Controllers
{
    public class SavedUsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SavedUsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SavedUsers
        public async Task<IActionResult> Index()
        {
            return View(await _context.SavedUsers.ToListAsync());
        }

        // GET: SavedUsers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var savedUsers = await _context.SavedUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (savedUsers == null)
            {
                return NotFound();
            }

            return View(savedUsers);
        }

        // GET: SavedUsers/Create
        public IActionResult Create()
        {

            List<Sectors> sectorList = new List<Sectors>();
            sectorList = (from sectors in _context.Sectors
                          select sectors).ToList();
            /*foreach (Sectors sector in sectorList)
            {
                Debug.WriteLine(sector.SectorName.ToString());
            }
            */

            ViewBag.ListofSectors = sectorList;
            return View();
        }

        // POST: SavedUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,Sectors,ToS")] SavedUsers savedUsers)
        {
            List<Sectors> sectorList = new List<Sectors>();
            sectorList = (from sectors in _context.Sectors
                          select sectors).ToList();
            ViewBag.ListofSectors = sectorList;

            if (ModelState.IsValid)
                {
                _context.Add(savedUsers);
                await _context.SaveChangesAsync();

                ViewBag.EditUser = savedUsers;


                return RedirectToAction(nameof(Edit), new { id = savedUsers.Id.ToString() });
            }
            return View(savedUsers);

        }

        // GET: SavedUsers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            List<Sectors> sectorList = new List<Sectors>();
            sectorList = (from sectors in _context.Sectors
                          select sectors).ToList();
            ViewBag.ListofSectors = sectorList;
            if (id == null)
            {
                return NotFound();
            }

            var savedUsers = await _context.SavedUsers.FindAsync(id);
            if (savedUsers == null)
            {
                return NotFound();
            }
            return View(savedUsers);
        }

        // POST: SavedUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserName,Sectors,ToS")] SavedUsers savedUsers)
        {
            List<Sectors> sectorList = new List<Sectors>();
            sectorList = (from sectors in _context.Sectors
                          select sectors).ToList();
            ViewBag.ListofSectors = sectorList;
            if (id != savedUsers.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(savedUsers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SavedUsersExists(savedUsers.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Edit), new { id = savedUsers.Id.ToString() });
            }
            return View(savedUsers);
        }

        // GET: SavedUsers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var savedUsers = await _context.SavedUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (savedUsers == null)
            {
                return NotFound();
            }

            return View(savedUsers);
        }

        // POST: SavedUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var savedUsers = await _context.SavedUsers.FindAsync(id);
            _context.SavedUsers.Remove(savedUsers);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SavedUsersExists(int id)
        {
            return _context.SavedUsers.Any(e => e.Id == id);
        }
    }
}
