using Microsoft.EntityFrameworkCore;
using OptimaTech.BuildingManager.User.Api.Middlewares;
using OptimaTech.BuildingManager.User.Api.Routes;
using OptimaTech.BuildingManager.User.Api.Services;
using OptimaTech.BuildingManager.User.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add Service
TenantServices.Add(builder);
RoleServices.Add(builder);
ProjectServices.Add(builder);
UnitServices.Add(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Register Middlewares
app.UseMiddleware<CheckCodeHeaderMiddleware>();

//Mapping Route
TenantRoute.Map(app);
RoleRoute.Map(app);
ProjectRoute.Map(app);
UnitRoute.Map(app);


app.Run();

