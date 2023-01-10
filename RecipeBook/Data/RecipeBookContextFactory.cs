using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RecipeBook.Data
{
    public class RecipeBookContextFactory : IDbContextFactory<RecipeBookContext>
    {
        public RecipeBookContext CreateDbContext()
        {
            var builder = new DbContextOptionsBuilder<RecipeBookContext>();
            builder.UseSqlServer("Data Source=0353L-GZW7KL3;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            return new RecipeBookContext(builder.Options);
        }
    }
}
