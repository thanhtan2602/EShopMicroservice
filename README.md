# EShopMicroservice
## Common techs:
* .NET 8 & C# 12.
* Data validation: Fluent Validation
* Deloyment: Docker, docker compose
## Catalog Api Microservice:
* Architecture: Vertical Slice Architecture.
* Libraties:
  *  Mediator: for CQRS pattern
  *  Marten: working with DocumentDB on PostgreSQL
  *  Carter: defining Minimal APIs
  *  Mapster: DTO mapping hight performance
  *  HealthChecks: Health check api, db, redis
* Design Patterns: CQRS, Mediator, Minimal APIs
* RESTful API pattern: Minimal APIs
* Database: Postgres
## Basket Api Microservice:
* Architecture: Vertical Slice Architecture.
* Database: Postgres
* Cache: Redis
## Discount Grpc:
* Architrecture: N-Layer architecture
* Database: SQLite Database
* Patterns: gRPC
## Ordering API Service
* Architecture: Clean Architectur, DDD (Tactital)
* Database: SQL Server
* Patterns: CQRS (Logical CQRS), Mediator, SOLID
  * Database patterns/priciple:
    * Event Sourcing Pattern, Sequence Pattern
    * Eventual Consistency Principle
## Ports:
* Catalog:
  * Local Env: 5000-5050
  * Docker Env: 6000-6060
  * Docker Inside: 8080-8081
* Basket:
  * Local Env: 5001-5051
  * Docker Env: 6001-6061
  * Docker Inside: 8080-8081
* Disconut:
  * Local Env: 5002-5052
  * Docker Env: 6002-6062
  * Docker Inside: 8080-8081
* Ordering:
  * Local Env: 5003-5053
  * Docker Env: 6003-6063
  * Docker Inside: 

![315259850-efe5e688-67f2-4ddd-af37-d9d3658aede4](https://github.com/user-attachments/assets/6c4cc076-802b-4f7f-a08b-5de00a28b596)

