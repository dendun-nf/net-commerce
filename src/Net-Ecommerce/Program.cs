using Net_Ecommerce;
using Net_Ecommerce.Data;
using Net_Ecommerce.Data.Seeds;
using Net_Ecommerce.Middleware;

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
    {
        using var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        using var ctx = scope.ServiceProvider.GetRequiredService<NetCommerceDbContext>();
        await ctx.Initialize();
        await ctx.Seed();
    }

    // app.UseExceptionHandler("/dev-error");
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<CustomExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
