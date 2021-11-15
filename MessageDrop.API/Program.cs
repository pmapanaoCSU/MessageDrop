using MessageDrop.API.Service;
using MessageDrop.Core.Interface;
using MessageDrop.EF;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// BUG : Creates 20, and when re-run it creates 20 more 
var context = new MessageDropDataContext();
context.Database.EnsureCreated();

IConfiguration _configuration;
_configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();


// Add services to the container.

// Add my services to container 
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// MAX 30 
//builder.Services.AddScoped<IMessageData, DBMessageData>();
if (context.Messages.Any()) builder.Services.AddSingleton<IMessageData>(new DBMessageData(context));
else builder.Services.AddSingleton<IMessageData>(new DBMessageData(true, 20, context));


// Add the DB context 
builder.Services.AddDbContext<MessageDropDataContext>(opt =>
    opt.UseSqlServer(_configuration.GetConnectionString("MessageDropDataConnex"))
    .EnableSensitiveDataLogging()
    );

builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
