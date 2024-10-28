using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace AlcanosApplication.Models
{
    public partial class DBAPIContext : DbContext
    {
        public DBAPIContext() { }

        public DBAPIContext(DbContextOptions<DBAPIContext> options) : base(options) {
        }

        public virtual DbSet<Enfermedad> enfermedad { get; set; } = null!;
        public string connetion = "server=(local);Database=alcanos;Integrated Security=true";
        SqlDataReader leer;
        DataTable tabla = new DataTable();

        public DataTable getList()
        {
            SqlConnection conexion = new SqlConnection(connetion);
            conexion.Open();
            string cadena = "select * from enfermedades";
            SqlCommand comando = new SqlCommand(cadena, conexion);
            leer = comando.ExecuteReader();
            tabla.Load(leer);
            conexion.Close();
            return tabla;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Enfermedad>(entity =>
            {

                entity.HasKey(e => e.id_enfermedad)
                 .HasName("PK__");

                entity.ToTable("enfermedades");

                entity.Property(e => e.nombre_enfermedad).HasMaxLength(30).IsUnicode(false);




            });
            OnModelCreatingPartial(modelBuilder);


        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);




    }
}
