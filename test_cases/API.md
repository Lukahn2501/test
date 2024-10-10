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
    "directors": [
        {
            "id": 1,
            "firstName": "Steven",
            "lastName": "Spielberg"
        }
    ],
    "actors": [
        {
            "id": 10,  
            "firstName": "Tom",
            "lastName": "Hanks"
        },
        {
            "id": 7,  
            "firstName": "Samuel",
            "lastName": "Jackson"
        }
    ]
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
    "directors": [
        {
            "id": 99,
            "firstName": "Quentin",
            "lastName": "Tarantino"
        }
    ],
    "actors": [
        {
            "id": 200,
            "firstName": "Bruce",
            "lastName": "Willis"
        }
    ]
}
```

### PUT
Valid body
```json
{
    "title": "Jurassic Park",
    "duration": 127,
    "genre": "Adventure",
    "year": 1993,
    "synopsis": "During a preview tour, a theme park suffers a major power breakdown that allows its cloned dinosaur exhibits to run amok.",
    "directors": [
        {
            "id": 1,
            "firstName": "Steven",
            "lastName": "Spielberg"
        }
    ],
    "actors": [
        {
            "id": 1,
            "firstName": "Steven",
            "lastName": "Spielberg"
        },
        {
            "id": 7,  
            "firstName": "Samuel",
            "lastName": "Jackson"
        }
    ]
}
```

## Persons

### POST
{
  "id": 25,
  "firstName": "Kevin",
  "lastName": "Roy"
}

### PUT
{
  "id": 25,
  "firstName": "Kendrick",
  "lastName": "Lamar"
}