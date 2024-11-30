namespace uvodni_strana.Data
{
    using Microsoft.EntityFrameworkCore;
   


    using System.Xml;

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // tady mohou být modely tabulek databáze z adresáře Models
        //public DbSet<voteparam>? voteparam { get; set; } // Deklarace jako nullable

    }
}

