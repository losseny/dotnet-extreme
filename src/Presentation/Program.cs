using Application;
using Data;
using Presentation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddPresentationServices();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.InitialiseAndSeedDatabase();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
