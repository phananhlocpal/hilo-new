using Microsoft.EntityFrameworkCore;
using MovieService.Data.MovieData;
using MovieService.Models;
using JwtAuthenticationManager;
<<<<<<< HEAD
using AutoMapper;  
=======
using AutoMapper;
using MovieService.Data.ActorData;
using MovieService.Data.ProducerData;
using MovieService.Data.CategoryData;
>>>>>>> 960a83c (commit)

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MovieContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));  
builder.Services.AddScoped<IMovieRepo, MovieRepo>();
<<<<<<< HEAD
=======
builder.Services.AddScoped<IActorRepo, ActorRepo>();
builder.Services.AddScoped<IProducerRepo, ProducerRepo>();
builder.Services.AddScoped<ICategoryRepo, CategoryRepo>();
>>>>>>> 960a83c (commit)
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddCustomJwtAuthentication();

var app = builder.Build();
<<<<<<< HEAD

=======
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:1000") // Update this URL to match your React app's URL
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});
>>>>>>> 960a83c (commit)
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
