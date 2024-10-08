
# Starting ASP.NET Web API with Docker Compose

This guide describes how to start an ASP.NET Web API project with Docker Compose, including configuring a database and seeding data.

## Prerequisites

1. **Docker**: If you want to use Docker and Docker Compose, make sure that both Docker and Docker Compose are installed on your machine. (optional. You can also run the application without Docker with your IDE or on commandline.)
2. **JetBrains Rider** (or another IDE like Visual Studio): To run and test the ASP.NET Web API locally.
3. **.NET Core SDK**: Ensure that you have the correct version of the .NET Core SDK installed.

## Project Structure

### Project Structure Overview

In this project, the `src` folder contains four main layers: `Presentation`, `Application`, `DataInfrastructure`, and `Domain`. Each layer serves a specific purpose within the architecture of the project.

---

### `Presentation` Layer

The `Presentation` layer is responsible for handling all interactions with the outside world, including APIs, user interfaces, and any form of input/output communication. This is where controllers, endpoints, and HTTP request handling are typically implemented. Its main role is to act as a bridge between the user (or external systems) and the core application logic.

---

### `Application` Layer

The `Application` layer contains the core business logic of the project. It defines the use cases and application services, coordinating the flow of data between the `Presentation` layer and the `Domain` layer. This layer focuses on orchestrating commands and queries, ensuring that the right business rules are applied at the right time.

---

### `DataInfrastructure` Layer

The `DataInfrastructure` layer is responsible for managing data access and communication with external systems. This includes not only repository implementations and database configurations but also interactions with external APIs and services. Whether the project needs to store data in a local database, retrieve information from an external API, or interact with third-party data sources, this layer abstracts those details and ensures consistent communication. It allows the other layers to remain agnostic of how and where data is being retrieved or persisted.

---

### `Domain` Layer

The `Domain` layer contains the heart of the business logic. It holds the core business entities, rules, and operations. This layer is responsible for representing the key concepts of the business and ensuring that all the business invariants are maintained. Entities, value objects, and domain services are typically placed in this layer, ensuring that the business logic is not tied to any specific infrastructure or presentation concerns.

---

By structuring the project in these distinct layers, the codebase remains clean, modular, and easier to maintain. Each layer has a clear responsibility, making the system more flexible and adaptable to change.

### Endpoints structure
Within the project, there are important .http files that can be found in the root or a test folder. These files are essential for testing the API. Below is a description of these files and their functions:
### `Presentation.Public.http`
This [file](src/Presentation/Presentation.Public.http) contains HTTP requests for testing the `public` endpoints:

1. **Simple Ping Test**
   - **Route**: `GET /public/ping`
   - **Description**: Tests if the API is active by getting a "Pong" response.

2. **Seed Data**
   - **Route**: `POST /public/seed`
   - **Description**: Seeds the database with test data.

### `Presentation.Reservation.http`
This [file](src/Presentation/Presentation.Reservation.http) contains HTTP requests for testing the `reservation` endpoints:

1. **Get Roster by Week**
   - **Route**: `GET /reservation/roster?week={{week_number}}`
   - **Description**: Retrieves the roster for a specific week.

2. **Get Reservation by ID**
   - **Route**: `GET /reservation/{id}`
   - **Description**: Retrieves a specific reservation by ID.

### `Presentation.Room.http`
This [file](src/Presentation/Presentation.Room.http) contains HTTP requests for testing the `room` endpoints:

1. **Create Reservation**
   - **Route**: `POST /room/{roomId}/reservation`
   - **Description**: Creates a new reservation for a specific room.
   - **Body**: JSON with reservation details such as date, period, and booker ID.


## Step-by-Step Guide

### 1. Running ASP.NET Web API locally (without Docker)

Follow these steps if you want to run the application without Docker:

1. Open the project in your preferred IDE (JetBrains Rider or Visual Studio).
2. Check if the correct version of the .NET Core SDK is installed. You can do this by using `dotnet --version` in your terminal.
3. Build the solution by clicking `Build` in your IDE or by running the following command in the terminal:
   ```bash
   dotnet build
   ```
4. Start the application by clicking `Run` in your IDE or use this command:
   ```bash
   dotnet run --project dotnet run --project src/Presentation/Presentation.csproj
   ```
5. Once the application is running, you can use the **Presentation.Public.http** file to test if the server is working correctly by sending a ping request. If everything is correct, you will receive a "Hello World" or another expected response.

### 2. Docker Compose Configuration

If you are using Docker Compose, you don’t have to worry about manually starting the database. The `docker-compose.yml` file handles the startup of both the ASP.NET Web API and the database.

#### 2.1 Docker Compose File ([docker-compose.yml](docker-compose.yml))

The `docker-compose.yml` file contains the configuration for starting the ASP.NET Web API and the PostgreSQL database. Below is an overview of the services defined in this file:
- **api**: The ASP.NET Web API service that hosts the application.
- **database**: The PostgreSQL database service that hosts the database.

#### 2.2 Starting the Application with Docker Compose

1. Ensure that you are in the project directory where the `docker-compose.yml` file is located.
2. Run the following command to start the containers:
   ```bash
   docker-compose up --build
   ```
   This command builds the API container and starts it along with the PostgreSQL database.

3. Wait until both containers are fully started. You can check if the database is running successfully by viewing the logs of the database container in the terminal:
   ```bash
   docker-compose logs db
   ```

4. Once the containers are running, you can use the **Presentation.Public.http** file to send a ping request to the API.

#### 2.3 Seeding Data into the Database

If you want to load test data into your database, you can use the [**Presentation.Public.http**](src/Presentation/Presentation.Public.http) file to send POST requests to the relevant API endpoints.
This ensures that dummy or test data is inserted into the database, which is useful for development purposes.

### 3. Troubleshooting

- **Database connection error**: If the API cannot connect to the database, check if the database container has fully started using `docker-compose logs db`.

### Note for Windows Users

If you are using Windows, you can download Docker Hub and use it instead of running the commands manually in the terminal. Docker Hub provides a graphical interface to manage containers, making it easier to start and stop the services without using command-line commands.
