using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieWebApi.Controllers
{
    
    [ApiController]
    [Route("[controller]s")]
    public class MovieController : ControllerBase
    {
        private static List<Movie> MovieList = new List<Movie>()
        {

            new Movie()
            {
                MovieId=1,
                MovieName="Upgrade",
                Duration=100,
                Category="Science Fiction",
                Director="Leigh Whannell"

            },

            new Movie()
            {
                MovieId=2,
                MovieName="A Beautiful Mind",
                Duration=135,
                Category="Drama",
                Director="Ron Howard"

            },
            new Movie()
            {
                MovieId=3,
                MovieName="Inception",
                Duration=148,
                Category="Science Fiction",
                Director="Christopher Nolan"

            },

        };

        [Route("error/[controller]s")]
        [HttpGet]
        public IActionResult Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context?.Error;

            if (exception is NotFoundException)
            {
                return NotFound(new { message = exception.Message });
            }
            else if (exception is BadRequestException)
            {
                return BadRequest(new { message = exception.Message });
            }
            else
            {
                return StatusCode(500, new { message = "Internal Server Error" });
            }

        }

        public class NotFoundException : Exception
        {
            public NotFoundException(string message) : base(message)
            {

            }
        }

        public class BadRequestException : Exception
        {
            public BadRequestException(string message) : base(message)
            {

            }
        }



        [HttpGet]
        public List<Movie> GetAll()
        {
            //listem statik olduğu için her baştan başladığında listem dolu gelecek
            //id ye göre sıralayarak getirdim görüntü açısından
            var movieList = MovieList.ToList();
            return movieList;
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var movie = MovieList.Find(x => x.MovieId == id);

            //böyle bir film yoksa null gelicek ve bad request olarak response vericek
            if (movie == null)
            {
                return NotFound(new { message = "Movie not found" });
            }

            //değilse onu döndürecek
            return Ok(movie);
        }


        //Fromquery ile yapılması
        [Route("fromquery/[controller]s")]
        [HttpGet]
        public IActionResult GetWithFromQuery([FromQuery] int id)
        {

            var movie = MovieList.Find(x => x.MovieId == id);

            //böyle bir film yoksa null gelicek ve bad request olarak response vericek
            if (movie == null)
            {
                return NotFound(new { message = "Movie not found" });
            }

            //değilse onu döndürecek
            return Ok(movie);
        }

        [HttpPost]
        public IActionResult postMovie([FromBody] Movie movie)
        {
            //ilk önce ekleme yapmadan önce listemde böyle bir nesne olup olmadığına bakmalıyım bunu da adına göre yapıcam

            var mymovie = MovieList.Where(x => x.MovieName == movie.MovieName).SingleOrDefault();

            if (mymovie is not null)
                return BadRequest(new { message = "Movie already exists" });

            MovieList.Add(movie);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult putMovie(int id, [FromBody] Movie movie)
        {
            var mymovie = MovieList.Find(x => x.MovieId == id);
            //böyle bir film yoksa null gelicek ve bad request olarak response vericek

            if (mymovie == null)
                return NotFound(new { message = "Movie not found" });


            mymovie.MovieName = movie.MovieName != default ? movie.MovieName : mymovie.MovieName;
            mymovie.Duration = movie.Duration != default ? movie.Duration : mymovie.Duration;
            mymovie.Category = movie.Category != default ? movie.Category : mymovie.Category;
            mymovie.Director = movie.Director != default ? movie.Director : mymovie.Director;

            return Ok();

        }

        [HttpPatch]
        public IActionResult patchMovie(int id, string director)
        {
            var mymovie = MovieList.Find(x => x.MovieId == id);
            //böyle bir film yoksa null gelicek ve bad request olarak response vericek

            if (mymovie == null)
                return NotFound(new { message = "Movie not found" });

            if (director == null)
                return BadRequest(new { message = "Director are required" });

            mymovie.Director = director;
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult deleteMovie(int id)
        {
            //yine ilk böyle bir film varmı yokmu diye kontrol ettik

            var mymovie = MovieList.Find(x => x.MovieId == id);
            //böyle bir film yoksa null gelicek ve bad request olarak response vericek

            if (mymovie == null)
                return NotFound(new { message = "Movie not found" });

            MovieList.Remove(mymovie);
            return Ok();
        }

        //moviname göre sırslayıp getirme
        [Route("order/[controller]s")]
        [HttpGet]
        public List<Movie> orderMovie()
        {
            var movielist = MovieList.OrderBy(x => x.MovieName).ToList();
            return movielist;
        }


    }

       

    
}
