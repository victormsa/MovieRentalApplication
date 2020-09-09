using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RentalWorkPlease.Areas.Identity.Data;
using RentalWorkPlease.Data;

[assembly: HostingStartup(typeof(RentalWorkPlease.Areas.Identity.IdentityHostingStartup))]
namespace RentalWorkPlease.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<AuthDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("AuthDbContextConnection")));

                services.AddDefaultIdentity<User>(options => 
                {
                    //alterações feitas no código para remover a necessidade de confirmação da conta criada
                    options.SignIn.RequireConfirmedAccount = false;
                    //removendo as restrições de criação de senha do usuário
                    options.Password.RequiredLength = 6;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                })
                    .AddEntityFrameworkStores<AuthDbContext>();
            });
        }
    }
}