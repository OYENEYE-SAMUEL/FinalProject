using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Services;
using Application.Settings;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PayStack.Net;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddDbContext<WasteContext>(config => config.UseMySQL(builder.Configuration.GetConnectionString("WasteString")));


builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IStaffRepository, StaffRepository>();
builder.Services.AddScoped<IWasteCollectionRepository, WasteCollectionRepository>();
builder.Services.AddScoped<IWasteReportRepository, WasteReportRepository>();
builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
builder.Services.AddScoped<IIndividualRepository, IndividualRepository>();
builder.Services.AddScoped<IGovernmentAgentRepository, GovernmentAgentRepository>();
builder.Services.AddScoped<IContractRepository, ContractRepository>();
builder.Services.AddScoped<ICommunityRepository, CommunityRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<ICurrentUser, CurrentUser>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IStaffService, StaffService>();
builder.Services.AddScoped<IWasteCollectionService, WasteCollecitonService>();
builder.Services.AddScoped<IWasteReportService, WasteReportService>();
builder.Services.AddScoped<ISubscrptionService, SubscriptionService>();
builder.Services.AddScoped<IIndividualService, IndividualService>();
builder.Services.AddScoped<IGovernmentAgentService, GovernmentAgentService>();
builder.Services.AddScoped<IContractService, ContractService>();
builder.Services.AddScoped<ICommunityService, CommunityService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


builder.Services.AddScoped<IPayStackApi>(provider =>
{
    var config = provider.GetRequiredService<IConfiguration>();
    var payStackSecretKey = config["PayStack:SecretKey"];
    return new
    PayStackApi(payStackSecretKey);
});
builder.Services.Configure<AuthToken>(builder.Configuration.GetSection("AuthToken"));
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(opt =>
    {
        var jwtAuth = builder.Services.BuildServiceProvider()
        .GetRequiredService<IOptions<AuthToken>>().Value;

        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtAuth.Issuer,
            ValidAudience = jwtAuth.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtAuth.Key)),
        };
    });
builder.Services.AddAuthorization(opts =>
{
    opts.AddPolicy("admin", policy =>
    policy.RequireRole("admin"));
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "WasteManagementApi",
        Version = "v1",
        Contact = new()
        {
            Name = "E-mail",
            Email = "raph.com"
        }
    });

    // Configure Swagger to use the Authorization header
    c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Description = "Input your Bearer token to access this API",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        BearerFormat = "JWT",
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
