# API

## Identity

### POST /login (useCookies: true)
```json
{ //Admin
  "email": "admin@admin.admin",
  "password": "adminP@ssw0rd"
}
```
```json
{ //Contributor
  "email": "cont@cont.cont",
  "password": "contP@ssw0rd"
}
```
```json
{ //Spectator
  "email": "spec@spec.spec",
  "password": "specP@ssw0rd"
}
```

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
    "title": "Jurassic Park",
    "duration": 127,
    "genre": "Adventure",
    "year": 1993,
    "synopsis": "During a preview tour, a theme park suffers a major power breakdown that allows its cloned dinosaur exhibits to run amok.",
    "directorsIds": [1],
    "actorsIds": [1,7]
}
```

### PUT /rate
```json
  3 //from 1 to 5
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