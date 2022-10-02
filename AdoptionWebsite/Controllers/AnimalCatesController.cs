using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdoptionWebsite.Models;

namespace AdoptionWebsite.Controllers
{
    public class AnimalCatesController : Controller
    {
        public Animal_AdoptionContext _context = new Animal_AdoptionContext();
        //private readonly Animal_AdoptionContext _context;

        //public AnimalCatesController(Animal_AdoptionContext context)
        //{
        //    _context = context;
        //}

        // GET: AnimalCates
        public async Task<IActionResult> Index()
        {
            return View(await _context.AnimalCate.ToListAsync());
        }

        // GET: AnimalCates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animalCate = await _context.AnimalCate
                .FirstOrDefaultAsync(m => m.Idno == id);
            if (animalCate == null)
            {
                return NotFound();
            }

            return View(animalCate);
        }

        // GET: AnimalCates/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AnimalCates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idno,CateName")] AnimalCate animalCate)
        {
            if (ModelState.IsValid)
            {
                _context.Add(animalCate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(animalCate);
        }

        // GET: AnimalCates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animalCate = await _context.AnimalCate.FindAsync(id);
            if (animalCate == null)
            {
                return NotFound();
            }
            return View(animalCate);
        }

        // POST: AnimalCates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idno,CateName")] AnimalCate animalCate)
        {
            if (id != animalCate.Idno)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(animalCate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimalCateExists(animalCate.Idno))
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
            return View(animalCate);
        }

        // GET: AnimalCates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animalCate = await _context.AnimalCate
                .FirstOrDefaultAsync(m => m.Idno == id);
            if (animalCate == null)
            {
                return NotFound();
            }

            return View(animalCate);
        }

        // POST: AnimalCates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var animalCate = await _context.AnimalCate.FindAsync(id);
            _context.AnimalCate.Remove(animalCate);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnimalCateExists(int id)
        {
            return _context.AnimalCate.Any(e => e.Idno == id);
        }
    }
}
