
using FluentValidation;
using FluentValidation.AspNetCore;
using HealthCare.Database;
using HealthCare.Mapping;
using HealthCare.Repositories;
using HealthCare.Services;
using HealthCare.Swagger;
using HealthCare.Validation;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;

namespace HealthCare
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddValidatorsFromAssemblyContaining<CreatePatientValidator>();
            builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IPatientRepository, PatientRepository>();
            builder.Services.AddScoped<IPatientService, PatientService>();

            builder.Services.AddAutoMapper(typeof(PatientProfile));

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new()
                {
                    Title = "Patients API",
                    Version = "v1",
                    Description = "Tes API for patients"
                });


                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.EnableAnnotations();
                options.IncludeXmlComments(xmlPath);
                options.ExampleFilters();
            });
            builder.Services.AddSwaggerExamplesFromAssemblyOf<PatientQueryExample>();

            var app = builder.Build();

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Patients API v1");
                options.RoutePrefix = "swagger"; // http://localhost:5000/swagger
            });

            
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
