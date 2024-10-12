using Microsoft.EntityFrameworkCore;
using MovieRanker.Models;
using static MovieRanker.DTO.PersonDTO;

namespace MovieRanker.Business
{
    public class PersonBusiness
    {
        private readonly MovieRankerContext _dbContext;

        public PersonBusiness(MovieRankerContext context)
        {
            _dbContext = context;
        }

        public async Task<List<PersonDto>> FindAll()
        {
            try
            {
                return await _dbContext.Persons
                .Select(p => new PersonDto
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                })
                .ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<PersonDto?> FindOne(int id)
        {
            try
            {
                Person? personEntity = await _dbContext.Persons.FindAsync(id);

                if (personEntity == null)
                {
                    throw new KeyNotFoundException($"Person with ID {id} not found.");
                }

                return new PersonDto
                {
                    Id = personEntity.Id,
                    FirstName = personEntity.FirstName,
                    LastName = personEntity.LastName,
                };
            }
            catch
            {
                throw;
            }
        }

        public async Task<PersonDto> AddOne(PostPersonDto personDto)
        {
            try
            {
                // Target Person
                Person newPerson = new Person
                {
                    FirstName = personDto.FirstName,
                    LastName = personDto.LastName,
                };
                _dbContext.Persons.Add(newPerson);
                await _dbContext.SaveChangesAsync();

                //return the new Person DTO
                return new PersonDto
                {
                    Id = newPerson.Id,
                    FirstName = newPerson.FirstName,
                    LastName = newPerson.LastName,
                };
            }
            catch
            {
                throw;
            }


        }

        public async Task<PersonDto> UpdateOne(int id, PutPersonDto personDto)
        {
            try
            {
                //Target Person
                Person? existingPerson = await _dbContext.Persons.FirstOrDefaultAsync(p => p.Id == id) ?? throw new KeyNotFoundException($"Person with ID {id} not found.");
                existingPerson.FirstName = personDto.FirstName;
                existingPerson.LastName = personDto.LastName;
                
                await _dbContext.SaveChangesAsync();

                //return the updated Person DTO
                return new PersonDto
                {
                    Id = existingPerson.Id,
                    FirstName = existingPerson.FirstName,
                    LastName = existingPerson.LastName,
                };
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteOne(int id)
        {
            try
            {
                Person? existingPerson = await _dbContext.Persons.FindAsync(id) ?? throw new KeyNotFoundException($"Person with ID {id} not found.");

                existingPerson.MoviesNavigation.Clear();
                _dbContext.Persons.Remove(existingPerson);

                // Save changes
                await _dbContext.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
