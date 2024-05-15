using Microsoft.EntityFrameworkCore;
using NOS.Engineering.Challenge.Models;

namespace NOS.Engineering.Challenge.Database
{
    public class Context : DbContext
    {
        public DbSet<Content> Content { get; set; } = null!;

        public Context(DbContextOptions options) : base(options)
        {
        }
    }
}
