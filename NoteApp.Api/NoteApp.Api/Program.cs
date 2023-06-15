using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using NoteApp.Api.Data;
using NoteApp.Api.Identity;
using NoteApp.Api.Services;
// using NoteApp.Api.Services;

const string myAllowAllOrigins = "_myAllowAllOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
                         {
                             options.AddPolicy(name: myAllowAllOrigins,
                                               policy =>
                                               {
                                                   policy.WithOrigins("*");
                                                   policy.AllowAnyMethod();
                                                   policy.AllowAnyHeader();
                                               });
                         });

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    config =>
    {
        config.SwaggerDoc("v1", new OpenApiInfo { Title = "Note App API", Version = "v1" });
        config.AddSecurityDefinition(
            "Bearer",
            new OpenApiSecurityScheme {
                Description =
                    "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer"
            });
        config.AddSecurityRequirement(new OpenApiSecurityRequirement {
            { new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme,
                                                                             Id = "Bearer" } },
              new List<string>() }
        });
    });

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
                                            { options.UseSqlServer(connectionString); });
builder.Services.AddScoped<NoteService>();
builder.Services.AddScoped<UserService>();

// Identity Services
builder.Services.AddIdentityCore<AppUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

// JWT Token setup
var jwtConfiguration = builder.Configuration.GetSection("Jwt").Get<JwtConfiguration>() ??
                       throw new Exception("JWT configuration not specified");

builder.Services.AddSingleton(jwtConfiguration);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
                  {
                      options.TokenValidationParameters = new TokenValidationParameters {
                          ValidateIssuer = true,
                          ValidateAudience = true,
                          ValidateLifetime = true,
                          ValidateIssuerSigningKey = true,
                          ValidIssuer = jwtConfiguration.Issuer,
                          ValidAudience = jwtConfiguration.Audience,

                          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.Secret))
                      };
                  });

var app = builder.Build();

// Create and see the database
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
    await IdentitySeed.SeedAsync(scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>(),
                                 scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>());
    // IMPORTANT: Must be left and run after IdentitySeed.SeedAsync
    Seeder.SeedNotes(db);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Configuration.GetValue<bool>("UseSwagger", false))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(myAllowAllOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
