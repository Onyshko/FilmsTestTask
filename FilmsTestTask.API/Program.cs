using FilmsTestTask.APIServices.Implementations;
using FilmsTestTask.APIServices.Interfaces;
using FilmsTestTask.APIServices.Mapping.Profiles;
using FilmsTestTask.APIServices.Models;
using FilmsTestTask.Domain.Entities;
using FilmsTestTask.Repositories.Context;
using FilmsTestTask.Repositories.Implementations;
using FilmsTestTask.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped(typeof(IBaseRepository<Film>), typeof(FilmRepository));

builder.Services.AddAutoMapper(typeof(FilmProfile).Assembly);
builder.Services.AddAutoMapper(typeof(CategoryProfile).Assembly);



builder.Services.AddScoped<IBaseCrudService<FilmApiModel>, BaseCrudService<Film, FilmApiModel>>();
builder.Services.AddScoped<IBaseCrudService<CategoryApiModel>, BaseCrudService<Category, CategoryApiModel>>();
builder.Services.AddScoped<IBaseCrudService<FilmCategoryApiModel>, BaseCrudService<FilmCategory, FilmCategoryApiModel>>();
builder.Services.AddScoped<ICategoryService, CategoryService>();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

app.Use(async (ctx, next) =>
{
    ctx.Response.Headers["Access-Control-Allow-Origin"] = "https://localhost:7010";
    
    if (HttpMethods.IsOptions(ctx.Request.Method))
    {
        ctx.Response.Headers["Access-Control-Allow-Headers"] = "*";

        await ctx.Response.CompleteAsync();
        return;
    }
    
    await next();
});

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
