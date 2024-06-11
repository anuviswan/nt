# Nt.Microservice

This project demonstrate usage of Microservice architecture for implementing backend for Nt application. Key points in the implementation include

- API Gateway has been implemented with [Ocelot](https://ocelot.readthedocs.io/en/latest/) library.

Implementation details of different services are as follows.

| Service | Role            | Database   | ORM              | Model Validation  | Entity Mapping | Unit Test | Mock        | Logger  |
| ------- | --------------- | ---------- | ---------------- | ----------------- | -------------- | --------- | ----------- | ------- |
| Auth    | Authentication  | Postgres   | Dapper           | Fluent Validation | Mapster        | NUnit     | NSubstitute | NLog    |
| User    | User Management | Sql Server | Entity Framework | Data Annotations  | Automapper     | XUnit     | Moq         | SeriLog |
| Movies  | Movie Meta info | MongoDb    | TBD              | Data Annotations  | ValueInjector     | MsTest    | FakeItEasy  | TBD     |
| Reviews | User Reviews    | TBD        | TBD              | Fluent Validation | Automapper     | MsTest    | Rhinomock   | TBD     |


Messaging Systems involved

 - RabbitMq


Monitoring Systems

 - Prometheus + Grafana
 - Portainer
