using Library.Data;
using Library.Services.Commands;
using Library.Services.Models.Media.Book;
using Library.Services.Models.Media.Movies;
using Library.Services.Models.Media.Music;
using Library.Services.Profiles;
using Library.Services.Queries;
using Library.Services.Services;
using Library.Services.Services.Media;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Media API",
        Version = "v1",
        Description = "An API to manage books, music, and movies."
    });
});

builder.Services.AddAutoMapper(typeof(LibraryProfile));

builder.Services.AddDbContext<LibraryContext>(options => options.UseSqlServer(builder.Configuration["SQLConnection"]));
builder.Services.AddScoped<IContentCommandFactory<Book>,BookCommand>();

builder.Services.AddScoped<IContentQueryFactory<Book>,BookQuery>();

builder.Services.AddScoped<IValidate,Validate>();

builder.Services.AddScoped<IContentServiceFactory<Book>,BookService>();
builder.Services.AddScoped<IContentServiceFactory<Movie>,MovieService>();
builder.Services.AddScoped<IContentServiceFactory<Music>,MusicService>();

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
