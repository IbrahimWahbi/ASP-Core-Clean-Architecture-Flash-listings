using Albayan_Task.Domain.Interfaces;
using Albayan_Task.Errors;
using Albayan_Task.ExceptionMiddleWare;
using Albayan_Task.Helpers;
using Albayan_Task.Model.Data;
using Albayan_Task.Model.Repositories;
using Albayan_Task.Model.SeedData;
using Albayan_Task.Service.Iservices.Icategories;
using Albayan_Task.Service.Iservices.Iproducts;
using Albayan_Task.Service.Iservices.IUsers;
using Albayan_Task.Service.Services;
using Albayan_Task.Service.Services.Categories;
using Albayan_Task.Service.Services.CustomServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<MyDbContext>(options =>
{
    options.EnableSensitiveDataLogging();
    options.UseSqlServer(connectionString);
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// edentity model configuration
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 6;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
})
    .AddEntityFrameworkStores<MyDbContext>()
    .AddDefaultTokenProviders();


// Error handling method
builder.Services.Configure<ApiBehaviorOptions>(options =>
              options.InvalidModelStateResponseFactory = ActionContext =>
              {
                  var error = ActionContext.ModelState
                              .Where(e => e.Value.Errors.Count > 0)
                              .SelectMany(e => e.Value.Errors)
                              .Select(e => e.ErrorMessage).ToArray();
                  var errorresponce = new APIValidationErrorResponce
                  {
                      Errors = error
                  };
                  return new BadRequestObjectResult(error);
              }
            );

// add auth and jwt
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
    };
});

builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

// init Swagger for API test and make it fimilure with Bearer token
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    var security = new Dictionary<string, IEnumerable<string>>
                {
                    { "Bearer",new string[0]}
                };
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id="Bearer"
                            },
                            Scheme ="oauth2",
                            Name ="Bearer",
                            In = ParameterLocation.Header,
                        },new List<string>()
                    }
                });
});


// the dip injection
builder.Services.AddAutoMapper(typeof(MappingProfiles));
builder.Services.AddScoped<MyDbContext, MyDbContext>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUsersRepo, UsersRepository>();
builder.Services.AddScoped<IUsersService, UsersService>();

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<ICategoriesService, CategoriesService>();
builder.Services.AddScoped<IProductsService, ProductsService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// middleware for error handling it gaves form for error
app.UseMiddleware<ExceptionMiddle>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
