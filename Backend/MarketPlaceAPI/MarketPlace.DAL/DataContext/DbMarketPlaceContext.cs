using System;
using System.Collections.Generic;
using MarketPlace.Models;
using Microsoft.EntityFrameworkCore;

namespace MarketPlace.DAL.DataContext;

public partial class DbMarketPlaceContext : DbContext
{
    public DbMarketPlaceContext()
    {
    }

    public DbMarketPlaceContext(DbContextOptions<DbMarketPlaceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cabezera> Cabezeras { get; set; }

    public virtual DbSet<Categoria> Categoria { get; set; }

    public virtual DbSet<Detalle> Detalles { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<RolUsuario> RolUsuarios { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cabezera>(entity =>
        {
            entity.HasKey(e => e.IdCompra).HasName("PK__Cabezera__0A5CDB5CE2073E5E");

            entity.ToTable("Cabezera");

            entity.Property(e => e.Fecha).HasColumnType("date");
            entity.Property(e => e.Total).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.UsuarioCedula)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.UsuarioIdUsuarioNavigation).WithMany(p => p.Cabezeras)
                .HasForeignKey(d => d.UsuarioIdUsuario)
                .HasConstraintName("FK__Cabezera__Usuari__46E78A0C");
        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__Categori__A3C02A10D1520959");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Detalle>(entity =>
        {
            entity.HasKey(e => e.IdDetalle).HasName("PK__Detalle__E43646A564599BB1");

            entity.ToTable("Detalle");

            entity.Property(e => e.Total).HasColumnType("decimal(12, 2)");

            entity.HasOne(d => d.CabezeraIdCompraNavigation).WithMany(p => p.Detalles)
                .HasForeignKey(d => d.CabezeraIdCompra)
                .HasConstraintName("FK__Detalle__Cabezer__5FB337D6");

            entity.HasOne(d => d.ProductoIdProductoNavigation).WithMany(p => p.Detalles)
                .HasForeignKey(d => d.ProductoIdProducto)
                .HasConstraintName("FK__Detalle__Product__5EBF139D");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__Producto__09889210AA5A5879");

            entity.ToTable("Producto");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(350)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.UrlImagen)
                .HasMaxLength(550)
                .IsUnicode(false);

            entity.HasOne(d => d.CategoriaIdCategoriaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.CategoriaIdCategoria)
                .HasConstraintName("FK__Producto__Catego__5BE2A6F2");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Rol__2A49584C0B62C97A");

            entity.ToTable("Rol");

            entity.Property(e => e.Nombre)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<RolUsuario>(entity =>
        {
            entity
                //.HasNoKey()
                .ToTable("RolUsuario");

            entity.HasOne(d => d.RolIdRolNavigation).WithMany()
                .HasForeignKey(d => d.RolIdRol)
                .HasConstraintName("FK__RolUsuari__RolId__3C69FB99");

            entity.HasOne(d => d.UsuarioIdUsuarioNavigation).WithMany()
                .HasForeignKey(d => d.UsuarioIdUsuario)
                .HasConstraintName("FK__RolUsuari__Usuar__3D5E1FD2");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF97938B111D");

            entity.ToTable("Usuario");

            entity.Property(e => e.Cedula)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Contraseña)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.CorreoElectronico)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
