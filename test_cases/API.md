# API

## Movies

### POST

Valid Body
```json
{
    "title": "Jurassic Park",
    "duration": 127,
    "genre": "Adventure",
    "year": 1993,
    "synopsis": "During a preview tour, a theme park suffers a major power breakdown that allows its cloned dinosaur exhibits to run amok.",
    "directorsIds": [1],
    "actorsIds": [10, 7]
}
```

Invalid Body
```json
{
    "title": "Pulp Fiction",
    "duration": 154,
    "genre": "Crime",
    "year": 1994,
    "synopsis": "The lives of two mob hitmen, a boxer, a gangster's wife, and a pair of diner bandits intertwine in four tales of violence and redemption.",
    "directorsIds": [99],
    "actorsIds": [200]
}
```

### PUT
Valid body
```json
{
    "id": 13, //Paste actual id here
    "title": "Jurassic Park",
    "duration": 127,
    "genre": "Adventure",
    "year": 1993,
    "synopsis": "During a preview tour, a theme park suffers a major power breakdown that allows its cloned dinosaur exhibits to run amok.",
    "directorsIds": [1],
    "actorsIds": [1,7]
}
```

## Persons

### POST
```json
{
  "firstName": "Kevin",
  "lastName": "Roy"
}
```

### PUT
```json
{
  "id": "", // Put actual id here
  "firstName": "Kendrick",
  "lastName": "Lamar"
}
```

## Roles

### PUT
```json
{
  "username": "cont@cont.cont",
  "role": "Admin"
}
```