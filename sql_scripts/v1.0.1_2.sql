DECLARE @minRating INT = 1;
DECLARE @maxRating INT = 5;
DECLARE @minCount INT = 100;
DECLARE @maxCount INT = 200;

UPDATE Movies
SET rating = FLOOR(((@maxRating - @minRating + 1) * RAND(CHECKSUM(NEWID()))) + @minRating),
    rating_count = FLOOR(((@maxCount - @minCount + 1) * RAND(CHECKSUM(NEWID()))) + @minCount)
WHERE id BETWEEN 1 AND 10;
