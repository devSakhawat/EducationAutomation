using pbERP.Api.Extension;
using pbERP.Api.Helpers;
using pbERP.Api.Middleware;
using pbERP.DataStructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// add services ApplicationServices
builder.Services.AddApplicationServices(builder.Configuration);


var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseSwagger();
app.UseSwaggerUI();

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<pbERPContext>();
var logger = services.GetRequiredService<ILogger<Program>>();

app.Run();
