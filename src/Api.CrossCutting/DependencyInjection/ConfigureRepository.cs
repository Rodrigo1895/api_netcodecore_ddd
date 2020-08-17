using System;
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

            if (Environment.GetEnvironmentVariable("DATABASE").ToLower() == "SQLSERVER".ToLower()) {
                //"Server=.\\SQLEXPRESS2019;Database=db_api_ddd;User Id=sa;Password=root"
                serviceCollection.AddDbContext<MyContext>(
                    options => options.UseSqlServer(Environment.GetEnvironmentVariable("DB_CONNECTION"))
                );

            } else {
                serviceCollection.AddDbContext<MyContext>(
                    //"Server=localhost;Port=3306;Database=db_api_ddd;Uid=root;Pwd=root"
                    options => options.UseMySql(Environment.GetEnvironmentVariable("DB_CONNECTION"))
                );
            }

        }
    }
}