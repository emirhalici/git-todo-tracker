# git-todo-tracker

See this [document](https://docs.google.com/document/d/1rbawcy05nQbd9XHw2ykaGYU4EBiTnISwY_bz5vodlKs/edit) for detailed project document.

# Database

This project uses SQL Server from Microsoft. Database is run via Docker to have support in ARM macOS. Docker Desktop app must be installed and running to start the docker instance.

> Note: Rosetta2 must be enabled on macOS to run non-arm binaries.

Start docker instance:

```zsh
docker run --platform=linux/amd64 --name git_todo_tracker -e ACCEPT_EULA=1 -e MSSQL_SA_PASSWORD=GitTodoTracker123# -p 2022:1433 -d mcr.microsoft.com/mssql/server:2022-latest
```

After running this command, go to Docker Desktop, Containers and switch on `Only show running containers` to see actively running containers. A container with the name `pokemonsql` should appear.

Connection string after docker instance is running:

```
Server=localhost,2022;User Id=SA;Password=GitTodoTracker123#;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=15;
```

## Endpoint Todo List

#### Auth

- [x] [GET] `/api/auth/me`
- [x] [POST] `/api/auth/register`
- [x] [POST] `/api/auth/login`
- [x] [POST] `/api/auth/refresh-token`
- [ ] [POST] `/api/auth/recovery/initiate`
- [ ] [POST] `/api/auth/recovery/reset-password`

#### Repository

- [ ] [GET] `api/repository/list`
- [ ] [POST] `api/repository/register`
- [ ] [DELETE] `api/repository/remove`

#### Todo

- [ ] [GET] `/api/todo/list`
- [ ] [POST] `/api/todo/convert-to-issue`
