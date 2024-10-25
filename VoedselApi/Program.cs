using Application;
using Application.Repositories;
using Infrastructure.ContextClasses;
using Microsoft.EntityFrameworkCore;
using HotChocolate.AspNetCore;
using VoedselApi.GraphQL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure the database context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Enable Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register repositories
builder.Services.AddScoped<IMealBoxRepository, MealBoxRepository>();

// Configure GraphQL with HotChocolate
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>(); // Register your query class here

var app = builder.Build();

// Enable Swagger for all environments (including production)
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
    options.RoutePrefix = "swagger"; // Serve Swagger UI at /swagger
});

// Enable GraphQL and GraphiQL
app.MapGraphQL("/graphql"); // GraphQL endpoint


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();