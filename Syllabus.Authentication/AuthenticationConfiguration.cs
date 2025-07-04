using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Syllabus.Authentication.Services;
using Syllabus.Domain.Authentication;
using Syllabus.Util.Options;
using System.Security.Claims;
using System.Text;

namespace Syllabus.Authentication;

public static class AuthenticationConfiguration
{
    public static IServiceCollection AddSyllabusAuthentication(this IServiceCollection services, IConfiguration configuration, Action<JwtOptions>? configureAction = null)
    {
        var jwtOptions = new JwtOptions();
        configuration.GetSection(JwtOptions.SectionName).Bind(jwtOptions);
        configureAction?.Invoke(jwtOptions);

        if (string.IsNullOrWhiteSpace(jwtOptions.Key))
        {
            throw new InvalidOperationException("JWT Key is not configured.");
        }

        var jwtKey = Encoding.UTF8.GetBytes(jwtOptions.Key);

        services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));

        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(jwtKey),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = jwtOptions.Issuer,
                ValidAudience = jwtOptions.Audience,
                NameClaimType = ClaimTypes.NameIdentifier
            };
        });

        return services;
    }
}
