using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using to_do_minimal_api_mongodb.Entities;
using to_do_minimal_api_mongodb.Infrastructure;
using Task = to_do_minimal_api_mongodb.Entities.Task;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<MongoDbService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapGet("/api/todo", async (MongoDbService _mongo) =>
{
    var taskCollection = _mongo.Database?.GetCollection<Task>("tasks");

    var tasks = await taskCollection.Find(Builders<Task>.Filter.Empty).ToListAsync();
    
    return Results.Ok(tasks);
});

app.MapPost("/api/todo", async ([FromBody] Task newTask, MongoDbService _mongo) =>
{
    var taskCollection = _mongo.Database?.GetCollection<Task>("tasks");
    
    await taskCollection!.InsertOneAsync(newTask);

    return Results.Created("api/todo", newTask);
});

app.MapPut("/api/todo/done-task/{id}", async (string id, MongoDbService _mongo) =>
{
    var taskCollection = _mongo.Database?.GetCollection<Task>("tasks");
    
    var filter = Builders<Task>.Filter.Eq(t => t.Id, id);
    var update = Builders<Task>.Update.Set(t => t.IsDone, true);

    await taskCollection!.UpdateOneAsync(filter, update);

    return Results.Ok($"Task with id: {id} done!");
});

app.MapDelete("/api/todo/{id}", async (string id, MongoDbService _mongo) =>
{
    var taskCollection = _mongo.Database?.GetCollection<Task>("tasks");
    
    var filter = Builders<Task>.Filter.Eq(t => t.Id, id);

    await taskCollection!.DeleteOneAsync(filter);

    return Results.Ok("Task deleted successfully!");
});

app.UseHttpsRedirection();

app.Run();
