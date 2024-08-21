using static System.Net.WebRequestMethods;
using System.Runtime.ConstrainedExecution;
using System;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
	options.AddPolicy(name: MyAllowSpecificOrigins,
					  policy =>
					  {
						  policy.WithOrigins("https://localhost:44310/",
											  "http://www.contoso.com",
											  "https://localhost:44310",
											  "https://localhost:44395",
											  "https://localhost:5295",
											  "https://localhost:7020",
											  "https://netdocs-my.sharepoint.com",
											  "http://www.microsoft.com"
											  ).AllowAnyHeader()
											  .AllowAnyMethod();
					  });
});





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
	app.UseCors(MyAllowSpecificOrigins);
}

app.UseHttpsRedirection();




app.UseAuthorization();
app.MapControllers();
app.UseCors(MyAllowSpecificOrigins);
app.Run();





//Do this in the order

//dotnet dev-certs https --clean
//Remove your keys and pem from C:\Users\%username%\AppData\Roaming\ASP.NET\https
//dotnet dev-certs https --trust
//Run SPA project with "start": "set HTTPS=true&&react-scripts start"