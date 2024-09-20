# Nt.Microservice

This project demonstrate usage of Microservice architecture for implementing backend for Nt application. Key points in the implementation include

- API Gateway has been implemented with [Ocelot](https://ocelot.readthedocs.io/en/latest/) library.

Implementation details of different services are as follows.

| Service | Role            | Database   | ORM              | Model Validation  | Entity Mapping | Unit Test | Mock        | Logger  |
| ------- | --------------- | ---------- | ---------------- | ----------------- | -------------- | --------- | ----------- | ------- |
| Auth    | Authentication  | ![PostgreSQL](https://img.shields.io/badge/PostgreSQL-316192?style=flat&logo=postgresql&logoColor=white) <br> Postgres   | ![Dapper ORM](https://img.shields.io/badge/Dapper-512BD4?style=flat&logo=.net&logoColor=white) <br> Dapper           | ![Fluent Validation](https://img.shields.io/badge/Fluent%20Validation-512BD4?style=flat&logo=.net&logoColor=white) <br> Fluent Validation | ![Mapster](https://img.shields.io/badge/Mapster-4B8BBE?style=flat&logo=.net&logoColor=white) <br> Mapster        | ![NUnit](https://img.shields.io/badge/NUnit-00BFFF?style=flat&logo=nunit&logoColor=white) <br> NUnit     | ![NSubstitute](https://img.shields.io/badge/NSubstitute-4B8BBE?style=flat&logo=.net&logoColor=white) <br> NSubstitute | ![NLog](https://img.shields.io/badge/NLog-4B8BBE?style=flat&logo=.net&logoColor=white) <br> NLog    |
| User    | User Management |![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=plastic&logo=microsoft-sql-server&logoColor=white) <br> Sql Server | ![Entity Framework](https://img.shields.io/badge/Entity%20Framework-512BD4?style=flat&logo=.net&logoColor=white) <br> Entity Framework | ![.NET Data Annotations](https://img.shields.io/badge/.NET%20Data%20Annotations-512BD4?style=flat&logo=.net&logoColor=white) <br> Data Annotations  | ![AutoMapper](https://img.shields.io/badge/AutoMapper-007ACC?style=flat&logo=.net&logoColor=white) <br> Automapper     | ![xUnit](https://img.shields.io/badge/xUnit-00BFFF?style=flat&logo=xunit&logoColor=white) <br> XUnit     | ![Moq](https://img.shields.io/badge/Moq-4B8BBE?style=flat&logo=.net&logoColor=white) <br> Moq         | ![Serilog](https://img.shields.io/badge/Serilog-4B8BBE?style=flat&logo=.net&logoColor=white) <br> SeriLog |
| Movies  | Movie Meta info | ![MongoDB](https://img.shields.io/badge/MongoDB-47A248?style=flat&logo=mongodb&logoColor=white) <br> MongoDb    | ![MongoDB Entities](https://img.shields.io/badge/MongoDB%20Entities-47A248?style=flat&logo=mongodb&logoColor=white) <br> MongoDb.Entities | ![JSON.NET](https://img.shields.io/badge/JSON.NET-7C7C7C?style=flat&logo=json&logoColor=white) <br> Json.Net  | ![ValueInjecter](https://img.shields.io/badge/ValueInjecter-4B8BBE?style=flat&logo=.net&logoColor=white) <br> ValueInjector     | ![MSTest](https://img.shields.io/badge/MSTest-00BFFF?style=flat&logo=visual-studio&logoColor=white) <br> MsTest    | ![FakeItEasy](https://img.shields.io/badge/FakeItEasy-4B8BBE?style=flat&logo=.net&logoColor=white) <br> FakeItEasy  | ![Microsoft.Extensions.Logging](https://img.shields.io/badge/Microsoft.Extensions.Logging-512BD4?style=flat&logo=.net&logoColor=white) <br> Microsoft.Extensions.Logging     |
| Reviews | User Reviews    | TBD        | TBD              | Fluent Validation | Automapper     | MsTest    | Rhinomock   | TBD     |
| Orchestrator | Orchestration between services    | None        | None              | None | None     | MsTest    | None   | TBD     |


Messaging Systems involved

 - RabbitMq


Monitoring Systems

 - Prometheus + Grafana
 - Portainer
