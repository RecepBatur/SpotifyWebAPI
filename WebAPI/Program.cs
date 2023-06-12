using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Business.DepedencyResolvers.Autofac;
using Core.Utilities.Security.Enycption;
using Core.Utilities.Security.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Autofac WebAPI Konfigurasyonu
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacBusinessModule()));

builder.Services.AddCors(options =>
{

    options.AddPolicy("AllowOrigin",
        configurePolicy: builder => builder.WithOrigins("http://localhost:7240")); //origin istek yapýlan yer demek.
});


var Configuration = builder.Configuration;
var tokenOptions = builder.Configuration.GetSection(key: "TokenOptions").Get<TokenOptions>(); //TokenOptions'u okuduk.



builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>

{
    options.TokenValidationParameters = new TokenValidationParameters

    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true, //token geçerliliði
        ValidIssuer = tokenOptions.Issuer,
        ValidAudience = tokenOptions.Audience,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)

    };
});

//builder.Services.AddHttpContextAccessor();
//builder.Services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();


// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(builder => builder.WithOrigins("http://localhost:7240").AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthentication(); //giriþ anahtarý

app.UseAuthorization(); //yetki

app.MapControllers();

app.Run();
