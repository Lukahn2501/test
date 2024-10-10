CREATE TABLE Persons (
    id INT PRIMARY KEY IDENTITY(1,1),
    first_name NVARCHAR(100),
    last_name NVARCHAR(100)
);

CREATE TABLE Movies (
    id INT PRIMARY KEY IDENTITY(1,1),
    title NVARCHAR(255),
    duration INT, -- Duration in minutes
    genre NVARCHAR(50),
    year INT,
    synopsis NVARCHAR(MAX)
);

-- (many-to-many)
CREATE TABLE MovieDirectors (
    movie_id INT,
    director_id INT,
    PRIMARY KEY (movie_id, director_id),
    FOREIGN KEY (movie_id) REFERENCES Movies(id) ON DELETE CASCADE,
    FOREIGN KEY (director_id) REFERENCES Persons(id) ON DELETE CASCADE
);

-- (many-to-many)
CREATE TABLE MovieActors (
    movie_id INT,
    actor_id INT,
    PRIMARY KEY (movie_id, actor_id),
    FOREIGN KEY (movie_id) REFERENCES Movies(id) ON DELETE CASCADE,
    FOREIGN KEY (actor_id) REFERENCES Persons(id) ON DELETE CASCADE
);