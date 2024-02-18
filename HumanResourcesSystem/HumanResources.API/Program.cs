using HumanResources.API.Middleware;
using HumanResources.Application.Extension;
using HumanResources.Infrastructure.Extension;
using HumanResources.Infrastructure.SeederDatabase;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.  //Add to Avoid problem with Identity  and ISystemClock
builder.Services.AddControllers();
builder.Host.UseNLog();
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
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
builder.Services.AddSwaggerGen();


var app = builder.Build();
var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<Seeder>();
await seeder.Seed();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.UseMiddleware<ErrorHandlingMiddleware>();


app.UseAuthorization(); //Add to Avoid problem with Identity  
app.UseAuthentication();
app.MapControllers();

app.Run();
