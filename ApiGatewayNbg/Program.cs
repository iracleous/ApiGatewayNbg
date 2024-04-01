using ApiGatewayNbg.Services;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//1
builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
                            .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
                            ;
builder.Services.AddControllers();


//2
builder.Services.AddOcelot();



var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
//3
app.UseOcelot().Wait();
app.UseMiddleware<TransformMiddleware>();
app.Run();
