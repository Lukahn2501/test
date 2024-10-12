using static MovieRanker.DTO.PersonDTO;

namespace MovieRanker.DTO
{
    public class MovieDTO
    {
        public class MovieDto
        {
            public int Id { get; set; }
            public required string Title { get; set; }
            public required int Duration { get; set; }
            public required string Genre { get; set; }
            public required int Year { get; set; }
            public string? Synopsis { get; set; }
            public List<PersonDto>? Directors { get; set; }
            public List<PersonDto>? Actors { get; set; }
            public double? Rating { get; set; }
            public int? RatingCount { get; set; }
        }

        public class PostMovieDto()
        {
            public required string Title { get; set; }
            public required int Duration { get; set; }
            public required string Genre { get; set; }
            public required int Year { get; set; }
            public string? Synopsis { get; set; }
            public List<int>? DirectorsIds { get; set; }
            public List<int>? ActorsIds { get; set; }
        }

        public class PutMovieDto()
        {
            public required int Id { get; set; }
            public required string Title { get; set; }
            public required int Duration { get; set; }
            public required string Genre { get; set; }
            public required int Year { get; set; }
            public string? Synopsis { get; set; }
            public List<int>? DirectorsIds { get; set; }
            public List<int>? ActorsIds { get; set; }
        }
    }
}
