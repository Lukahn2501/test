using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieRanker.Business;
using MovieRanker.Models;
using static MovieRanker.DTO.MovieDTO;

namespace MovieRanker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MovieController : ControllerBase
    {
        private readonly MovieBusiness _movieBusiness;

        public MovieController(MovieRankerContext context)
        {
            _movieBusiness = new MovieBusiness(context);
        }

        //GET: api/Movie
        [HttpGet]
        [ProducesResponseType(typeof(List<MovieDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<MovieDto>? res = await _movieBusiness.FindAll();
                return res != null ? Ok(res) : NotFound();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        //GET: api/Movie/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(MovieDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                MovieDto? res = await _movieBusiness.FindOne(id);
                return res != null ? Ok(res) : NotFound();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Movie with ID {id} not found.");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        //POST: api/Movie/{id}
        [HttpPost]
        [ProducesResponseType(typeof(MovieDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles= "Admin, Contributor")]
        public async Task<IActionResult> Create([FromBody] PostMovieDto movieDto)
        {
            if (movieDto == null)
            {
                return BadRequest("Movie data is required.");
            }

            try
            {
                // Call the business logic layer to add the movie
                MovieDto? createdMovie = await _movieBusiness.AddOne(movieDto);

                // Return a 201 Created response with the created movie DTO
                return CreatedAtAction(nameof(GetById), new { id = createdMovie.Id }, createdMovie);
            }
            catch (Exception e)
            {
                // Log the exception message if needed
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // PUT: api/movies/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(MovieDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin, Contributor")]
        public async Task<IActionResult> Update(int id, [FromBody] PutMovieDto movieDto)
        {
            if (movieDto == null)
            {
                return BadRequest("MovieDto cannot be null.");
            }

            try
            {
                MovieDto updatedMovie = await _movieBusiness.UpdateOne(id, movieDto);
                return Ok(updatedMovie);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Movie with ID {id} not found.");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // DELETE: api/movies/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _movieBusiness.DeleteOne(id);
                return NoContent(); // HTTP 204 No Content
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Movie with ID {id} not found.");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // PUT: api/movies/{id}/rate
        [HttpPut("{id}/rate")]
        [ProducesResponseType(typeof(MovieDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Rate(int id, [FromBody] int rating)
        {
            if (rating < 1 || rating > 5)
            {
                return BadRequest("Rating must be between 1 and 5.");
            }

            try
            {
                MovieDto updatedMovie = await _movieBusiness.RateOne(id, rating);
                return Ok(updatedMovie);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Movie with ID {id} not found.");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
