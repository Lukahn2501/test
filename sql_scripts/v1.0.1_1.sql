ALTER TABLE Movies
ADD rating FLOAT CHECK (rating BETWEEN 1 AND 5),
    rating_count INT DEFAULT 0;