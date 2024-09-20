# Nt.Microservice

This project demonstrate usage of Microservice architecture for implementing backend for Nt application. Key points in the implementation include

- API Gateway has been implemented with [Ocelot](https://ocelot.readthedocs.io/en/latest/) library.

Implementation details of different services are as follows.

| Service | Role            | Database   | ORM              | Model Validation  | Entity Mapping | Unit Test | Mock        | Logger  |
| ------- | --------------- | ---------- | ---------------- | ----------------- | -------------- | --------- | ----------- | ------- |
| Auth    | Authentication  | Postgres   | Dapper           | Fluent Validation | Mapster        | NUnit     | NSubstitute | NLog    |
| User    | User Management | Sql Server | Entity Framework | Data Annotations  | Automapper     | XUnit     | Moq         | SeriLog |
| Movies  | Movie Meta info | MongoDb    | MongoDb.Entities | Json.Net  | ValueInjector     | MsTest    | FakeItEasy  | Microsoft.Extensions.Logging     |
| Reviews | User Reviews    | TBD        | TBD              | Fluent Validation | Automapper     | MsTest    | Rhinomock   | TBD     |
| Orchestrator | Orchestration between services    | None        | None              | None | None     | MsTest    | None   | TBD     |


Messaging Systems involved

 - RabbitMq


Monitoring Systems

 - Prometheus + Grafana
 - Portainer
