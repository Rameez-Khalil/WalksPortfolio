using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.ObjectPool;
using Microsoft.IdentityModel.Tokens;
using System.Text;
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
builder.Services.AddDbContext<WalksAuthDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("AuthDb"))); 

//injecting the scope.
builder.Services.AddScoped<IRegionRepository, RegionRepository>();
builder.Services.AddScoped<IWalkRepository, WalkRepository>();
builder.Services.AddScoped<ITokenRepo, TokenRepo>();

//injecting the automapper.
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));



//adding identity.
builder.Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("WalksAuth")
    .AddEntityFrameworkStores<WalksAuthDbContext>()
    .AddDefaultTokenProviders();

//adding identity options.
//password validation  length, or precursor to settting up the password
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 6;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequiredUniqueChars = 1;
}); 

//adding authentication.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,  //ensures that our server generated the token.
            ValidateAudience = true, //ensures that the token was intended for our API.
            ValidateLifetime = true, //ensures that the token hasn't expired yet.
            ValidateIssuerSigningKey = true, //ensures that the token's isn't tampered.
            ValidIssuer = builder.Configuration["Jwt:Issuer"],  //the trusted issuer (appsettings)
            ValidAudience = builder.Configuration["Jwt:Audience"],  //expected audience
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]??"")) //key validation

        }); 



var app = builder.Build();

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
