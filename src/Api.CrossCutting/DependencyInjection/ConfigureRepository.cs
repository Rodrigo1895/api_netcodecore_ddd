using Api.Data.Context;
using Api.Data.Implementations;
using Api.Data.Repository;
using Api.Domain.Interfaces;
using Api.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.DependencyInjection {
    public class ConfigureRepository {
        public static void ConfigureDependenciesRepository(IServiceCollection serviceCollection) {
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            serviceCollection.AddScoped<IUserRepository, UserImplementation>();

            // MySql
            /*serviceCollection.AddDbContext<MyContext> (
                options => options.UseMySql("Server=localhost;Port=3306;Database=db_api_ddd;Uid=root;Pwd=root")
            );*/

            // SqlServer
            serviceCollection.AddDbContext<MyContext>(
                options => options.UseSqlServer("Server=.\\SQLEXPRESS2019;Database=db_api_ddd;User Id=sa;Password=root")
            );
        }
    }
}