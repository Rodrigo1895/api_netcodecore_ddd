using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Api.Data.Context {
    public class ContextFactory : IDesignTimeDbContextFactory<MyContext> {
        public MyContext CreateDbContext(string[] args) {

            //SqlServer
            //var connectionString = "Server=.\\SQLEXPRESS2019;Database=db_api_ddd;User Id=sa;Password=root";

            //MySql
            var connectionString = "Server=localhost;Port=3306;Database=db_api_ddd;Uid=root;Pwd=root";
            var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
            optionsBuilder.UseMySql(connectionString);
            //optionsBuilder.UseSqlServer(connectionString);
            return new MyContext(optionsBuilder.Options);
        }
    }
}