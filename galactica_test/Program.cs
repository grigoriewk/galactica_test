
using galactica_test.Db;
using galactica_test.Db.Accessors;
using galactica_test.Db.Accessors.Abstract;
using galactica_test.Services;
using galactica_test.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using System.Diagnostics;
using FluentValidation;
using FluentValidation.AspNetCore;
using galactica_test.Models.Request;
using galactica_test.Validators;

namespace galactica_test
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            // Add services to the container.

            builder.Services.AddControllers();
            
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Galactica_test"
                });
                var filePath = Path.Combine(AppContext.BaseDirectory, "galactica_test.xml");
                c.IncludeXmlComments(filePath);
            });

            builder.Services.AddScoped<IValidator<EditEmployeeCarRequest>, EditEmployeeCarRequestValidator>();
            builder.Services.AddScoped<IValidator<CreateEmployeeRequest>, CreateEmployeeRequestValidator>();
            builder.Services.AddScoped<IValidator<CreateEmployeeLicensePlateRequest>, CreateEmployeeLicensePlateRequestValidator>();
            builder.Services.AddScoped<IValidator<RemoveEmployeeLicensePlateRequest>, RemoveEmployeeLicensePlateRequestValidator>();
            
            builder.Services.AddSingleton<ILogService, LogService>();
            builder.Services.AddSingleton<ISecurityService, SecurityService>();
            builder.Services.AddSingleton<ISecurityContextAccessor, SecurityContextAccessor>();

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddDbContextFactory<SecurityContext>(ConfigureDbContext);
            builder.Services.AddDbContext<SecurityContext>(ConfigureDbContext);

            builder.Services.AddFluentValidationAutoValidation();
            
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

            app.UseCors(policyBuilder => policyBuilder
            .SetIsOriginAllowed(_ => true)
            .AllowAnyMethod()
            .AllowAnyHeader());

            app.Run();

            void ConfigureDbContext(DbContextOptionsBuilder options)
            {
                var connectionString = configuration.GetConnectionString("PgConnection");
                if (string.IsNullOrWhiteSpace(connectionString))
                    throw new NotSupportedException("ConnectionString.PgConnection can't be empty");

                options.UseNpgsql(connectionString, x =>
                {
                    x.MigrationsAssembly(typeof(SecurityContext).Assembly.FullName);
                    x.MigrationsHistoryTable(SecurityContext.MigrationsTableName);
                    x.CommandTimeout(10 * 60);
                    x.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                });

                options.EnableDetailedErrors();
                options.EnableSensitiveDataLogging();

                options.LogTo(s => Debug.WriteLine(s), LogLevel.Information);
            }
        }
    }
}
