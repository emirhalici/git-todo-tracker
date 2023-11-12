global using git_todo_tracker.Models;
global using git_todo_tracker.Dtos.Auth;
global using git_todo_tracker.Dtos.Todo;

using git_todo_tracker.Services;
using git_todo_tracker.Services.Repository;
using git_todo_tracker.Repositories.Repository;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IRepositoryService, RepositoryService>();
builder.Services.AddSingleton<IRepositoryRepository, RepositoryRepository>();
builder.Services.AddSingleton<GitRepositoryService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
