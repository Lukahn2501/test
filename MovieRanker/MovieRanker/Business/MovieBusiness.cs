using Microsoft.EntityFrameworkCore;
using MovieRanker.Models;
using NuGet.Packaging;
using static MovieRanker.DTO.MovieDTO;
using static MovieRanker.DTO.PersonDTO;

namespace MovieRanker.Business
{
    public class MovieBusiness
    {
        private readonly MovieRankerContext _dbContext;

        public MovieBusiness(MovieRankerContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<MovieDto>?> FindAll()
        {
            try
            {
                return await _dbContext.Movies
                    .Include(movie => movie.Directors)
                    .Include(movie => movie.Actors)
                    .Select(movie => new MovieDto
                    {
                        Id = movie.Id,
                        Title = movie.Title,
                        Duration = movie.Duration,
                        Genre = movie.Genre,
                        Year = movie.Year,
                        Synopsis = movie.Synopsis,
                        Directors = movie.Directors.Select(d => new PersonDto
                        {
                            Id = d.Id,
                            FirstName = d.FirstName,
                            LastName = d.LastName
                        }).ToList(),
                        Actors = movie.Actors.Select(a => new PersonDto
                        {
                            Id = a.Id,
                            FirstName = a.FirstName,
                            LastName = a.LastName
                        }).ToList()
                    })
                    .ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<MovieDto?> FindOne(int id)
        {
            try
            {
                Movie? movieEntity = await _dbContext.Movies
                    .Include(movie => movie.Directors)
                    .Include(movie => movie.Actors)
                    .FirstOrDefaultAsync(movie => movie.Id == id);

                if (movieEntity == null)
                {
                    throw new KeyNotFoundException($"Movie with ID {id} not found.");
                }

                return new MovieDto
                {
                    Id = movieEntity.Id,
                    Title = movieEntity.Title,
                    Duration = movieEntity.Duration,
                    Genre = movieEntity.Genre,
                    Year = movieEntity.Year,
                    Synopsis = movieEntity.Synopsis,
                    Directors = movieEntity.Directors.Select(d => new PersonDto
                    {
                        Id = d.Id,
                        FirstName = d.FirstName,
                        LastName = d.LastName
                    }).ToList(),
                    Actors = movieEntity.Actors.Select(a => new PersonDto
                    {
                        Id = a.Id,
                        FirstName = a.FirstName,
                        LastName = a.LastName
                    }).ToList()
                };
            }
            catch
            {
                throw;
            }
        }

        public async Task<MovieDto> AddOne(PostMovieDto movieDto)
        {
            try
            {
                // Target Movie
                Movie newMovie = new Movie
                {
                    Title = movieDto.Title,
                    Duration = movieDto.Duration,
                    Genre = movieDto.Genre,
                    Year = movieDto.Year,
                    Synopsis = movieDto.Synopsis ?? "",
                    Directors = new List<Person>(),
                    Actors = new List<Person>()
                };


                // Checking data integrity and adding directors and actors to the junction tables


                if (movieDto.DirectorsIds != null)
                {
                    List<Person>? directors = await _dbContext.Persons
                        .Where(p => movieDto.DirectorsIds.Contains(p.Id))
                        .ToListAsync();

                    if (directors.Count != movieDto.DirectorsIds!.Count)
                    {
                        throw new Exception("One or more directors do not exist.");
                    }

                    newMovie.Directors.AddRange(directors);
                }

                if (movieDto.ActorsIds != null)
                {
                    List<Person>? actors = await _dbContext.Persons
                        .Where(p => movieDto.ActorsIds.Contains(p.Id))
                        .ToListAsync();

                    if (actors.Count != movieDto.ActorsIds!.Count)
                    {
                        throw new Exception("One or more actors do not exist.");
                    }

                    newMovie.Actors.AddRange(actors);
                }

                _dbContext.Movies.Add(newMovie);
                await _dbContext.SaveChangesAsync();

                // Return the created movie DTO
                return new MovieDto
                {
                    Id = newMovie.Id,
                    Title = newMovie.Title,
                    Duration = newMovie.Duration,
                    Genre = newMovie.Genre,
                    Year = newMovie.Year,
                    Synopsis = newMovie.Synopsis,
                    Directors = newMovie.Directors.Select(d => new PersonDto
                    {
                        Id = d.Id,
                        FirstName = d.FirstName,
                        LastName = d.LastName
                    }).ToList(),
                    Actors = newMovie.Actors.Select(a => new PersonDto
                    {
                        Id = a.Id,
                        FirstName = a.FirstName,
                        LastName = a.LastName
                    }).ToList()
                };
            }
            catch
            {
                throw;
            }
        }

        public async Task<MovieDto> UpdateOne(int id, PutMovieDto movieDto)
        {
            try
            {
                // Find the existing movie by ID
                Movie? existingMovie = await _dbContext.Movies
                    .Include(m => m.Directors)
                    .Include(m => m.Actors)
                    .FirstOrDefaultAsync(m => m.Id == id) ?? throw new KeyNotFoundException($"Movie with ID {id} not found.");

                // Update movie properties
                existingMovie.Title = movieDto.Title;
                existingMovie.Duration = movieDto.Duration;
                existingMovie.Genre = movieDto.Genre;
                existingMovie.Year = movieDto.Year;
                existingMovie.Synopsis = movieDto.Synopsis ?? "";

                // Clear existing directors and actors (junction table)
                existingMovie.Directors.Clear();
                existingMovie.Actors.Clear();

                // Checking data integrity and replacing the existing directors and actors

                if (movieDto.DirectorsIds != null)
                {
                    List<Person>? directors = await _dbContext.Persons
                        .Where(p => movieDto.DirectorsIds.Contains(p.Id))
                        .ToListAsync();

                    if (directors.Count != movieDto.DirectorsIds!.Count)
                    {
                        throw new Exception("One or more directors do not exist.");
                    }

                    existingMovie.Directors = directors;
                }

                if (movieDto.ActorsIds != null)
                {
                    List<Person>? actors = await _dbContext.Persons
                        .Where(p => movieDto.ActorsIds.Contains(p.Id))
                        .ToListAsync();

                    if (actors.Count != movieDto.ActorsIds!.Count)
                    {
                        throw new Exception("One or more actors do not exist.");
                    }

                    existingMovie.Actors = actors;
                }

                // Save changes to the database
                await _dbContext.SaveChangesAsync();

                // Return the updated movie DTO
                return new MovieDto
                {
                    Id = existingMovie.Id,
                    Title = existingMovie.Title,
                    Duration = existingMovie.Duration,
                    Genre = existingMovie.Genre,
                    Year = existingMovie.Year,
                    Synopsis = existingMovie.Synopsis,
                    Directors = existingMovie.Directors.Select(d => new PersonDto
                    {
                        Id = d.Id,
                        FirstName = d.FirstName,
                        LastName = d.LastName
                    }).ToList(),
                    Actors = existingMovie.Actors.Select(a => new PersonDto
                    {
                        Id = a.Id,
                        FirstName = a.FirstName,
                        LastName = a.LastName
                    }).ToList()
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
                // Find the movie by ID
                Movie? existingMovie = await _dbContext.Movies
                    .Include(m => m.Directors)
                    .Include(m => m.Actors)
                    .FirstOrDefaultAsync(m => m.Id == id) ?? throw new KeyNotFoundException($"Movie with ID {id} not found."); ;

                // Clear existing directors and actors (junction tables)
                existingMovie.Directors.Clear();
                existingMovie.Actors.Clear();

                _dbContext.Movies.Remove(existingMovie);

                // Save changes to the database
                await _dbContext.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<MovieDto> RateOne(int id, int rating)
        {
            try
            {
                if (rating < 1 || rating > 5)
                {
                    throw new Exception("Rating must be between 1 and 5.");
                }

                Movie? movieEntity = await _dbContext.Movies
                    .Include(movie => movie.Directors)
                    .Include(movie => movie.Actors)
                    .FirstOrDefaultAsync(movie => movie.Id == id);

                if (movieEntity == null)
                {
                    throw new KeyNotFoundException($"Movie with ID {id} not found.");
                }

                double currentRating = movieEntity.Rating ?? 0;
                int currentRatingCount = movieEntity.RatingCount ?? 0;

                movieEntity.Rating = (currentRating * currentRatingCount + rating) / (currentRatingCount + 1);
                movieEntity.RatingCount = currentRatingCount + 1;

                await _dbContext.SaveChangesAsync();

                return new MovieDto
                {
                    Id = movieEntity.Id,
                    Title = movieEntity.Title,
                    Duration = movieEntity.Duration,
                    Genre = movieEntity.Genre,
                    Year = movieEntity.Year,
                    Synopsis = movieEntity.Synopsis,
                    Directors = movieEntity.Directors.Select(d => new PersonDto
                    {
                        Id = d.Id,
                        FirstName = d.FirstName,
                        LastName = d.LastName
                    }).ToList(),
                    Actors = movieEntity.Actors.Select(a => new PersonDto
                    {
                        Id = a.Id,
                        FirstName = a.FirstName,
                        LastName = a.LastName
                    }).ToList(),
                    Rating = movieEntity.Rating,
                    RatingCount = movieEntity.RatingCount
                };
            }
            catch
            {
                throw;
            }
        }
    }
}
