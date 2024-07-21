# Shopping Basket
An API to manage a shopping basket. <br>
It is possible to create, retrieve, update and delete a questionnaire, it's questions and answers. <br>
This project was developed using .NET Core 7.0 and Entity Framework Core 7.0.9. <br>
The api depends on a SQL Server database (included in the docker-compose).

## Installing / Getting started
To run this application you will need Docker and Docker-compose. Go to the root directory of the solution (where docker-compose.yml is) and execute the command:
```shell
docker-compose up -d --build
```

The docker-compose file will do all the job and start the application at the url http://localhost:4200.

To stop the application just execute the following command:
```shell
docker-compose stop
```

## Project Overview

This API uses layers architecture with CQRS, thinking about a production use case, where it is possible to have two different databases, one for reading, another one for writing. <br>
Features included:
- Unit tests
- Logging
- Code First Migrations
- External call to a third-party API
- HATEOAS Rest architecture
- SOLID design principles
- Design patterns (Mediator, Builder)
- Dependency Injection (defined in Registry.cs)
- Dockerfile and docker-compose
- Postman collection with use cases

##  Improvements

Some points to improve if there was more time, or for a real production scenario:
- Increase the Unit Tests coverage to be higher than 80%
- Add integration tests
- Improve HATEOAS links
- Add caching
- Improve logging for more specific situations
- Add a monitoring tool such as Kibana
- Add validators to secure data integrity
- Parametrize application properties with profiles
- Use vaults to store sensitive data
- Use two databases, one for writing, another for reading