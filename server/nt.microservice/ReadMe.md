# Nt.Microservice

This project demonstrate usage of Microservice architecture for implementing backend for Nt application. Key points in the implementation include

- API Gateway has been implemented with [Ocelot](https://ocelot.readthedocs.io/en/latest/) library.

Implementation details of different services are as follows.

| Service | Role            | Database   | ORM              | Model Validation  | Entity Mapping | Unit Test | Mock        | Logger  |
| ------- | --------------- | ---------- | ---------------- | ----------------- | -------------- | --------- | ----------- | ------- |
| Auth    | Authentication  | ![PostgreSQL](https://img.shields.io/badge/PostgreSQL-316192?style=flat&logo=postgresql&logoColor=white) Postgres   | ![Dapper ORM](https://img.shields.io/badge/Dapper-512BD4?style=flat&logo=.net&logoColor=white) Dapper           | Fluent Validation | Mapster        | NUnit     | NSubstitute | NLog    |
| User    | User Management |![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=plastic&logo=microsoft-sql-server&logoColor=white) Sql Server | ![Entity Framework](https://img.shields.io/badge/Entity%20Framework-512BD4?style=flat&logo=.net&logoColor=white)  Entity Framework | Data Annotations  | Automapper     | XUnit     | Moq         | SeriLog |
| Movies  | Movie Meta info | ![MongoDB](https://img.shields.io/badge/MongoDB-47A248?style=flat&logo=mongodb&logoColor=white)  MongoDb    | ![MongoDB Entities](https://img.shields.io/badge/MongoDB%20Entities-47A248?style=flat&logo=mongodb&logoColor=white) MongoDb.Entities | Json.Net  | ValueInjector     | MsTest    | FakeItEasy  | Microsoft.Extensions.Logging     |
| Reviews | User Reviews    | TBD        | TBD              | Fluent Validation | Automapper     | MsTest    | Rhinomock   | TBD     |
| Orchestrator | Orchestration between services    | None        | None              | None | None     | MsTest    | None   | TBD     |


Messaging Systems involved

 - RabbitMq


Monitoring Systems

 - Prometheus + Grafana
 - Portainer
