using backend.models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Carpeta> carpeta { get; set; }
        public DbSet<Archivo> archivo { get; set; }
        public DbSet<TablaCarpetas> tablaCarpetas { get; set; }

        public DbSet<User> user { get; set; }

        public DbSet<User_Role> user_Roles { get; set; }
        

    }
}
