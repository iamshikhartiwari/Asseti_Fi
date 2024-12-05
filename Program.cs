//
//
// using System.Security.Claims;
// using System.Text;
// using Asseti_Fi.Aessiti_Fi_DBContext;
// using Asseti_Fi.Models;
// using Asseti_Fi.Repositories.AdminRepository; // Add Repositories namespaces
// using Asseti_Fi.Repositories.CommonRepository;
// using Asseti_Fi.Repositories.UserRepository;
// using Microsoft.AspNetCore.Authentication.JwtBearer;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.IdentityModel.Tokens;
// using Microsoft.OpenApi.Models;
//
// var builder = WebApplication.CreateBuilder(args);
//
// // Configure DbContext
// builder.Services.AddDbContext<AesstsDBContext>(options =>
//     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//
// // Configure Identity
// builder.Services.AddIdentity<User, IdentityRole>()
//     .AddEntityFrameworkStores<AesstsDBContext>()
//     .AddDefaultTokenProviders();
//
// // Register Repositories for Dependency Injection
// builder.Services.AddScoped<IAssetManagementRepo, AssetManagementRepo>();
// builder.Services.AddScoped<IAuditManagementRepo, AuditManagementRepo>();
// builder.Services.AddScoped<IUserManagementRepo, UserManagementRepo>();
// builder.Services.AddScoped<IAssetAllocationRepo, AssetAllocationRepo>();
// builder.Services.AddScoped<IServiceRequestsRepo, ServiceRequestRepo>();
// builder.Services.AddScoped<IUserRepo, UserRepo>();
//
// // Configure Swagger
// builder.Services.AddSwaggerGen(c =>
// {
//     c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
// });
//
// // Configure JWT Authentication
// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//     .AddJwtBearer(options =>
//     {
//         options.RequireHttpsMetadata = false;
//         options.SaveToken = true;
//         options.TokenValidationParameters = new TokenValidationParameters
//         {
//             ValidateIssuer = true,
//             ValidateAudience = true,
//             ValidateLifetime = true,
//             ValidateIssuerSigningKey = true,
//             ValidIssuer = builder.Configuration["Jwt:Issuer"],
//             ValidAudience = builder.Configuration["Jwt:Audience"],
//             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]))
//         };
//     });
//
// // Add MVC services
// builder.Services.AddControllersWithViews();
//
// var app = builder.Build();
//
// // Configure the HTTP request pipeline
// if (!app.Environment.IsDevelopment())
// {
//     app.UseExceptionHandler("/Home/Error");
//     app.UseHsts();
// }
//
// app.UseSwagger();
// app.UseSwaggerUI(c =>
// {
//     c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1");
// });
//
// app.UseHttpsRedirection();
// app.UseStaticFiles();
//
// app.UseRouting();
//
// // Add Authentication & Authorization middleware
// app.UseAuthentication(); 
// app.UseAuthorization();
//
// app.MapControllerRoute(
//     name: "default",
//     pattern: "{controller=Home}/{action=Index}/{id?}");
//
// app.Run();
using System.Text;
using Asseti_Fi.Aessiti_Fi_DBContext;
using Asseti_Fi.Models;
using Asseti_Fi.Repositories.AdminRepository;
using Asseti_Fi.Repositories.AuthRepository;
using Asseti_Fi.Repositories.CommonRepository;
using Asseti_Fi.Repositories.UserRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configure DbContext
builder.Services.AddDbContext<AesstsDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure Identity


// Register Repositories for Dependency Injection
// builder.Services.AddScoped<IAssetManagementRepo, AssetManagementRepo>();
// builder.Services.AddScoped<IAuditManagementRepo, AuditManagementRepo>();
// builder.Services.AddScoped<IUserManagementRepo, UserManagementRepo>();
// builder.Services.AddScoped<IAssetAllocationRepo, AssetAllocationRepo>();
// builder.Services.AddScoped<IServiceRequestsRepo, ServiceRequestRepo>();
builder.Services.AddScoped<IUserRepo, UserRepo>();
// builder.Services.AddScoped<IAuthRepository, AuthRepository>();


// builder.Services.AddIdentity<User, IdentityRole>()
//     .AddEntityFrameworkStores<AesstsDBContext>()
//     .AddDefaultTokenProviders();



// Configure Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {token}'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[] {}
        }
    });
});

// Configure JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"])),
            RoleClaimType = "roles" // Add this line to support roles from token claims
        };
    });

// Add Authorization policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
});

// Add MVC services
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1");
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Add Authentication & Authorization middleware
app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
