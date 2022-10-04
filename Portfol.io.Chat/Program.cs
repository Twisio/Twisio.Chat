using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Portfol.io.Chat.Data;
using Portfol.io.Chat.Hubs;
using System.Reflection;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("PostgreSQL");
builder.Services.AddDbContext<ChatDbContext>(option => option.UseNpgsql());
builder.Services.AddScoped<IChatDbContext>(provider => provider.GetService<ChatDbContext>()!);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    config.IncludeXmlComments(xmlPath);

    config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    config.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
            },
            new string[] { }
        }
    });
});

builder.Services.AddSignalR();

builder.Services.AddAuthentication(conf =>
{
    conf.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    conf.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = AuthenticationOptions.ISSUER,
        ValidateAudience = true,
        ValidAudience = AuthenticationOptions.AUDIENCE,
        ValidateLifetime = true,
        IssuerSigningKey = AuthenticationOptions.GetSymmetricSecurityKey(),
        ValidateIssuerSigningKey = true
    };

    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var access_token = context.HttpContext.User.Claims.First(u => u.Type == ClaimTypes.NameIdentifier).Value;

            var path = context.HttpContext.Request.Path;

            if(access_token is not null && path.StartsWithSegments("/chat"))
            {
                context.Token = access_token;
            }

            return Task.CompletedTask;
        }
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<ChatHub>("/chat");

app.Run();
