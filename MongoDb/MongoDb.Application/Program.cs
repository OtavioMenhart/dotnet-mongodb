using FluentValidation;
using MongoDb.Application.Configurations;
using MongoDb.Data.Context;
using MongoDb.Domain.Configurations;
using MongoDb.Domain.Dto;
using MongoDb.Domain.Interfaces.Services;
using MongoDb.Service.Services;
using MongoDb.Service.Validator;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<DatabaseSettings>(
        builder.Configuration.GetSection("MongoConnection"));

builder.Services.AddSingleton<MongoDbContext>();

builder.Services.AddSingleton<IValidator<CreateOrUpdateBookDto>, CreateOrUpdateBookDtoValidator>();

builder.Services.AddScoped<IDataService, DataService>();
builder.Services.AddTransient<IBookService, BookService>();

LogConfiguration.ConfigureLogging();
builder.Host.UseSerilog();

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
