using System.IdentityModel.Tokens.Jwt;
using System.Threading.RateLimiting;
using DigiChoiceBackend.Persistance;
using DigiChoiceBackend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
{
    JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

     builder.Services.AddDbContext<DataContext>(options =>
     {
         string connectionString = builder.Configuration.GetConnectionString("MARIADB");
         MariaDbServerVersion serverVersion = new(ServerVersion.AutoDetect(connectionString));

         options.UseMySql(connectionString, serverVersion);
     });
     
    // Add services to the DI container.
    builder.Services.AddProjectServices(); // Custom services

    builder.Services.AddControllers();

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    
    builder.Services.AddSwaggerGen(setup =>
    {
        var jwtSecurityScheme = new OpenApiSecurityScheme
        {
            BearerFormat = "JWT",
            Name = "JWT Authentication",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = JwtBearerDefaults.AuthenticationScheme,
            Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

            Reference = new OpenApiReference
            {
                Id = JwtBearerDefaults.AuthenticationScheme,
                Type = ReferenceType.SecurityScheme
            }
        };

        setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

        setup.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            { jwtSecurityScheme, Array.Empty<string>() }
        });
    });
    
    builder.Services.AddCors(p => p.AddPolicy("corsSettings",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173", "localhost:5173").AllowAnyMethod()
                .AllowAnyHeader().AllowCredentials();
        }));

    builder.Services.AddRateLimiter(opt =>
    {
        opt.AddFixedWindowLimiter(policyName: "fixed", options =>
        {
            options.PermitLimit = 12;
            options.Window = TimeSpan.FromSeconds(12);
        });
    
        opt.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    });
    
    builder.Services.AddOutputCache();

    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
        o.MetadataAddress = "http://localhost:8080/realms/digid/.well-known/openid-configuration";
        o.RequireHttpsMetadata = false; // only for dev
        o.SaveToken = true;
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            // NOTE: Usually you don't need to set the issuer since the middleware will extract it 
            // from the .well-known endpoint provided above. but since I am using the container name in
            // the above URL which is not what is published issuer by the well-known, I'm setting it here.
            // ValidIssuer = "http://localhost:8080/auth/realms/AuthDemoRealm", 
                    
            ValidAudience = "account",
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.FromMinutes(1)
        };
        
    });
    
}

var app = builder.Build();
{
// Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    
    app.UseCors("corsSettings");

    app.UseHttpsRedirection();
    
    app.UseAuthentication();
    
    app.UseAuthorization();

    app.UseRateLimiter();

    app.UseOutputCache();

    app.UseStaticFiles();

    app.MapControllers();

    app.Run();
}

