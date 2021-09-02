using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RateMyMovie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateMyMovie.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserSetController : ControllerBase
    {
        private ILogger<UserSetController> _logger;
        private MovieDbContext _context;
        public UserSetController(ILogger<UserSetController> logger, MovieDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserSet>> Get(int id)
        {
            var user = await _context.UserSets.Include(u => u.CommentSets).AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet]
        public IEnumerable<UserSet> GetAll()
        {
            return _context.UserSets.Select(u => u).ToArray();
        }

        [HttpPost]
        public void Post(UserSet user)
        {
            user.Password = Encrypt(user.Password);
            _context.UserSets.Add(user);
            _context.SaveChanges();
        }

        private string Encrypt(string input)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputByte = System.Text.Encoding.UTF8.GetBytes(input);
                byte[] hashByte = md5.ComputeHash(inputByte);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashByte.Length; i++)
                {
                    sb.Append(hashByte[i].ToString("X2"));
                }

                Console.WriteLine(sb.ToString());

                return sb.ToString();
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<UserSet>> PostLogin(UserSet user)
        {
            var userLogin = await _context.UserSets.FirstOrDefaultAsync(u => u.Username == user.Username);

            if (userLogin == null)
            {
                return NotFound();
            }

            if (userLogin.Password != Encrypt(user.Password))
            {
                return Unauthorized();
            }

            return Ok(userLogin);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UserSet newUser)
        {
            if (id != newUser.Id)
            {
                return BadRequest();
            }

            _context.Entry(newUser).State = EntityState.Modified;

            try
            {
               await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserSetExists(id))
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
            var user = await _context.UserSets.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.UserSets.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserSetExists(int id)
        {
            var user = _context.UserSets.Find(id);
            return user != null;
        }
    }
}
