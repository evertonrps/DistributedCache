using DistributedCache.Data.Repositories;
using DistributedCache.Domain.Interfaces;
using DistributedCache.Domain.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddDistributedMemoryCache();

builder.Services.AddScoped<IDistributedCacheService, DistributedCacheService>();
builder.Services.AddScoped<IDistributedRepository, DistributedRepository>();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.InstanceName = "memorydb:";
    options.Configuration = "localhost,port: 6379,password=Redis2024!";
});
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