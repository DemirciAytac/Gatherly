using Gatherly.App;
using Gatherly.App.Extensions;
using Gatherly.Application;
using Gatherly.Domain.Repositories;
using Gatherly.Infrastructure;
using Gatherly.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApp()
    .AddApplication()
    .AddInfrastructure()
    .AddPersistence(builder.Configuration);
builder.Services.AddControllers()
    .AddApplicationPart(Gatherly.Presentation.AssemblyReference.Assembly);
//builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    await app.ApplyMigrationAsync();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
