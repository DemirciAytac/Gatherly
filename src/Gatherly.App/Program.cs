using Gatherly.App;
using Gatherly.Application;
using Gatherly.Domain.Repositories;
using Gatherly.Infrastructure;
using Gatherly.Persistence;
using MediatR;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApp()
    .AddApplication()
    .AddInfrastructure()
    .AddPersistence(builder.Configuration);
builder.Services.AddControllers()
    .AddApplicationPart(Gatherly.Presentation.AssemblyReference.Assembly);

builder.Services.AddSwaggerGen();

var app = builder.Build();


    app.UseSwagger();
    app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
