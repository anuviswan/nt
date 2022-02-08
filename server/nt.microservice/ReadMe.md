# Nt.Microservice

This project demonstrate usage of Microservice architecture for implementing backend for Nt application. Key points in the implementation include

- API Gateway has been implemented with [Ocelot](https://ocelot.readthedocs.io/en/latest/) library.

Implementation details of different services are as follows.

| Service | Role            | Database   | ORM              | Unit Test | Model Validation  | Entity Mapping |
| ------- | --------------- | ---------- | ---------------- | --------- | ----------------- | -------------- |
| Auth    | Authentication  | Postgres   | Dapper           |           | Fluent Validation | Mapster        |
| User    | User Management | Sql Server | Entity Framework | XUnit     | Data Annotations  | Automapper     |
