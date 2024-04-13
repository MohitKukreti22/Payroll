using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PayRoll.Contexts;
using PayRoll.Interfaces;
using PayRoll.Models;
using PayRoll.Repositories;
using PayRoll.Services;
using System.Text;
using System.Text.Json.Serialization;

namespace PayRoll
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().AddJsonOptions(opts =>
            {
                opts.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                opts.JsonSerializerOptions.WriteIndented = true;
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });
                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["SecretKey"])),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("ReactPolicy", opts =>
                {
                    opts.WithOrigins("http://localhost:3000", "null").AllowAnyMethod().AllowAnyHeader();
                });
            });



            #region contexts
            builder.Services.AddDbContext<RequestTarkerContext>(opts =>
            {
                opts.UseSqlServer(builder.Configuration.GetConnectionString("requestTrackerConnection"));
            });
            #endregion

            #region RepositoryInjection
            builder.Services.AddScoped<IRepository<string, Validation>, ValidationRepository>();
            builder.Services.AddScoped<IRepository<int, PayrollProcessor>, PayrollProcessorRepository>();
            builder.Services.AddScoped<IRepository<int, Employee>, EmployeeRepository>();
            builder.Services.AddScoped<IRepository<int, Admin>, AdminRepository>();
            builder.Services.AddScoped<IRepository<int, AuditTrail>, AuditTrailRepository>();
            builder.Services.AddScoped<IRepository<int, LeaveRequest>, LeaveRequestRepository>();
            builder.Services.AddScoped<IRepository<int, Manager>, ManagerRepository>();
            builder.Services.AddScoped<IRepository<int, Payroll>, PayrollRepository>();
            builder.Services.AddScoped<IRepository<int, PayrollPolicy>, PayrollPolicyRepository>();
            builder.Services.AddScoped<IRepository<int, TimeSheet>, TimeSheetRepository>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IEmployeeLoginService, EmployeeService>();
            builder.Services.AddScoped<IAdminService, AdminService>();
            builder.Services.AddScoped<IAdminLoginService,AdminService>();
            builder.Services.AddScoped<IManagerService, ManagerService>();
            builder.Services.AddScoped<IManagerLoginService,ManagerService>();
            builder.Services.AddScoped<IAdminEmployeeService, AdminEmployeeService>();
           builder.Services.AddScoped<IEmployeeTimeSheetService, EmployeeTimeSheetService>();
            builder.Services.AddScoped<IEmployeePayrollService, EmployeePayrollService>();
            builder.Services.AddScoped<IPayrollProcessorLoginService, PayrollProcessorService>();
            builder.Services.AddScoped<IPayrollProcessorService,PayrollProcessorService>();
            builder.Services.AddScoped<IEmployeeLeaveRequestService, EmployeeLeaveRequestService>();
            builder.Services.AddScoped<IManagerLeaveRequestService,ManagerLeaveRequestService>();
            builder.Services.AddScoped<IPayrollProcessorPayrollService, PayrollProcessorPayrollService>();

            #endregion
          var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("ReactPolicy");
            app.UseAuthentication();

            app.UseAuthorization();

         

            


            app.MapControllers();

            app.Run();
        }
    }
}
