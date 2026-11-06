using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SchoolManagement.Application;
using SchoolManagement.Infrastructure.Repositories;
using SchoolManagement.Infrastructure.Repositories;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Identity;
using SchoolManagement.Infrastructure.Persistence;
using SchoolManagement.Infrastructure.Services;
using Serilog;
using System.Text;
using SchoolManagement.Infrastructure.Services.SchoolManagement.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// ---------- 1. SERILOG SETUP ----------
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();

// ---------- 2. DBCONTEXT SETUP ----------
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IStudentRepository,StudentRepository>();
builder.Services.AddScoped<IStudentService, StudentService>();

builder.Services.AddScoped<ITeacherRepository,TeacherRepository>();
builder.Services.AddScoped<ITeacherService, TeacherService>();

builder.Services.AddScoped<IClassRepository, ClassRepository>();
builder.Services.AddScoped<IClassService, ClassService>();


builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();
builder.Services.AddScoped<ISubjectService,SubjectService>();

builder.Services.AddScoped<IClassSubjectRepository, ClassSubjectRepository>();
builder.Services.AddScoped<IClassSubjectService, ClassSubjectService>();

builder.Services.AddScoped<IAttendanceRepository, AttendanceRepository>();
builder.Services.AddScoped<IAttendanceService, AttendanceService>();

builder.Services.AddScoped<IExamRepository, ExamRepository>();
builder.Services.AddScoped<IExamService, ExamService>();

builder.Services.AddScoped<IMarksRepository, MarksRepository>();
builder.Services.AddScoped<IMarksService, MarksService>();

builder.Services.AddScoped<IFeeRepository, FeeRepository>();
builder.Services.AddScoped<IFeeService, FeeService>();

builder.Services.AddScoped<INoticeRepository, NoticeRepository>();
builder.Services.AddScoped<INoticeService, NoticeService>();

builder.Services.AddScoped<IAdmissionRepository, AdmissionRepository>();
builder.Services.AddScoped<IAdmissionService, AdmissionService>();
// ---------- 3. IDENTITY SETUP ----------
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// ---------- 4. JWT AUTHENTICATION SETUP ----------
//var jwtSettings = builder.Configuration.GetSection("JwtSettings");
//var secretKey = jwtSettings["Key"]!;

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>()!;

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
    };
});

// ---------- 5. MEMORY CACHE ----------
builder.Services.AddMemoryCache();

// ---------- 6. AUTOMAPPER ----------
builder.Services.AddAutoMapper(cfg => { }, AppDomain.CurrentDomain.GetAssemblies());

// ---------- 7. CONTROLLERS + SWAGGER ----------
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() { Title = "School Management API", Version = "v1" });

    // JWT ko Swagger UI me support karne ke liye
    options.AddSecurityDefinition("Bearer", new()
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter: Bearer {your token}"
    });

    options.AddSecurityRequirement(new()
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new List<string>()
        }
    });
});

builder.Services.AddControllers();
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowHtmlPage", policy =>
//    {
//        policy
//            .WithOrigins("http://127.0.0.1:5500")
//            .AllowAnyHeader()
//            .AllowAnyMethod();
//    });
//});
builder.Services.AddCors(o => o.AddPolicy("react", p =>
    p.WithOrigins("http://localhost:5173").AllowAnyHeader().AllowAnyMethod()));
// ...

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<SchoolManagement.Api.Middleware.ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//app.UseCors("AllowHtmlPage");
app.UseCors("react");   // UseAuthentication se pehle
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
// ---------- ROLE SEEDING ----------
using (var scope = app.Services.CreateScope())
{
    await RoleSeeder.SeedRolesAsync(scope.ServiceProvider);
}

app.Run();
