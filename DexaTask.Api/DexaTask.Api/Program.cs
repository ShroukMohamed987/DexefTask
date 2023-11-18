
using DexefTask.Api;
using DexefTask.BL.IReposatories;
using DexefTask.BL.Reposatories;
using DexefTask.DAL.Context;
using DexefTask.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;

namespace DexaTask.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            #region default

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            #endregion

            #region dataBase

            var connectionString = builder.Configuration.GetConnectionString("connectionStr");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            #endregion

            #region services

            builder.Services.AddScoped<IDexefUserRepo, DexefUserRepo>();
            builder.Services.AddScoped<ILoginServices, LoginServices>();


            #endregion

           


            #region Mapping

            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            #endregion

            #region identity

            builder.Services.AddIdentity<DexefUser, IdentityRole>(
            option =>
            {
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequiredLength = 5;
                option.Password.RequireUppercase = true;
                option.Password.RequireLowercase = true;
                option.User.RequireUniqueEmail = true;
                


            }).AddEntityFrameworkStores<ApplicationDbContext>();

            #endregion

            #region authentication

            builder.Services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = "col";
                option.DefaultChallengeScheme = "col";
            }).AddJwtBearer("col", options =>
            {
                var key = builder.Configuration.GetValue<string>("secretKey");
                //conver key to byte
                var keyAsByte = Encoding.ASCII.GetBytes(key ?? string.Empty);
                //convert key byte to object
                var keyObject = new SymmetricSecurityKey(keyAsByte);

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = keyObject,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                };
            });

            #endregion


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}