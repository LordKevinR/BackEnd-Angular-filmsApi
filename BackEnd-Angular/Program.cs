using AutoMapper;
using BackEnd_Angular;
using BackEnd_Angular.Filters;
using BackEnd_Angular.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddSingleton(provider =>
	new MapperConfiguration(config =>
	{
		var geometryFactory = provider.GetRequiredService<GeometryFactory>();
		config.AddProfile(new AutoMapperProfiles(geometryFactory));
	}).CreateMapper());

builder.Services.AddTransient<IAzureStorage, AzureStorage>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer("name=DefaultConnection", x => x.UseNetTopologySuite()));

builder.Services.AddSingleton<GeometryFactory>(NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326));

builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(builder =>
	{
		var frontendURL =
		builder.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader()
		.WithExposedHeaders(new string[] { "totalRecordsNumber" });
	});
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();

builder.Services.AddControllers(options =>
{
	options.Filters.Add(typeof(ExceptionFilter));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();