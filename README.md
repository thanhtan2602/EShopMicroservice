# EShopMicroservice
## Big techs:
* .NET 8 & C# 12.
* Docker, docker compose
* Postgres, SQL Server, SQLite
## Install and setups:
  * .NET 8 SDK
  * Tools:
    * Docker: Docker desktop
    * Postgres: pgAdmin4
    * SQLite: DB Browser
    * SQL Server: SQL Server Management
    * Visual Studio, VSCode, Postman
## BuildingBlocks
## ApiGateway
## Microservices
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
## Frontend:
  * ASP.NET Razor, Bootstrap 4
  * Patterns: Factory
  * Libraries: Refit
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
  * Docker Inside: 8080-8081
* Yarp.ApiGateway:
  * Local Env: 5004-5054
  * Docker Env: 6004-6064
  * Docker Inside: 8080-8081
* Frontend:
  * Local Env: 5005-5055
  * Docker Env: 6005-6065
  * Docker Inside: 8080-8081
* Auth API:
  * Local Env: 5006-5056
  * Docker Env: 6006-6066
  * Docker Inside: 8080-8081
