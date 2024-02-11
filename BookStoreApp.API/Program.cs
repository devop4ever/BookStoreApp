using BookStoreApp.API.Configurations;
using BookStoreApp.API.Data;
using BookStoreApp.API.Security;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("BookStoreConnection");

builder.Services.AddDbContext<BookStoreContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddAutoMapper(typeof(AutoMapperConfig));


builder.Host.UseSerilog((ctx, lc) =>
    lc.WriteTo.Console()
    .ReadFrom.Configuration(ctx.Configuration)
); 

builder.Services.AddCors(options =>
{
    options.AddPolicy(nameof(CorsPolicies.AllowAll), 
        b => b.AllowAnyMethod()
        .AllowAnyHeader()
        .AllowAnyOrigin());
});


var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(nameof(CorsPolicies.AllowAll));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
