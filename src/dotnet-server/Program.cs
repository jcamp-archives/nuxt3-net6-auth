using dotnet_server;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var Configuration = builder.Configuration;

// Add services to the container.
services.AddMediatR(typeof(ApplicationDbContext));

services.AddMongoDbContext<ApplicationDbContext>(o =>
    o.ConnectionString = Configuration.GetConnectionString("DefaultConnection"));

services.AddMongoIdentity<ApplicationUser, MongoIdentityRole, ApplicationDbContext>(options => options.SignIn.RequireConfirmedAccount = true);

services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = Configuration["JwtIssuer"],
            ValidAudience = Configuration["JwtAudience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSecurityKey"]))
        };
    });

services.AddAuthorization(config =>
{
    config.AddPolicy(Policies.IsAdmin, Policies.IsAdminPolicy());
    config.AddPolicy(Policies.IsUser, Policies.IsUserPolicy());
});


services.AddControllersWithViews()
    .ConfigureApiBehaviorOptions(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                return new BadRequestObjectResult(new Result().Invalid(context.ModelState));
            };
        })
        .AddFluentValidation(fv =>
        {
            fv.ImplicitlyValidateChildProperties = true;
            fv.RegisterValidatorsFromAssemblyContaining<PersonValidator>();
        })
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

services.Configure<SmtpSettings>(Configuration.GetSection("SmtpSettings"));
services.AddScoped<IEmailService, EmailService>();
services.AddScoped<IRazorViewToStringRenderer, RazorViewToStringRenderer>();

services.AddTransient<IJwtHelper, JwtHelper>();
services.AddScoped<IUserAccessor, UserAccessor>();


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

app.Run();
