using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using QLBAOCAOCV.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace QLBAOCAOCV.DAL
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            // Connection string dùng cho design-time
            var connectionString =
                "Server=localhost,1433;" +
                "Database=QLBAOCAOCV;" +
                "User Id=sa;" +
                "Password=Thinh@123;" +
                "TrustServerCertificate=True";

            optionsBuilder.UseSqlServer(connectionString);

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
