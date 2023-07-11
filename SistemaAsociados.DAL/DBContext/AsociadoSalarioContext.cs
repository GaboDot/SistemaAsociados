using Microsoft.EntityFrameworkCore;
using SistemaAsociados.Model;

namespace SistemaAsociados.DAL.DBContext;

public partial class AsociadoSalarioContext : DbContext
{
    public AsociadoSalarioContext()
    {
    }

    public AsociadoSalarioContext(DbContextOptions<AsociadoSalarioContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Asociado> Asociados { get; set; }

    public virtual DbSet<Departamento> Departamentos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asociado>(entity =>
        {
            entity.HasKey(e => e.IdAsociado);

            entity.ToTable("Asociado");

            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.FechaIngreso).HasColumnType("datetime");
            entity.Property(e => e.FkIdDepartamento).HasColumnName("FK_IdDepartamento");
            entity.Property(e => e.Nombre)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Salario).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.FkIdDepartamentoNavigation).WithMany(p => p.Asociados)
                .HasForeignKey(d => d.FkIdDepartamento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Asociado_Departamento");
        });

        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.HasKey(e => e.IdDepartamento);

            entity.ToTable("Departamento");

            entity.Property(e => e.Nombre)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.UltimoAumento)
                .HasMaxLength(8)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario);

            entity.ToTable("Usuario");

            entity.Property(e => e.Email).HasMaxLength(25);
            entity.Property(e => e.FkIdAsociado).HasColumnName("FK_IdAsociado");

            entity.HasOne(d => d.FkIdAsociadoNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.FkIdAsociado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuario_Asociado");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Menu");

            entity.Property(e => e.Etiqueta)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Icono)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.IdMenu)
                .ValueGeneratedOnAdd()
                .HasColumnName("idMenu");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
