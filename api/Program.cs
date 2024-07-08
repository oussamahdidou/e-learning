
using api.Data;
using api.interfaces;
using api.Model;
using api.Repository;
using api.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers().AddNewtonsoftJson(
    options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

    }
);
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
builder.Services.AddDbContext<apiDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

});
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;

})
.AddEntityFrameworkStores<apiDbContext>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme =
    options.DefaultChallengeScheme =
    options.DefaultForbidScheme =
    options.DefaultScheme =
    options.DefaultSignInScheme =
    options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true, //issuer url same as jwt token creation url
        ValidIssuer = builder.Configuration["JWT:Issuer"], //audience
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"],//issuer url same as jwt token creation url
        ValidateIssuerSigningKey = true,
        //IssuerSigningKey = new SymmetricSecurityKey(
                //System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigninKey"]))

    };
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin",
        builder => builder.WithOrigins("http://localhost:4200")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    );
});
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IModuleRepository, ModuleRepository>();
builder.Services.AddScoped<IInstitutionRepository, InstitutionRepository>();


builder.Services.AddScoped<INiveauScolaireRepository, NiveauScolaireRepository>();

builder.Services.AddScoped<IChapitreRepository, ChapitreRepository>();




builder.Services.AddScoped<ITestNiveauRepository, TestNiveauRepository>();
builder.Services.AddScoped<IModuleRequirementsRepository, ModuleRequirementsRepository>();

builder.Services.AddScoped<IQuizRepository,QuizRepository>();
builder.Services.AddScoped<IQuizResultRepository,QuizResultRepository>();
builder.Services.AddScoped<ICheckChapterRepository, CheckChapterRepository>();
builder.Services.AddScoped<IResultControleRepository, ResultControleRepository>();

var app = builder.Build();
if (args.Length >= 2 && args[0].Length == 1 && args[1].ToLower() == "seeddata")
{
    await SeedData.SeedUsersAndRolesAsync(app);
    await SeedData.Initialize(app);

}
else
{
    Console.WriteLine("Invalid arguments or missing command.");
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseCors("AllowOrigin");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();


app.Run();

