using Core.Domain.Database;
using Core.Interfaces;
using Core.Models;
using Core.Services;
using Microsoft.EntityFrameworkCore;
using WarpPortalAPI.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));


builder.Services.AddDbContextPool<RpaControlDBContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionBase")));
builder.Services.AddScoped<IDatabeseService, DatabeseService>();
builder.Services.AddScoped<IToolsxService, ToolsxService>();
builder.Services.AddScoped<IJwtUtils, JwtUtils>(); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseMiddleware<JwtMiddleware>();

app.MapControllers();

app.Run();
