# ADR_1 - 10/10/2024 - API Structure

_Status_: Accepted
_Context_: What structure should the API take, given the small scale of the project. Should we decrease the abstraction level at which we operate?
_Decision_: No. Since this project is also mean to showcase a 

_Consequences_: The API will use a standard three layers architecture.

- Controller: implementing endpoints and high-level logic
- Business layer: implementing low-level logic
- DTOs: Data Tranfert Object, representing entities to be manipulated. These don't necessarily reflect the database entities and most importantly don't expose the database model to method that don't need to know it.

Note: in our use case, since the scope is limited, they do reflect the database entities.