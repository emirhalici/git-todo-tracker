# git-todo-tracker

This is .NET Web API project I developed for a web applications class. 
As the course was project based this is my submission for the final evaluation.

This project serves as a simple backend with a REST api, the frontend client is implemented on a separate .NET Razor project linked [here](https://github.com/emirhalici/git-todo-tracker-web-client).

# Get Started

To get started please first setup the database and start running the server image. 

Then start this project by running

```
dotnet watch run
```

# Database

This project uses SQL Server from Microsoft. Database is run via Docker to have support in ARM macOS. Docker Desktop app must be installed and running to start the docker instance.

> Note: Rosetta2 must be enabled on macOS to run non-arm binaries.

Start docker instance:

```zsh
docker run --platform=linux/amd64 --name git_todo_tracker -e ACCEPT_EULA=1 -e MSSQL_SA_PASSWORD=GitTodoTracker123# -p 2022:1433 -d mcr.microsoft.com/mssql/server:2022-latest
```

After running this command, go to Docker Desktop, Containers and switch on `Only show running containers` to see actively running containers. A container with the name `git_todo_tracker` should appear.

Connection string after docker instance is running:

```
Server=localhost,2022;User Id=SA;Password=GitTodoTracker123#;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=15;
```

# Endpoints

<img width="1453" alt="swagger endpoints" src="https://github.com/emirhalici/git-todo-tracker/assets/81600010/adb833b7-acbd-46f2-805b-7aeb4614ea11">


