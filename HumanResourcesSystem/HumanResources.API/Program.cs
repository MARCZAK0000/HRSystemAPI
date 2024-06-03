using HumanResources.API.Middleware;
using HumanResources.Application.Extension;
using HumanResources.Domain.ServiceExtension;
using HumanResources.Infrastructure.Extension;
using HumanResources.Infrastructure.SeederDatabase;
using Microsoft.OpenApi.Models;
using NLog.Web;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.  //Add to Avoid problem with Identity  and ISystemClock
builder.Services.AddControllers();
builder.Host.UseNLog();
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<LoggerMiddleware>();
builder.Services.AddInfrastructure(builder.Configuration, builder.Environment.IsDevelopment());
builder.Services.AddApplication();
builder.Services.AddValidation();
builder.Services.AddMemoryCache();
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", cfg =>
    {
        cfg.WithOrigins("http://localhost:5173")
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Pleas pass your JWT TOKEN KEY",
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });


    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme()
            {
                Name = "Bearer",
                In = ParameterLocation.Header,
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

var app = builder.Build();
var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<Seeder>();
await seeder.Seed();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<LoggerMiddleware>();

app.UseAuthorization(); //Add to Avoid problem with Identity  
app.UseAuthentication();
app.MapControllers();

app.Run();
