# Nt.Microservice

This application is built using a microservices architecture to ensure modularity, scalability, and maintainability across service boundaries. Each service is independently deployable and communicates primarily over REST APIs, with asynchronous messaging used where strong decoupling is required.

Key points in the implementation include

## 🔐 Authentication Layer – Auth Service
- Implements token-based authentication and user credential validation.
- Utilizes Dapper as a lightweight micro-ORM for fine-grained SQL control.
- Backed by a PostgreSQL datastore optimized for transactional consistency.
- Publishes domain events to RabbitMQ for decoupled service orchestration.

## 🧍‍♂️ User Management – User Service
- Manages user metadata and profile data.
- Employs Entity Framework on SQL Server for relational persistence.
- Subscribes to AuthService events via RabbitMQ, enabling eventual consistency and decoupled workflows.

## 🔗 Identity Composition – UserIdentity Aggregator
- A cross-service orchestration layer abstracting Auth and User services.
- Serves as a Backend For Frontend (BFF), exposing a canonical identity model.
- Centralizes validation logic and shields clients from domain fragmentation.
- Uses Consul Service discovery for discovery of individual services

### Movie Service
- Implements movie catalog and metadata management.
- Built using MongoDb.Entities ODM over MongoDB for flexible schema design and nested document queries.

### Review Service
- Currently a placeholder microservice.
- Positioned to evolve into an event-sourced or CQRS-enabled bounded context.

## 🔄 Inter-Service Communication Model

| Pattern      | Technology | Use Case                      |
|--------------|------------|-------------------------------|
| Synchronous  | REST (HTTP)| Request-response APIs         |
| Asynchronous | RabbitMQ   | Decoupled event-based messaging |


**System Architecture**
   
   ![NT System Design](https://github.com/anuviswan/nt/blob/master/server/nt.microservice/SystemDesign.jpg)

Implementation details of different services are as follows.

|                   | Authentication  | User | Movies  | Reviews | User Identity Aggregator                   |
|-------------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|--------------------------------------|---------------------------------|
| **Role**          | Authentication                                                                                                                                                       | User Management                                                                                                                                                                                                                | Movie Meta info                                                                                                                                                                                                                                       | User Reviews                        | Aggregation between services |
| **Database**      | ![PostgreSQL](https://img.shields.io/badge/PostgreSQL-316192?style=flat&logo=postgresql&logoColor=white) <br> Postgres                                                | ![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=plastic&logo=microsoft-sql-server&logoColor=white) <br> Sql Server                                                                                        | ![MongoDB](https://img.shields.io/badge/MongoDB-47A248?style=flat&logo=mongodb&logoColor=white) <br> MongoDb                                                                                                                                         | TBD                                  | None                            |
| **ORM**           | ![Dapper ORM](https://img.shields.io/badge/Dapper-512BD4?style=flat&logo=.net&logoColor=white) <br> Dapper                                                            | ![Entity Framework](https://img.shields.io/badge/Entity%20Framework-512BD4?style=flat&logo=.net&logoColor=white) <br> Entity Framework                                                                                          | ![MongoDB Entities](https://img.shields.io/badge/MongoDB%20Entities-47A248?style=flat&logo=mongodb&logoColor=white) <br> MongoDb.Entities                                                                                                           | TBD                                  | None                            |
| **Model Validation** | ![Fluent Validation](https://img.shields.io/badge/Fluent%20Validation-512BD4?style=flat&logo=.net&logoColor=white) <br> Fluent Validation                             | ![.NET Data Annotations](https://img.shields.io/badge/.NET%20Data%20Annotations-512BD4?style=flat&logo=.net&logoColor=white) <br> Data Annotations                                                                             | ![JSON.NET](https://img.shields.io/badge/JSON.NET-7C7C7C?style=flat&logo=json&logoColor=white) <br> Json.Net                                                                                                                                         | Fluent Validation                    | None                            |
| **Entity Mapping** | ![Mapster](https://img.shields.io/badge/Mapster-4B8BBE?style=flat&logo=.net&logoColor=white) <br> Mapster                                                            | ![AutoMapper](https://img.shields.io/badge/AutoMapper-007ACC?style=flat&logo=.net&logoColor=white) <br> Automapper                                                                                                              | ![ValueInjecter](https://img.shields.io/badge/ValueInjecter-4B8BBE?style=flat&logo=.net&logoColor=white) <br> ValueInjector                                                                                                                         | Automapper                           | None                            |
| **Unit Test**      | ![NUnit](https://img.shields.io/badge/NUnit-00BFFF?style=flat&logo=nunit&logoColor=white) <br> NUnit                                                                 | ![xUnit](https://img.shields.io/badge/xUnit-00BFFF?style=flat&logo=xunit&logoColor=white) <br> XUnit                                                                                                                                               | ![MSTest](https://img.shields.io/badge/MSTest-00BFFF?style=flat&logo=visual-studio&logoColor=white) <br> MsTest                                                                                                                                      | MsTest                               | MsTest                          |
| **Mock**           | ![NSubstitute](https://img.shields.io/badge/NSubstitute-4B8BBE?style=flat&logo=.net&logoColor=white) <br> NSubstitute                                                | ![Moq](https://img.shields.io/badge/Moq-4B8BBE?style=flat&logo=.net&logoColor=white) <br> Moq                                                                                                                                  | ![FakeItEasy](https://img.shields.io/badge/FakeItEasy-4B8BBE?style=flat&logo=.net&logoColor=white) <br> FakeItEasy                                                                                                                                   | Rhinomock                            | None                            |
| **Logger**         | ![NLog](https://img.shields.io/badge/NLog-4B8BBE?style=flat&logo=.net&logoColor=white) <br> NLog                                                                     | ![Serilog](https://img.shields.io/badge/Serilog-4B8BBE?style=flat&logo=.net&logoColor=white) <br> SeriLog                                                                                                                         | ![Microsoft.Extensions.Logging](https://img.shields.io/badge/Microsoft.Extensions.Logging-512BD4?style=flat&logo=.net&logoColor=white) <br> Microsoft.Extensions.Logging                                                                            | TBD                                  | TBD                             |
| **Load Balancer**         | ![NGINX](https://img.shields.io/badge/nginx-009639?style=flat&logo=nginx&logoColor=white) <br> nginx |  | | | |
| **Message Queue**         | ![RabbitMQ](https://img.shields.io/badge/RabbitMQ-FF6600?style=flat&logo=rabbitmq&logoColor=white) <br> RabbitMq | ![RabbitMQ](https://img.shields.io/badge/RabbitMQ-FF6600?style=flat&logo=rabbitmq&logoColor=white) <br> RabbitMq | | | |



**Monitoring Systems**

 - Prometheus + Grafana
 - Portainer


   
