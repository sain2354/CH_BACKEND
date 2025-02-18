﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CH_BACKEND.DBCalzadosHuancayo;

public partial class _DbContextCalzadosHuancayo : DbContext
{
    public _DbContextCalzadosHuancayo()
    {
    }

    public _DbContextCalzadosHuancayo(DbContextOptions<_DbContextCalzadosHuancayo> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categoria { get; set; }

    public virtual DbSet<DetalleVenta> DetalleVenta { get; set; }

    public virtual DbSet<Devolucion> Devoluciones { get; set; }

    public virtual DbSet<Empresa> Empresas { get; set; }

    public virtual DbSet<HistorialInventario> HistorialInventarios { get; set; }

    public virtual DbSet<Inventario> Inventarios { get; set; }

    public virtual DbSet<MedioPago> MedioPagos { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Promocion> Promocions { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<SubCategoria> SubCategoria { get; set; }

    public virtual DbSet<Talla> Tallas { get; set; }

    public virtual DbSet<TallaProducto> TallaProductos { get; set; }

    public virtual DbSet<UnidadMedida> UnidadMedida { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<UsuarioRol> UsuarioRols { get; set; }

    public virtual DbSet<Venta> Venta { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-6HLGI3JI\\SQLEXPRESS;Initial Catalog=Calzados;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__Categori__CD54BC5A95EA531F");
        });

        modelBuilder.Entity<DetalleVenta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DetalleV__3213E83FB2E9106F");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetalleVenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetalleVe__id_pr__6477ECF3");

            entity.HasOne(d => d.IdUnidadMedidaNavigation).WithMany(p => p.DetalleVenta).HasConstraintName("FK__DetalleVe__id_un__656C112C");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.DetalleVenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetalleVe__id_ve__6383C8BA");
        });

        modelBuilder.Entity<Devolucion>(entity =>
        {
            entity.HasKey(e => e.IdDevolucion).HasName("PK__Devoluci__0BBAEF8D371B6290");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Devoluciones)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Devolucio__id_pr__6EF57B66");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.Devoluciones)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Devolucio__id_ve__6E01572D");
        });

        modelBuilder.Entity<Empresa>(entity =>
        {
            entity.HasKey(e => e.IdEmpresa).HasName("PK__Empresa__4A0B7E2C00D3FFD2");
        });

        modelBuilder.Entity<HistorialInventario>(entity =>
        {
            entity.HasKey(e => e.IdMovimiento).HasName("PK__Historia__2A071C24B44D7A36");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.HistorialInventarios)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Historial__id_pr__02084FDA");

            entity.HasOne(d => d.UsuarioResponsableNavigation).WithMany(p => p.HistorialInventarios).HasConstraintName("FK__Historial__usuar__02FC7413");
        });

        modelBuilder.Entity<Inventario>(entity =>
        {
            entity.HasKey(e => e.IdInventario).HasName("PK__Inventar__013AEB511DD5E6B9");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Inventarios)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Inventari__id_pr__71D1E811");
        });

        modelBuilder.Entity<MedioPago>(entity =>
        {
            entity.HasKey(e => e.IdMedioPago).HasName("PK__MedioPag__19467AD5802A4AB8");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.IdPago).HasName("PK__Pago__0941B074510023E3");

            entity.HasOne(d => d.IdMedioPagoNavigation).WithMany(p => p.Pagos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pago__id_medio_p__6B24EA82");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.Pagos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pago__id_venta__6A30C649");
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.IdPersona).HasName("PK__Persona__2EC8D2ACBFB74037");

            entity.Property(e => e.TipoPersona).HasDefaultValue("Cliente");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__Producto__FF341C0D54E1F0E5");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Productos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Producto__id_cat__5070F446");

            entity.HasOne(d => d.IdSubCategoriaNavigation).WithMany(p => p.Productos).HasConstraintName("FK__Producto__id_sub__5165187F");

            entity.HasOne(d => d.IdUnidadMedidaNavigation).WithMany(p => p.Productos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Producto__id_uni__52593CB8");

            entity.HasMany(d => d.IdPromocions).WithMany(p => p.IdProductos)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductoPromocion",
                    r => r.HasOne<Promocion>().WithMany()
                        .HasForeignKey("IdPromocion")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Producto___id_pr__778AC167"),
                    l => l.HasOne<Producto>().WithMany()
                        .HasForeignKey("IdProducto")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Producto___id_pr__76969D2E"),
                    j =>
                    {
                        j.HasKey("IdProducto", "IdPromocion").HasName("PK__Producto__E0BD2C8374DEB058");
                        j.ToTable("Producto_Promocion");
                        j.IndexerProperty<int>("IdProducto").HasColumnName("id_producto");
                        j.IndexerProperty<int>("IdPromocion").HasColumnName("id_promocion");
                    });
        });

        modelBuilder.Entity<Promocion>(entity =>
        {
            entity.HasKey(e => e.IdPromocion).HasName("PK__Promocio__F89308E0881B49D8");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Rol__6ABCB5E05CD9B680");
        });

        modelBuilder.Entity<SubCategoria>(entity =>
        {
            entity.HasKey(e => e.IdSubCategoria).HasName("PK__SubCateg__52A25E8A2A196AFE");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.SubCategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SubCatego__id_ca__4BAC3F29");
        });

        modelBuilder.Entity<Talla>(entity =>
        {
            entity.HasKey(e => e.IdTalla).HasName("PK__Talla__C16F403DA58B752D");
        });

        modelBuilder.Entity<TallaProducto>(entity =>
        {
            entity.HasKey(e => new { e.IdProducto, e.IdTalla }).HasName("PK__Talla_Pr__3322E80EFD5B2D18");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.TallaProductos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Talla_Pro__id_pr__7E37BEF6");

            entity.HasOne(d => d.IdTallaNavigation).WithMany(p => p.TallaProductos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Talla_Pro__id_ta__7F2BE32F");
        });

        modelBuilder.Entity<UnidadMedida>(entity =>
        {
            entity.HasKey(e => e.IdUnidadMedida).HasName("PK__UnidadMe__2F721BD3014DED1B");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__4E3E04AD691DD7B6");
        });

        modelBuilder.Entity<UsuarioRol>(entity =>
        {
            entity.HasKey(e => new { e.IdUsuario, e.IdRol }).HasName("PK__Usuario___5895CFF3A0ACAA9D");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.UsuarioRols)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Usuario_R__id_ro__5DCAEF64");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.UsuarioRols)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Usuario_R__id_us__5CD6CB2B");
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.IdVenta).HasName("PK__Venta__BC1240BD26422425");

            entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.Venta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Venta_Persona");
        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
