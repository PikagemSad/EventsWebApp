using EventsWebApp.Application.Interfaces;
using EventsWebApp.Application.Services;
using EventsWebApp.Core.Interfaces;
using EventsWebApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using EventsWebApp.Infrastructure.Data;
using System.Text.Json.Serialization;
using Core.Exceptions;


var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<EventsDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IParticipantRepository, ParticipantRepository>();

builder.Services.AddAutoMapper(typeof(EventProfile));
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "https://localhost:5001";
        options.RequireHttpsMetadata = false;
        options.Audience = "WebAPI";
    });

builder.Services.AddAuthorization();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve; 
    });

var app = builder.Build();
app.UseMiddleware<ExceptionHandlingMiddleware>();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); 
    app.UseSwaggerUI(); 
}

app.MapGet("/", async context =>
{
    context.Response.Redirect("/swagger");
    await context.Response.CompleteAsync();
});

app.MapGet("/open-events", async context =>
{
    context.Response.Redirect("/api/events");
    await context.Response.CompleteAsync();
});

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
