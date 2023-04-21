using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CrudSample.Models;

public partial class McsVecinosContext : DbContext
{
    public McsVecinosContext()
    {
    }

    public McsVecinosContext(DbContextOptions<McsVecinosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Producto> Productos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS2017;Database=mcsVecinos;Integrated Security=True;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    
        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Producto__3214EC07208340A8");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Costo).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.Nombre).HasMaxLength(300);
            entity.Property(e => e.Precio).HasColumnType("numeric(18, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
