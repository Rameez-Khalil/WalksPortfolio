using Microsoft.EntityFrameworkCore;
using Walks.API.Data;
using Walks.API.Mappings;
using Walks.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//injecting DB contexxt.
builder.Services.AddDbContext<WalksDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("WalksDb"))); 

//injecting the scope.
builder.Services.AddScoped<IRegionRepository, RegionRepository>();
builder.Services.AddScoped<IWalkRepository, WalkRepository>();

//injecting the automapper.
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles)); 

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
