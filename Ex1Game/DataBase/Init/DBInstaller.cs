using Ex1Game.DataBase.Entity;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ex1Game.DataBase.Init
{
    public static class DBInstaller
    {
        public static IServiceCollection AddDBConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PlayerDBContext>(options =>
            {
                options.UseInMemoryDatabase("Game");
            });

            services.AddScoped<IPlayerRepository, PlayerRepository>();
            services.AddScoped<DBLoader>();
            return services;
        }
        public static void UseDBInitializer(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var initializer = scope.ServiceProvider.GetService<DBLoader>();
                initializer.Load();
            }
        }
    }
}
