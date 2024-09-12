
using api.Data;
using api.interfaces;
using api.Model;
using api.Repository;
using api.Dtos.EmailConfirmation;

using api.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Azure.Storage.Blobs;
using CloudinaryDotNet;
using Hangfire;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(5062); // Listen on all network interfaces on port 5062
});

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
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), sqlServerOptions =>
    {
        sqlServerOptions.CommandTimeout(300);
    });
});

builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;
    // options.Tokens.EmailConfirmationTokenProvider.Lifetime = TimeSpan.FromHours(1); 
    // options.Tokens.PasswordResetTokenProvider.Lifetime = TimeSpan.FromHours(2);

})
.AddDefaultTokenProviders()
.AddEntityFrameworkStores<apiDbContext>();

// builder.Services.Configure<DataProtectionTokenProviderOptions>(opt =>
//     opt.TokenLifespan = TimeSpan.FromHours(2));


builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHangfireServer();

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
        IssuerSigningKey = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigninKey"]))

    };
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
              builder =>
               {
                   builder.WithOrigins("*")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
               });
});
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<ITestNiveauRepository, TestNiveauRepository>();
builder.Services.AddScoped<IModuleRequirementsRepository, ModuleRequirementsRepository>();
builder.Services.AddScoped<IQuizRepository, QuizRepository>();
builder.Services.AddScoped<IQuizResultRepository, QuizResultRepository>();
builder.Services.AddScoped<ICheckChapterRepository, CheckChapterRepository>();
builder.Services.AddScoped<IResultControleRepository, ResultControleRepository>();
builder.Services.AddScoped<IinstitutionRepository, InstitutionRepository>();
builder.Services.AddScoped<INiveauScolaireRepository, NiveauScolaireRepository>();
builder.Services.AddScoped<IModuleRepository, ModuleRepository>();
builder.Services.AddScoped<IChapitreRepository, ChapitreRepository>();
builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));
builder.Services.AddSingleton<IMailer, Mailer>();
builder.Services.AddScoped<IDashboardRepository, DashboardRepository>();
builder.Services.AddScoped<IControleRepository, ControleRepository>();
builder.Services.AddScoped<IExamFinalRepository, ExamFinalRepository>();
builder.Services.AddScoped<IUserCenterInterface, UserCenterRepository>();
builder.Services.AddScoped<IResultExamRepository, ResultExamRepository>();
builder.Services.AddScoped<IBlobStorageService, BlobStorageService>();
builder.Services.AddScoped<IParagrapheRepository, ParagrapheRepository>();
builder.Services.AddScoped<IPosteRepository, PosteRepository>();
builder.Services.AddScoped<IElementPedagogiqueRepository, ElementPedagogiqueRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IReminder, ReminderRepository>();
builder.Services.AddScoped<IInstitutionStudentRepository, InstitutionStudentRepository>();

builder.Services.AddSingleton(x => new BlobServiceClient(builder.Configuration["AzureBlobStorage:ConnectionString"]));
builder.Services.AddSingleton(x => new Cloudinary(builder.Configuration["Cloudinary:CloudinaryUrl"]));

var app = builder.Build();
if (args.Length >= 2 && args[0].Length == 1 && args[1].ToLower() == "seeddata")
{
    await SeedData.SeedUsersAndRolesAsync(app);
    // await SeedData.Initialize(app);

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
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        ctx.Context.Response.Headers.Append("Access-Control-Allow-Origin", "http://localhost:4200");
        ctx.Context.Response.Headers.Append("Access-Control-Allow-Methods", "GET,HEAD,OPTIONS");
        ctx.Context.Response.Headers.Append("Access-Control-Allow-Headers", "Content-Type");
    }
});

app.UseHangfireDashboard();
RecurringJob.AddOrUpdate<IReminder>("send-reminder",
    x => x.SendReminder(),
    "0 9 * * 1");

app.UseRouting();
app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigin");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();


app.Run();

