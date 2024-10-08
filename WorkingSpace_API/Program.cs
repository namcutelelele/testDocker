using Microsoft.EntityFrameworkCore;
using WorkingSpace_BusinessObject.Models;
using WorkingSpace_DataAccessLayer.DataLayer;
using WorkingSpace_Repositories;
using WorkingSpace_Services;
using ExceptionHandling;
using Microsoft.AspNetCore.Hosting;
using AutoMapper;
using WorkingSpace_BusinessObject.DTOs;

var mappingConfig = new MapperConfiguration(mc =>
{
    mc.CreateMap<AddNewWorkingSpaceDTO, WorkingSpace>();
    mc.CreateMap<WorkingSpace, ViewWorkingSpaceDTO>();
    mc.CreateMap<ViewWorkingSpaceDTO, WorkingSpace>();
});

IMapper mapper = mappingConfig.CreateMapper();


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnectionString"),
        new MySqlServerVersion(new Version(8, 0, 23))));

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IWorkingSpaceService, WorkingSpaceService>();
builder.Services.AddScoped<IWorkingSpaceRepository, WorkingSpaceRepository>();
builder.Services.AddScoped<WorkingSpaceDAO>();

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddSingleton(mapper);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
app.UseMiddleware<GlobalErrorHandlingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
