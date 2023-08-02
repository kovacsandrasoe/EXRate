using EXRate.Backend.Data;
using EXRate.Backend.Logic;
using EXRate.Backend.Models;
using EXRate.Backend.Repository;
using EXRate.Backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MNB;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IMNBArfolyamServiceSoapClient, MNBArfolyamServiceSoapClient>();
builder.Services.AddTransient<IMNBService, MNBService>();
builder.Services.AddTransient<IRateRecordRepository, RateRecordRepository>();
builder.Services.AddTransient<IAuthManager, AuthManager>();
builder.Services.AddTransient<IRateLogic, RateLogic>();

builder.Services.AddDbContext<EXRateContext>(options =>
                options.UseInMemoryDatabase("db"));
builder.Services.AddCors();

builder.Services.AddIdentity<AppUser, IdentityRole>(
                    option =>
                    {
                        option.Password.RequireDigit = true;
                        option.Password.RequiredLength = 10;
                        option.Password.RequireNonAlphanumeric = false;
                        option.Password.RequireUppercase = true;
                        option.Password.RequireLowercase = true;
                    }
                ).AddEntityFrameworkStores<EXRateContext>()
                .AddDefaultTokenProviders();

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Site"],
        ValidIssuer = builder.Configuration["Jwt:Site"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SigningKey"]))
    };
});

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

app.UseCors(t => t
    .WithOrigins("http://localhost:4200")
    .AllowAnyHeader()
    .AllowCredentials()
    .AllowAnyMethod());

app.Run();
