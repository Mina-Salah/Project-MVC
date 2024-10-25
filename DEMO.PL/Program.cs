using DEMO.BLL.InterFaces;
using DEMO.BLL.Repository;
using DEMO.DAL.Context;
using DEMO.DAL.Entity;
using DEMO.PL.mapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DEMO.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();



            builder.Services.AddDbContext<Add_context>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("Defaultconnection"));
            });

            builder.Services.AddScoped<IDepartment_interface,Department_Repository>();
            builder.Services.AddScoped<IEmployee_InterFace,Employee_Rep0sitory>();
            builder.Services.AddScoped<ICatigory_inteface,Catigory_Repository>();
            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
            builder.Services.AddAutoMapper(map => map.AddProfile(new mappingprofile()));

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,option=> {
                        option.LoginPath = new PathString("Account/Login");
                    });
            builder.Services.AddIdentity<AplicationUser,ApplicationRole>(options =>
            {
                options.Password.RequireDigit = true;  
                options.Password.RequireUppercase = true;  
                options.Password.RequiredLength = 7;  

            })
                
                .AddEntityFrameworkStores<Add_context>()
                .AddTokenProvider<DataProtectorTokenProvider<AplicationUser>>
                (TokenOptions.DefaultProvider);
           ;


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();    

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}