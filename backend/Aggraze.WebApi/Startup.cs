using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Aggraze.Application;
using Aggraze.Application.Insights;
using Aggraze.Application.Services;
using Aggraze.Domain.Calculators;
using Aggraze.Infrastructure;
using Aggraze.Infrastructure.Services;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Aggraze.WebApi;

public class Startup
{
    private readonly IConfiguration configuration;

    public Startup(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddOpenApi();
        services.AddControllers();
        services.AddHealthChecks();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Aggraze API",
                Version = "v1",
            });

            var jwtSecurityScheme = new OpenApiSecurityScheme
            {
                Scheme = "bearer",
                BearerFormat = "JWT",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Description = "JWT Authorization header using the Bearer scheme.",
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };

            options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { jwtSecurityScheme, Array.Empty<string>() }
            });
        });

        services.AddHangfire(config =>
            config.UseSqlServerStorage(this.configuration.GetConnectionString("DefaultConnection")));
        services.AddHangfireServer();

        // Register Domain services
        services.AddScoped<IAverageRunningTimeCalculator, AverageRunningTimeCalculator>();
        services.AddScoped<IMutationCalculator, MutationCalculator>();
        services.AddScoped<IMaximumDrawdownCalculator, MaximumDrawdownCalculator>();
        services.AddScoped<IMaximumRiskRewardWinningTradesCalculator, MaximumRiskRewardWinningTradesCalculator>();

        // Register application services
        services.AddScoped<AggregationOrchestratorService>();

        // Register infrastructure services
        services.AddScoped<IFileWriterService, FileWriterService>();
        services.AddScoped<IExcelGenerationService, ExcelGenerationService>();
        services.AddScoped<IFileStorage, LocalFileStorage>();
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<IFileProcessingService, FileProcessingService>();
        services.AddScoped<IBackgroundJobService, HangfireBackgroundJobService>();

        // Register insights
        services.AddScoped<IInsight, AverageRunningTime>();
        services.AddScoped<IInsight, AverageRunningTimeLosers>();
        services.AddScoped<IInsight, AverageRunningTimeWinners>();
        services.AddScoped<IInsight, MaximumDrawdown>();
        services.AddScoped<IInsight, MaximumRiskRewardWinningTrades>();
        services.AddScoped<IInsight, Mutation>();

        // Database
        services.AddDbContext<AggrazeDbContext>(options =>
            options.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection")));

        services
            .AddIdentityCore<UserEntity>(options => options.User.RequireUniqueEmail = true)
            .AddEntityFrameworkStores<AggrazeDbContext>();

        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer("Bearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = this.configuration["Authentication:Jwt:Issuer"],
                    ValidAudience = this.configuration["Authentication:Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(this.configuration["Authentication:Jwt:SecretKey"]!))
                };
            });

        // Optional: Logging
    }

    public void Configure(IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AggrazeDbContext>();
            dbContext.Database.Migrate();
        }

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseHealthChecks("/health");
        app.UseEndpoints(endpoint =>
        {
            endpoint.MapControllers();
            endpoint.MapHealthChecks("/health");
            endpoint.MapHangfireDashboard();
        });

        app.UseHttpsRedirection();

        // TODO: add check for environment
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Aggraze web API");
            options.RoutePrefix = string.Empty;
        });
    }
}
