using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DotNetApplicationMVC.Data;
using DotNetApplicationMVC.Models;

namespace DotNetApplicationMVC.Controllers
{
    public class MoviesController : Controller
    {
        private readonly DotNetApplicationMVCContext _context;

        public MoviesController(DotNetApplicationMVCContext context)
        {
            _context = context;
        }

        // GET: Movies
        public async Task<IActionResult> Index( string MovieGenre, string SearchString)
        {
            //vendo sobre o banco de dados
            if (_context.Movie == null)
            {
                return Problem("Error with database connection");
            }

            // pegando de forma dinamica os generos existentes
            var genresQuery = from filmes in _context.Movie select filmes.Genre;


            // generos
            var movies = from m in _context.Movie select m;

            if (!String.IsNullOrEmpty(SearchString))
            {
                movies = movies.Where(movie => movie.Title!.ToLower().Contains(SearchString.ToLower()));
            }

            if (!String.IsNullOrEmpty(MovieGenre))
            {
                movies = movies.Where( movie => movie.Genre!.ToUpper() == MovieGenre.ToUpper());
            }

            MovieGenreViewModel movieGenderVM = new MovieGenreViewModel
            {
                Genres = new SelectList(await genresQuery.Distinct().ToListAsync()),
                Movies = await movies.ToListAsync() 
            };




            return View(movieGenderVM);
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Genre,Price")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.


        [HttpPost]

        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Price")] Movie movie)
        {

            // uma forma de fazer com que não se altere o id do filme pelo metodo post

            if (id != movie.Id)

            {
                return NotFound();
            }

            if (ModelState.IsValid)

            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
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
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movie.FindAsync(id);
            if (movie != null)
            {
                _context.Movie.Remove(movie);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.Id == id);
        }
    }
}
