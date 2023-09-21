using BackEnd_Angular;
using BackEnd_Angular.Filters;
using BackEnd_Angular.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddTransient<IAzureStorage, AzureStorage>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer("name=DefaultConnection"));

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

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
