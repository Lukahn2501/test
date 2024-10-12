using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieRanker.Migrations
{
    /// <inheritdoc />
    public partial class SeedDatabaseMoviesPersons : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sqlScript = @"
                -- Insert persons (both actors and directors)
                INSERT INTO Persons (first_name, last_name) VALUES 
                ('Steven', 'Spielberg'), 
                ('Christopher', 'Nolan'), 
                ('Quentin', 'Tarantino'), 
                ('Martin', 'Scorsese'), 
                ('James', 'Cameron'), 
                ('Leonardo', 'DiCaprio'), 
                ('Brad', 'Pitt'), 
                ('Samuel', 'Jackson'), 
                ('Tom', 'Hanks'), 
                ('Robert', 'Downey Jr.'), 
                ('Scarlett', 'Johansson'), 
                ('Morgan', 'Freeman'), 
                ('Matt', 'Damon'), 
                ('Natalie', 'Portman'), 
                ('Christian', 'Bale'), 
                ('Emma', 'Stone'), 
                ('Anne', 'Hathaway'), 
                ('Denzel', 'Washington'), 
                ('Keanu', 'Reeves'), 
                ('Meryl', 'Streep'),
                ('John', 'Travolta'),
                ('Kate', 'Winslet'),
                ('Carrie-Anne', 'Moss'),
                ('Matthew', 'McConaughey');

                -- Insert movies
                INSERT INTO Movies (title, duration, genre, year, synopsis) VALUES
                ('Inception', 148, 'Sci-Fi', 2010, 'A skilled thief leads a team to infiltrate dreams to steal secrets.'),
                ('Pulp Fiction', 154, 'Crime', 1994, 'The lives of two mob hitmen, a boxer, and others intertwine in crime.'),
                ('The Dark Knight', 152, 'Action', 2008, 'Batman must face his greatest enemy, the Joker, to save Gotham City.'),
                ('Forrest Gump', 142, 'Drama', 1994, 'The story of a man with a low IQ who accomplishes great things in life.'),
                ('The Matrix', 136, 'Sci-Fi', 1999, 'A hacker learns about the true nature of his reality and his role in the war.'),
                ('The Godfather', 175, 'Crime', 1972, 'The aging patriarch of an organized crime dynasty transfers control.'),
                ('Titanic', 195, 'Romance', 1997, 'A young couple fall in love aboard the ill-fated R.M.S. Titanic.'),
                ('Avengers: Endgame', 181, 'Action', 2019, 'The Avengers assemble once more to reverse the actions of Thanos.'),
                ('Interstellar', 169, 'Sci-Fi', 2014, 'A group of explorers travel through a wormhole in space in an attempt to ensure humanity''s survival.'),
                ('The Departed', 151, 'Crime', 2006, 'An undercover cop and a mole in the police try to identify each other while infiltrating an Irish gang in Boston.');

                -- Insert directors for movies in MovieDirectors table
                INSERT INTO MovieDirectors (movie_id, director_id) VALUES
                ((SELECT id FROM Movies WHERE title = 'Inception'), (SELECT id FROM Persons WHERE last_name = 'Nolan')),
                ((SELECT id FROM Movies WHERE title = 'Pulp Fiction'), (SELECT id FROM Persons WHERE last_name = 'Tarantino')),
                ((SELECT id FROM Movies WHERE title = 'The Dark Knight'), (SELECT id FROM Persons WHERE last_name = 'Nolan')),
                ((SELECT id FROM Movies WHERE title = 'Forrest Gump'), (SELECT id FROM Persons WHERE last_name = 'Spielberg')),
                ((SELECT id FROM Movies WHERE title = 'The Matrix'), (SELECT id FROM Persons WHERE last_name = 'Cameron')),
                ((SELECT id FROM Movies WHERE title = 'The Godfather'), (SELECT id FROM Persons WHERE last_name = 'Scorsese')),
                ((SELECT id FROM Movies WHERE title = 'Titanic'), (SELECT id FROM Persons WHERE last_name = 'Cameron')),
                ((SELECT id FROM Movies WHERE title = 'Avengers: Endgame'), (SELECT id FROM Persons WHERE last_name = 'Tarantino')),
                ((SELECT id FROM Movies WHERE title = 'Interstellar'), (SELECT id FROM Persons WHERE last_name = 'Nolan')),
                ((SELECT id FROM Movies WHERE title = 'The Departed'), (SELECT id FROM Persons WHERE last_name = 'Scorsese'));

                -- Insert actors for movies in MovieActors table
                INSERT INTO MovieActors (movie_id, actor_id) VALUES
                ((SELECT id FROM Movies WHERE title = 'Inception'), (SELECT id FROM Persons WHERE last_name = 'DiCaprio')),
                ((SELECT id FROM Movies WHERE title = 'Inception'), (SELECT id FROM Persons WHERE last_name = 'Hathaway')),
                ((SELECT id FROM Movies WHERE title = 'Pulp Fiction'), (SELECT id FROM Persons WHERE last_name = 'Jackson')),
                ((SELECT id FROM Movies WHERE title = 'Pulp Fiction'), (SELECT id FROM Persons WHERE last_name = 'Travolta')),
                ((SELECT id FROM Movies WHERE title = 'The Dark Knight'), (SELECT id FROM Persons WHERE last_name = 'Bale')),
                ((SELECT id FROM Movies WHERE title = 'The Dark Knight'), (SELECT id FROM Persons WHERE last_name = 'Freeman')),
                ((SELECT id FROM Movies WHERE title = 'Forrest Gump'), (SELECT id FROM Persons WHERE last_name = 'Hanks')),
                ((SELECT id FROM Movies WHERE title = 'The Matrix'), (SELECT id FROM Persons WHERE last_name = 'Reeves')),
                ((SELECT id FROM Movies WHERE title = 'The Matrix'), (SELECT id FROM Persons WHERE last_name = 'Moss')),
                ((SELECT id FROM Movies WHERE title = 'Titanic'), (SELECT id FROM Persons WHERE last_name = 'DiCaprio')),
                ((SELECT id FROM Movies WHERE title = 'Titanic'), (SELECT id FROM Persons WHERE last_name = 'Winslet')),
                ((SELECT id FROM Movies WHERE title = 'Avengers: Endgame'), (SELECT id FROM Persons WHERE last_name = 'Downey Jr.')),
                ((SELECT id FROM Movies WHERE title = 'Avengers: Endgame'), (SELECT id FROM Persons WHERE last_name = 'Johansson')),
                ((SELECT id FROM Movies WHERE title = 'Interstellar'), (SELECT id FROM Persons WHERE last_name = 'McConaughey')),
                ((SELECT id FROM Movies WHERE title = 'Interstellar'), (SELECT id FROM Persons WHERE last_name = 'Hathaway')),
                ((SELECT id FROM Movies WHERE title = 'The Departed'), (SELECT id FROM Persons WHERE last_name = 'DiCaprio')),
                ((SELECT id FROM Movies WHERE title = 'The Departed'), (SELECT id FROM Persons WHERE last_name = 'Damon'));
            ";

            migrationBuilder.Sql(sqlScript);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
