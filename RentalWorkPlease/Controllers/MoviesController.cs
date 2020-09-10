using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentalWorkPlease.Data;
using RentalWorkPlease.Models;


namespace RentalWorkPlease.Controllers
{
    [Authorize]
    public class MoviesController : Controller
    {
        private readonly RentalWorkPleaseContext _context;

        public MoviesController(RentalWorkPleaseContext context)
        {
            _context = context;
        }

        // GET: Movies
        public async Task<IActionResult> Index(int? id, int? GenreId)
        {
            List<MovieIndexData> MData = new List<MovieIndexData>();
            var viewModel = new MovieIndexData();
            viewModel.Movies = await _context.Movies
                  .Include(i => i.GenreAssigns)
                    .ThenInclude(i => i.Genre)
                  .AsNoTracking()
                  .ToListAsync();

            if (id != null)
            {
                ViewData["MovieId"] = id.Value;
                Movie movie = viewModel.Movies.Where(
                    i => i.MovieID == id.Value).Single();
                viewModel.Genres = movie.GenreAssigns.Select(s => s.Genre);
            }
            return View(viewModel);
        }

        //POST: Movies
        [HttpPost]
        public ActionResult Index(string[] ids)
        {
            if (ids == null || ids.Length == 0)
            {
                ModelState.AddModelError("", "No items to delete");
                return View();
            }
            List<int> TaskIds = ids.Select(x => Int32.Parse(x)).ToList();
            for(int i = 0; i < TaskIds.Count(); i++)
            {
                var SelectedMovie = _context.Movies.Find(TaskIds[i]);
                _context.Movies.Remove(SelectedMovie);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.MovieID == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            var movie = new Movie();
            movie.GenreAssigns = new List<GenreAssign>();
            PopulateAssignedGenreData(movie);
            return View();
        }

        // POST: Movies/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MovieName, CreationDate, Active")] Movie movie, string[] selectedGenres)
        {
            if (selectedGenres != null)
            {
                movie.GenreAssigns = new List<GenreAssign>();
                foreach (var genre in selectedGenres)
                {
                    var genreToAdd = new GenreAssign { MovieID = movie.MovieID, GenreID = int.Parse(genre) };
                    movie.GenreAssigns.Add(genreToAdd);
                }
            }
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateAssignedGenreData(movie);
            return View(movie);
        }

        // GET: Movies/Edit/5

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .Include(i => i.GenreAssigns)
                    .ThenInclude(i => i.Genre)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.MovieID == id);
            if (movie == null)
            {
                return NotFound();
            }
            PopulateAssignedGenreData(movie);
            return View(movie);
        }

        private void PopulateAssignedGenreData(Movie movie)
        {
            var allGenres = _context.Genres;
            var movieGenres = new HashSet<int>(movie.GenreAssigns.Select(c => c.GenreID));
            var viewModel = new List<AssignedGenreData>();
            foreach (var genre in allGenres)
            {
                viewModel.Add(new AssignedGenreData
                {
                    GenreID = genre.GenreID,
                    GenreName = genre.GenreName,
                    Assigned = movieGenres.Contains(genre.GenreID)
                });
            }
            ViewData["Genres"] = viewModel;
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string[] selectedGenres)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieToUpdate = await _context.Movies
                .Include(i => i.GenreAssigns)
                    .ThenInclude(i => i.Genre)
                .FirstOrDefaultAsync(m => m.MovieID == id);

            if (await TryUpdateModelAsync<Movie>(
                movieToUpdate,
                "",
                i => i.MovieName, i => i.CreationDate, i => i.Active))
            {

                UpdateMovieGenres(selectedGenres, movieToUpdate);
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
            UpdateMovieGenres(selectedGenres, movieToUpdate);
            PopulateAssignedGenreData(movieToUpdate);
            return View(movieToUpdate);
        }


        private void UpdateMovieGenres(string[] selectedGenres, Movie movieToUpdate)
        {
            if (selectedGenres == null)
            {
                movieToUpdate.GenreAssigns = new List<GenreAssign>();
                return;
            }

            var selectedGenreHS = new HashSet<string>(selectedGenres);
            var movieGenres = new HashSet<int>
                (movieToUpdate.GenreAssigns.Select(c => c.Genre.GenreID));
            foreach (var genre in _context.Genres)
            {
                if (selectedGenreHS.Contains(genre.GenreID.ToString()))
                {
                    if (!movieGenres.Contains(genre.GenreID))
                    {
                        movieToUpdate.GenreAssigns.Add(new GenreAssign { MovieID = movieToUpdate.MovieID, GenreID = genre.GenreID });
                    }
                }
                else
                {

                    if (movieGenres.Contains(genre.GenreID))
                    {
                        GenreAssign genreToRemove = movieToUpdate.GenreAssigns.FirstOrDefault(i => i.GenreID == genre.GenreID);
                        _context.Remove(genreToRemove);
                    }
                }
            }
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.MovieID == id);
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
            Movie movie = await _context.Movies
                .Include(i => i.GenreAssigns)
                .SingleAsync(i => i.MovieID == id);
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost, ActionName("DeleteMovies")]
       
        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.MovieID == id);
        }
    }
}
