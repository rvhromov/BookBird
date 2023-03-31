# BookBird

**Sample** .NET 6 application which follows **clean architecture** and **CQRS** principles with a bit of **DDD** flavour.
The main **goal** of this project is to try and explore contemporary frameworks, libraries, patterns and principles when building modern web application.
The structure is the following:
- Api - provides public endpoints to interact with application;
- Jobs - background workers for long-running operations and time scheduled tasks;
- Infrastructure - place where gatherd all implementation details, I/O operations, cross-cutting concerns and configurations;
- Application - the level responsible for orchestration and abstractions ;
- Domain - business rules and rich domain models with POCO objects;
- UnitTests - project to unit test the app's logic.

# Main Tools and Technologies

- MediatR
- MassTransit
- RabbitMq
- ElasticSearch
- Kibana
- Quartz
- SendGrid
- Serilog

# Requirements

- [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [Docker](https://docs.docker.com/get-docker)

To start the infrastructure via Docker, type the following command at the `compose` directory:

`docker compose -f infrastructure.yml up -d`

For a proper work the Jobs app should be started alongside with the Api.