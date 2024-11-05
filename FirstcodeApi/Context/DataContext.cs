using Microsoft.EntityFrameworkCore;
using FirstcodeApi.Context;
using FirstcodeApi;
namespace FirstcodeApi.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) { 

        

        }
       
        public DbSet<Employee> Employees { get; set; }

    }
}


