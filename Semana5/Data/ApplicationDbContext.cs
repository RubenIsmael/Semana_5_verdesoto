using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Semana5.Models;

namespace Semana5.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

         public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<DetalleFactura> DetalleFacturas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

          
            modelBuilder.Entity<Factura>()
                .HasOne(f => f.Cliente)
                .WithMany(c => c.Facturas)
                .HasForeignKey(f => f.ClienteId);

            modelBuilder.Entity<DetalleFactura>()
                .HasOne(df => df.Factura)
                .WithMany(f => f.DetalleFacturas)
                .HasForeignKey(df => df.FacturaId);

            modelBuilder.Entity<DetalleFactura>()
                .HasOne(df => df.Producto)
                .WithMany(p => p.DetalleFacturas)
                .HasForeignKey(df => df.ProductoId);

            
            modelBuilder.Entity<Producto>()
                .Property(p => p.Precio)
                .HasColumnType("decimal(18,2)") 
                .IsRequired(); 

            modelBuilder.Entity<DetalleFactura>()
                .Property(df => df.PrecioUnitario)
                .HasColumnType("decimal(18,2)")
                .IsRequired(); 
        }
    }
}