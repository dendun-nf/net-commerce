using Net_Ecommerce;
using Net_Ecommerce.Data;
using Net_Ecommerce.Data.Seeds;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddNetCommerceService(builder.Configuration);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
    using var ctx = scope.ServiceProvider.GetRequiredService<NetCommerceDbContext>();
    await ctx.Database.EnsureCreatedAsync();
    await ctx.Initialize();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
