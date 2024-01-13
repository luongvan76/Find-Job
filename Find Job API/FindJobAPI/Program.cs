using FindJobAPI.Data;
using Microsoft.EntityFrameworkCore;
using System;
using FindJobAPI.Repository.Interfaces;
using FindJobAPI.Repository.Queries;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using FindJobAPI;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
  
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Description = "Token receive by firebase",
            Name = "Authorization",
            Scheme = "Bearer",
            BearerFormat = "JWT"
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference()
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });
    
});


// Firebase
builder.Services.AddSingleton(FirebaseApp.Create(new AppOptions
{
    Credential = GoogleCredential.FromJson(builder.Configuration.GetValue<string>("firebaseConfig"))
}));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddScheme<AuthenticationSchemeOptions, FirebaseAuthenticationHandler>(JwtBearerDefaults.AuthenticationScheme,
        options => { });

// Register DB
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")!);
    options.EnableSensitiveDataLogging();
});

// Register Repository
builder.Services.AddScoped<IType_Repository, Type_Repository>();
builder.Services.AddScoped<IIndustry_Repository, Industry_Repository>();
builder.Services.AddScoped<IAccount_Repository, Account_Repository>();
builder.Services.AddScoped<ISeeker_Repository, Seeker_Repository>();
builder.Services.AddScoped<IEmployer_Repository, Employer_Repository>();
builder.Services.AddScoped<IJob_Repository, Job_Repository>();
builder.Services.AddScoped<IRecruitment_Repository, Recruitment_Repository>();
builder.Services.AddScoped<IRecruitmentNoAccount_Repository, RecruitmentNoAccountRepository>();

//Add CORS service
builder.Services.AddCors(option =>
{
    option.AddPolicy("FindJobPolicy", builder =>
    {
        builder
        .AllowAnyOrigin() // enable all origins
        .AllowAnyHeader() // enable all headers
        .AllowAnyMethod(); // enable all method
    });
});




var app = builder.Build();

app.UseCors("FindJobPolicy");

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();


app.MapControllers();

app.Run();
