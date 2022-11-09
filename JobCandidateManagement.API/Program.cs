using JobCandidateManagement.Business;
using JobCandidateManagement.DataAccess.Services;
using System.Reflection;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;
using JobCandidateManagement.API;
using Microsoft.OpenApi.Any;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/jobcandidate.txt", rollingInterval: RollingInterval.Day)
    .WriteTo.SQLite(Environment.CurrentDirectory + @"\log.db")
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;
}).AddNewtonsoftJson()
.AddXmlDataContractSerializerFormatters();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setupAction =>
{
    setupAction.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Job Candidate API",
        Description = "Job Candidate API",
        Version = "v1"
    });

    var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);
    setupAction.IncludeXmlComments(xmlCommentsFullPath);

    setupAction.MapType(typeof(TimeSpan), () => new OpenApiSchema
    {
        Type = "string",
        Example = new OpenApiString("00:00:00")
    });
});

builder.Services.AddScoped<IJobCandidateService, JobCandidateCSVService>();
builder.Services.AddScoped<IJobCandidateRepository, JobCandidateCSVRepository>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.ConfigureExceptionHandling();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
