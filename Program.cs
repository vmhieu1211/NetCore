
global using Microsoft.EntityFrameworkCore;
global using WebApiApp.Data;
global using WebApiApp.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using WebApiApp.Middleware;
using WebApiApp.Repositories;
using WebApiApp.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyDB")));
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
});
builder.Services.AddTransient<Middleware>();
builder.Services.AddScoped<IRepository<Post>, PostServices>();
builder.Services.AddScoped<PostServices>();
builder.Services.AddScoped<UserServices>();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseClassWithNoImplementationMiddleware();
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseMiddleware<Middleware>();

}


//app.Run(async context =>
//{
//    await context.Response.WriteAsync("Hello from Middleware.");
//});


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
