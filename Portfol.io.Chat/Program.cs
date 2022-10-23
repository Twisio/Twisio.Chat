using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Portfol.io.Chat.API.Hubs;
using Portfol.io.Chat.Application;
using Portfol.io.Chat.Persistence;
using System.Reflection;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("PostgreSQL");

builder.Services.AddApplication();
builder.Services.AddPersistence(connectionString);

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

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
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
                var accessToken = context.Request.Headers.Authorization.FirstOrDefault();

                if (accessToken == null) accessToken = context.Request.Query.FirstOrDefault(u => u.Key == "access_token").Value;

                if(accessToken != null && accessToken!.Contains("Bearer ")) accessToken = accessToken!.Substring(7);

                // если запрос направлен хабу
                var path = context.HttpContext.Request.Path;
                if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/chat"))
                {
                    // получаем токен из строки запроса
                    context.Token = accessToken;
                }
                return Task.CompletedTask;
            }
        };
    });


builder.Services.AddCors(options => options.AddPolicy("AllowAllOrigins", builder =>
{
    builder.AllowAnyHeader();
    builder.AllowAnyMethod();
    builder.AllowCredentials();
    builder.SetIsOriginAllowed((host) => true);
}));

var app = builder.Build();

app.UseCors("AllowAllOrigins");

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHub<ChatHub>("/chat");

app.Run();
