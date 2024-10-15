using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieRanker.Business;
using MovieRanker.Models;
using static MovieRanker.DTO.PersonDTO;

namespace MovieRanker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PersonController : ControllerBase
    {
        private readonly PersonBusiness _personBusiness;

        public PersonController(MovieRankerContext context)
        {
            _personBusiness = new PersonBusiness(context);
        }

        //GET: api/Person
        [HttpGet]
        [ProducesResponseType(typeof(List<PersonDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<PersonDto>? res = await _personBusiness.FindAll();
                return res != null ? Ok(res) : NotFound();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        //GET: api/Person/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PersonDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                PersonDto? res = await _personBusiness.FindOne(id);
                return res != null ? Ok(res) : NotFound();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Person with ID {id} not found.");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        //POST: api/Person/{id}
        [HttpPost]
        [ProducesResponseType(typeof(PersonDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = "Admin, Contributor")]
        public async Task<IActionResult> Create([FromBody] PostPersonDto personDto)
        {
            if (personDto == null)
            {
                return BadRequest("Person data is required.");
            }

            try
            {
                // Call the business logic layer to add the person
                PersonDto? createdPerson = await _personBusiness.AddOne(personDto);

                // Return a 201 Created response with the created person DTO
                return CreatedAtAction(nameof(GetById), new { id = createdPerson.Id }, createdPerson);
            }
            catch (Exception e)
            {
                // Log the exception message if needed
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // PUT: api/Person/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(PersonDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = "Admin, Contributor")]
        public async Task<IActionResult> Update(int id, [FromBody] PutPersonDto personDto)
        {
            if (personDto == null)
            {
                return BadRequest("PersonDto cannot be null.");
            }

            try
            {
                PersonDto updatedPerson = await _personBusiness.UpdateOne(id, personDto);
                return Ok(updatedPerson);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Person with ID {id} not found.");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // DELETE: api/Person/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _personBusiness.DeleteOne(id);
                return NoContent(); // HTTP 204 No Content
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Person with ID {id} not found.");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
