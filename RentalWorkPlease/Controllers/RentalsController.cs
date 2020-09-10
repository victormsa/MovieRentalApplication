using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentalWorkPlease.Data;
using RentalWorkPlease.Models;

namespace RentalWorkPlease.Controllers
{
    [Authorize]
    public class RentalsController : Controller
    {
        private readonly RentalWorkPleaseContext _context;

        public RentalsController(RentalWorkPleaseContext context)
        {
            _context = context;
        }

        // GET: Rentals
        public async Task<IActionResult> Index(int? id, int? MovieId)
        {
            var viewModel = new RentalIndexData();
            viewModel.Rentals = await _context.Rentals
                  
                  .Include(i => i.MovieAssigns)
                    .ThenInclude(i => i.Movie)
                  .AsNoTracking()
                  .ToListAsync();

            if (id != null)
            {
                ViewData["RentalId"] = id.Value;
                Rental rental = viewModel.Rentals.Where(
                    i => i.RentalID == id.Value).Single();
                viewModel.Movies = rental.MovieAssigns.Select(s => s.Movie);
            }

            return View(viewModel);
        }

        // GET: Rentals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rental = await _context.Rentals
                .FirstOrDefaultAsync(m => m.RentalID == id);
            if (rental == null)
            {
                return NotFound();
            }

            return View(rental);
        }

        // GET: Rentals/Create
        public IActionResult Create()
        {
            var rental = new Rental();
            rental.MovieAssigns = new List<MovieAssign>();
            PopulateAssignedMovieData(rental);
            return View();
        }

        // POST: Rentals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RentalID,Cpf,RentalDate")] Rental rental, string[] selectedMovies)
        {
            if (selectedMovies != null)
            {
                rental.MovieAssigns = new List<MovieAssign>();
                foreach (var movie in selectedMovies)
                {
                    var movieToAdd = new MovieAssign { RentalID = rental.RentalID, MovieID = int.Parse(movie) };
                    rental.MovieAssigns.Add(movieToAdd);
                }
            }
            if (ModelState.IsValid)
            {
                _context.Add(rental);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateAssignedMovieData(rental);
            return View(rental);
        }

        // GET: Rentals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rental = await _context.Rentals
                .Include(i => i.MovieAssigns)
                    .ThenInclude(i => i.Movie)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.RentalID == id);
            if (rental == null)
            {
                return NotFound();
            }
            PopulateAssignedMovieData(rental);
            return View(rental);
        }

        private void PopulateAssignedMovieData(Rental rental)
        {
            var allMovies = _context.Movies;
            var rentalMovies = new HashSet<int>(rental.MovieAssigns.Select(c => c.MovieID));
            var viewModel = new List<AssignedMovieData>();
            foreach (var movie in allMovies)
            {
                viewModel.Add(new AssignedMovieData
                {
                    MovieID = movie.MovieID,
                    MovieName = movie.MovieName,
                    Assigned = rentalMovies.Contains(movie.MovieID)
                });
            }
            ViewData["Movies"] = viewModel;
        }

        // POST: Rentals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string[] selectedMovies)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rentalToUpdate = await _context.Rentals
                .Include(i => i.MovieAssigns)
                    .ThenInclude(i => i.Movie)
                .FirstOrDefaultAsync(m => m.RentalID == id);

            if (await TryUpdateModelAsync<Rental>(
                rentalToUpdate,
                "",
                i => i.Cpf, i => i.RentalDate))
            {
                
                UpdateMovieMovies(selectedMovies, rentalToUpdate);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
                return RedirectToAction(nameof(Index));
            }
            UpdateMovieMovies(selectedMovies, rentalToUpdate);
            PopulateAssignedMovieData(rentalToUpdate);
            return View(rentalToUpdate);
        }


        private void UpdateMovieMovies(string[] selectedMovies, Rental rentalToUpdate)
        {
            if (selectedMovies == null)
            {
                rentalToUpdate.MovieAssigns = new List<MovieAssign>();
                return;
            }

            var selectedMovieHS = new HashSet<string>(selectedMovies);
            var rentalMovies = new HashSet<int>
                (rentalToUpdate.MovieAssigns.Select(c => c.Movie.MovieID));
            foreach (var movie in _context.Movies)
            {
                if (selectedMovieHS.Contains(movie.MovieID.ToString()))
                {
                    if (!rentalMovies.Contains(movie.MovieID))
                    {
                        rentalToUpdate.MovieAssigns.Add(new MovieAssign { RentalID = rentalToUpdate.RentalID, MovieID = movie.MovieID });
                    }
                }
                else
                {

                    if (rentalMovies.Contains(movie.MovieID))
                    {
                        MovieAssign movieToRemove = rentalToUpdate.MovieAssigns.FirstOrDefault(i => i.MovieID == movie.MovieID);
                        _context.Remove(movieToRemove);
                    }
                }
            }
        }

        // GET: Rentals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rental = await _context.Rentals
                .FirstOrDefaultAsync(m => m.RentalID == id);
            if (rental == null)
            {
                return NotFound();
            }

            return View(rental);
        }

        // POST: Rentals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rental = await _context.Rentals.FindAsync(id);
            _context.Rentals.Remove(rental);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RentalExists(int id)
        {
            return _context.Rentals.Any(e => e.RentalID == id);
        }
    }
}
