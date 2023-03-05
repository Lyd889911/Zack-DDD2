using FileServer.WebAPI.Dtos;
using FluentValidation;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFileServer();
builder.Services.AddScoped<IValidator<FileRequest>, FileRequestValidator>();
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseAuthorization();

app.MapControllers();

app.Run();
