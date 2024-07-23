# Shopping Basket API
An API to manage a shopping basket. <br>
It is possible to create, retrieve, update and delete products and baskets.
This project was developed using .NET Core 8.0 and Entity Framework Core 8.0.0. <br>
The api depends on a Postgres database (included in the docker-compose).

## Installing / Getting started
To run this application you will need Docker and Docker-compose. Go to the root directory of the main app solution (where docker-compose.yml is) and execute the command:
```shell
docker-compose up -d --build
```

The docker-compose file will do all the job and start the application at the url http://localhost:4200.

To stop the application just execute the following command:
```shell
docker-compose stop
```