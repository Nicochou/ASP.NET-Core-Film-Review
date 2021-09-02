using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RateMyMovie.Models;
using System.Linq;
using System.Threading.Tasks;

namespace RateMyMovie.Controllers
{
    [ApiController]
    [Route("genres")]
    public class GenreSetController : ControllerBase
    {
        private ILogger<GenreSetController> _logger;
        private MovieDbContext _context;
        public GenreSetController(ILogger<GenreSetController> logger, MovieDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GenreSet>> Get(int id)
        {
            var genre = await _context.GenreSets.FindAsync(id);

            if (genre == null)
            {
                return NotFound();
            }
            return genre;
        }

        [HttpGet]
        public IEnumerable<GenreSet> GetAll()
        {
            return _context.GenreSets.Select(g => g).ToArray();
        }

        [HttpPost]
        public void Post(GenreSet genre)
        {
            _context.GenreSets.Add(genre);
            _context.SaveChanges();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, GenreSet newGenre)
        {
            if (id != newGenre.Id)
            {
                return BadRequest();
            }

            _context.Entry(newGenre).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenreSetExists(id))
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
            var genre = await _context.GenreSets.FindAsync(id);
            if (genre == null)
            {
                return NotFound();
            }

            _context.GenreSets.Remove(genre);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GenreSetExists(int id)
        {
            var genre = _context.GenreSets.Find(id);
            return genre != null;
        }
    }
}
