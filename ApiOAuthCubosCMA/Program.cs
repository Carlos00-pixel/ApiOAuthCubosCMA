using ApiOAuthCubosCMA.Data;
using ApiOAuthCubosCMA.Helpers;
using ApiOAuthCubosCMA.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<HelperOAuthToken>();

HelperOAuthToken helper = new HelperOAuthToken(builder.Configuration);

builder.Services.AddAuthentication(helper.GetAuthenticationOptions())
    .AddJwtBearer(helper.GetJwtOptions());

// Add services to the container.
string connectionString =
    builder.Configuration.GetConnectionString("SqlServer");
builder.Services.AddTransient<RepositoryCubos>();
builder.Services.AddDbContext<CubosContext>
    (options => options.UseSqlServer(connectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Api OAuth Cubos 2023",
        Version = "v1",
        Description = "Prueba Examen Seguridad Api"
    });
});

//builder.Services.AddOpenApiDocument(document =>
//{
//    document.Title = "Api Cubos";
//    document.Description = "Api Cubos";
//    document.AddSecurity("JWT", Enumerable.Empty<string>(),
//        new NSwag.OpenApiSecurityScheme
//        {
//            Type = OpenApiSecuritySchemeType.ApiKey,
//            Name = "Authorization",
//            In = OpenApiSecurityApiKeyLocation.Header,
//            Description = "Copia y pega el Token en el campo 'Value:' así: Bearer {Token JWT}."
//        }
//    );
//    document.OperationProcessors.Add(
//        new AspNetCoreOperationSecurityScopeProcessor("JWT"));
//});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint(
        url: "/swagger/v1/swagger.json", name: "Api Cubos");
    options.RoutePrefix = "";
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
