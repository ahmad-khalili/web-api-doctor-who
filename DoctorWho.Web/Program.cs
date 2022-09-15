using DoctorWho.Db;
using DoctorWho.Db.Entities;
using DoctorWho.Db.Repositories;
using DoctorWho.Web.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DoctorWhoCoreDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:DoctorWhoDb"]);
});

builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();

builder.Services.AddScoped<IEpisodeRepository, EpisodeRepository>();

builder.Services.AddScoped<IEnemyRepository, EnemyRepository>();

builder.Services.AddScoped<ICompanionRepository, CompanionRepository>();

builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IValidator<Doctor>, DoctorValidator>();

builder.Services.AddScoped<IValidator<Episode>, EpisodeValidator>();

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