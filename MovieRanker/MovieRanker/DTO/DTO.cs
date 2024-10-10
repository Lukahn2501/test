namespace MovieRanker.DTO
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
    }

    public class PersonDto
    {
        public required int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
    }
}