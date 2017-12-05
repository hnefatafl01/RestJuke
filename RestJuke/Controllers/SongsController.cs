using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestJuke.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestJuke.Controllers
{
    [Route("api/Songs")]
    public class SongsController : Controller
    {
        private readonly SongContext _context;

        public SongsController(SongContext context) 
        {
            _context = context;
            if (_context.SongList.Count() == 0)
            {
                _context.SongList.Add(new Song { 
                    Id = 1,
                    Name = "Solitary Man",
                    Duration = 123,
                    Album = "American III: Solitary Man",
                    Artist = "Johnny Cash"
                });
                _context.SaveChanges();
            } 
        }

        // GET: api/song
        [HttpGet]
        public IEnumerable<Song> GetAll()
        {
            return _context.SongList.ToList();
        }

        // GET api/song/5
        [HttpGet("{id}", Name = "GetSong")]
        public IActionResult GetById(long id)
        {
            var song = _context.SongList.FirstOrDefault(s => s.Id == id);
            if (song == null) 
            {
                return NotFound();
            }
            return new ObjectResult(song);
        }

        // POST api/song
        [HttpPost]
        public IActionResult Create([FromBody] Song song)
        {
            if (song == null) 
            {
                return BadRequest();
            }
            _context.SongList.Add(song);
            _context.SaveChanges();
            return CreatedAtRoute("GetSong", new { Id = song.Id, Name = song.Name, duration = song.Duration }, song);
        }

        // PUT api/song/5
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Song song)
        {
            if (song == null || song.Id != id)
            {
                return BadRequest();
            }
            var songItem = _context.SongList.FirstOrDefault(s => s.Id == id);
            if (songItem == null)
            {
                return NotFound();
            }

            songItem.Duration = song.Duration;
            songItem.Name = song.Name;
            songItem.Album = song.Album;
            songItem.Artist = song.Artist;

            _context.SongList.Update(songItem);
            _context.SaveChanges();
            return new NoContentResult();
        }

        // DELETE api/song/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var songItem = _context.SongList.FirstOrDefault(s => s.Id == id);
            if (songItem == null)
            {
                return NotFound();
            }
            _context.Remove(songItem);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}
