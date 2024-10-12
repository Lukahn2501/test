namespace MovieRanker.DTO
{
    public class PersonDTO
    {
        public class PersonDto
        {
            public required int Id { get; set; }
            public required string FirstName { get; set; }
            public required string LastName { get; set; }
        }

        public class PostPersonDto
        {
            public required string FirstName { get; set; }
            public required string LastName { get; set; }
        }

        public class PutPersonDto
        {
            public required int Id { get; set; }
            public required string FirstName { get; set; }
            public required string LastName { get; set; }
        }
    }
}
