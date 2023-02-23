using eTickets.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using eTickets.Repositories;
using Serilog;
using eTickets;
using eTickets.Services.UserService;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

serilog.Initializeloggers(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString") ?? throw new InvalidOperationException("Connection string 'eTicketsContext' not found.")));
builder.Services.AddScoped<IActorRepository, ActorRepository>();
builder.Services.AddScoped<IUploadImageRepository, UploadImageRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Logging.AddSerilog();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo { Title = "eTickets", Version = "v1" });
//});

//var _logger = new LoggerConfiguration().
//    ReadFrom.Configuration(builder.Configuration).Enrich.FromLogContext()
//    //.MinimumLevel.Warning()
//    //.WriteTo.File("G:\\MCA DDU\\sem-4\\.Log-.log", rollingInterval: RollingInterval.Day)
//    .CreateLogger();    
//builder.Logging.AddSerilog(_logger);

//Log.Logger = new LoggerConfiguration().CreateBootstrapLogger();
//builder.Host.UseSerilog(((ctx, lc) => lc

//.ReadFrom.Configuration(ctx.Configuration)));
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization By Dipesh",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Name = "Authorization"
    });
    c.OperationFilter<SecurityRequirementsOperationFilter>();
});


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding
        .UTF8.GetBytes(
         builder.Configuration
         .GetSection("AppSettings:Token").Value)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseClassWithNoImplementationMiddleware();
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.Run(async context =>
//{
//    await context.Response.WriteAsync("Hello Dear Readers from middleware!");
//});
//app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseAuthentication();


app.UseAuthorization();
// app.UseEndpoints(endpoint =>
// {
//     endpoint.MapControllers();
// });
app.UseMiddleware<LogUserNameMiddleware>();

app.MapControllers();

//app.MapCinemaEndpoints();

app.Run();
