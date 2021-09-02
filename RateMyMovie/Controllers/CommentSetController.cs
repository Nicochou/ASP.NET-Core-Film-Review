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
    [Route("comments")]
    public class CommentSetController : ControllerBase
    {
        private ILogger<CommentSetController> _logger;
        private MovieDbContext _context;
        public CommentSetController(ILogger<CommentSetController> logger, MovieDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CommentSet>> Get(int id)
        {
            var comment = await _context.CommentSets.FindAsync(id);

            if (comment == null)
            {
                return NotFound();
            }
            return comment;
        }

        [HttpGet]
        public IEnumerable<CommentSet> GetAll()
        {
            return _context.CommentSets.Select(u => u).ToArray();
        }

        [HttpPost]
        public void Post(CommentSet comment)
        {
            _context.CommentSets.Add(comment);
            _context.SaveChanges();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, CommentSet newComment)
        {
            if (id != newComment.Id)
            {
                return BadRequest();
            }

            _context.Entry(newComment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentSetExists(id))
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
            var comment = await _context.CommentSets.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            _context.CommentSets.Remove(comment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CommentSetExists(int id)
        {
            var comment = _context.CommentSets.Find(id);
            return comment != null;
        }
    }
}
