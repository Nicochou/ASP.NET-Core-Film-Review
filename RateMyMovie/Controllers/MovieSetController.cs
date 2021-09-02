using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RateMyMovie.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace RateMyMovie.Controllers
{
    [ApiController]
    [Route("movies")]
    public class MovieSetController : ControllerBase
    {
        private ILogger<MovieSetController> _logger;
        private MovieDbContext _context;
        public MovieSetController(ILogger<MovieSetController> logger, MovieDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MovieSet>> Get(int id)
        {
            var movie = await _context.MovieSets.Include(m => m.CommentSets).ThenInclude(c => c.User).Include(m => m.GenreMovies).ThenInclude(gm => gm.Genre).AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }
            return movie;
        }

        [HttpGet]
        public IEnumerable<MovieSet> GetAll()
        {
            return _context.MovieSets.Include(m => m.CommentSets).ThenInclude(c => c.User).Include(m => m.GenreMovies).ThenInclude(gm => gm.Genre).AsNoTracking().Select(m => m).ToArray();
        }

        [HttpPost]
        public async Task<IActionResult> Post(MovieCreate movie)
        {
            MovieSet newMovie = movie.MapMovieSet();

            _context.MovieSets.Add(newMovie);
            _context.SaveChanges();

            foreach (var genre in movie.GenreIds)
            {
                var g = await _context.GenreSets.FindAsync(genre);
                if (g == null)
                {
                    return NotFound();
                }
                newMovie.GenreMovies.Add(new GenreMovie { MovieId = newMovie.Id, GenreId = genre });
            }

            _context.MovieSets.Update(newMovie);
            _context.SaveChanges();

            return Created("data", newMovie);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, MovieSet newMovie)
        {
            if (id != newMovie.Id)
            {
                return BadRequest();
            }

            _context.Entry(newMovie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieSetExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var movie = await _context.MovieSets.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.MovieSets.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovieSetExists(int id)
        {
            var movie = _context.MovieSets.Find(id);
            return movie != null;
        }
    }
}
