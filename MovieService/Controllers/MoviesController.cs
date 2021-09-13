using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using MovieService.Data;
using MovieService.Models;

namespace MovieService.Controllers
{
    public class MoviesController : ApiController
    {
        private readonly MovieServiceContext db = new MovieServiceContext();

        private static LRUcache LR = new LRUcache(4);

        // GET: api/Movies
        public IQueryable<MovieDto> GetMovies()
        {
            var movies = from m in db.Movies
                         select new MovieDto()
                         {
                             Id = m.Id,
                             Title = m.Titulo,
                             Price = m.Precio,
                             Status="ok"
                         };
            return movies;
        }

        // GET: api/Movies/5
        [ResponseType(typeof(MovieDto))]
        public async Task<IHttpActionResult> GetMovie(int id)
        {
            var response = new MovieDto();
            //Movie movie = await db.Movies.FindAsync(id);
            Movie movie = LR.get_value_from_key(id);
            if (movie == null)
            {
                movie = await db.Movies.FindAsync(id);
                if (movie == null)
                    return NotFound();
                LR.push(id, movie);
                response.Status = "Se extrajo del DB y ahora esta en cache";
            }
            else
                response.Status = "Ya estaba en cache";
            response.Id = movie.Id;
            response.Price = movie.Precio;
            response.Title = movie.Titulo;
            response.Year = movie.Año;
            return Ok(response);
        }

        // PUT: api/Movies/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMovie(int id, Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != movie.Id)
            {
                return BadRequest();
            }

            db.Entry(movie).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Movies
        [ResponseType(typeof(Movie))]
        public async Task<IHttpActionResult> PostMovie(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Movies.Add(movie);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = movie.Id }, movie);
        }

        // DELETE: api/Movies/5
        [ResponseType(typeof(Movie))]
        public async Task<IHttpActionResult> DeleteMovie(int id)
        {
            Movie movie = await db.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            db.Movies.Remove(movie);
            await db.SaveChangesAsync();

            return Ok(movie);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MovieExists(int id)
        {
            return db.Movies.Count(e => e.Id == id) > 0;
        }
    }
}