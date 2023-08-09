# Task manager app
### Welcome to the Task Manager CRUD App repository
The main feature of this app is the ability to create and delete tasks which are then stored in the database for the specific user. New users can be created in the register page or you can log into an existing user on the login page.

## Features and Technologies
- ASP.NET Core Web API backend
- Entity Framework which is an ORM framework for handling database entities
- MSSQL database server
- React was used for the frontend which is a JavaScript library for building user interfaces
- JWT authentication and authorization
- Protected React routes
- CI pipeline with Github workflows for .NET with own tests

## To get started with the TaskManager-CRUD-app

### You can run the TaskManager-CRUD-app

Either with Docker or manually by using dotnet, npm and mssql

First clone the repository

### With Docker
You will need Docker

Navigate to: .\TaskManager-CRUD-app

Then type in the terminal
```sh
docker compose build
```

Once the building finishes start the containers by typing in the terminal
```sh
docker compose up -d
```

### Without Docker
You will need to have dotnet, nodejs and mssql installed

Install the required dependencies for the frontend by navigating to:  .\TaskManager-CRUD-app\Frontend\task-manager

Then type in the terminal
```sh
npm install
```
To run the backend navigate to:  .\TaskManager-CRUD-app\Backend\TaskManagerApi\TaskManagerApi 

Then type in the terminal
```sh
dotnet run
```
To run the frontend navigate to:  .\TaskManager-CRUD-app\Frontend\task-manager

Then type in the terminal
```sh
npm start
```

