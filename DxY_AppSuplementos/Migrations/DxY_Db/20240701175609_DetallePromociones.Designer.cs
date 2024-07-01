﻿// <auto-generated />
using System;
using DxY_AppSuplementos.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DxY_AppSuplementos.Migrations.DxY_Db
{
    [DbContext(typeof(DxY_DbContext))]
    [Migration("20240701175609_DetallePromociones")]
    partial class DetallePromociones
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DxY_AppSuplementos.Models.Categoria", b =>
                {
                    b.Property<int>("CategoriaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoriaID"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Disponibilidad")
                        .HasColumnType("int");

                    b.Property<bool>("Eliminado")
                        .HasColumnType("bit");

                    b.Property<int>("FechaRegistro")
                        .HasColumnType("int");

                    b.HasKey("CategoriaID");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("DxY_AppSuplementos.Models.DetallePromocion", b =>
                {
                    b.Property<int>("DetallePromocionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DetallePromocionID"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<int>("Disponibilidad")
                        .HasColumnType("int");

                    b.Property<bool>("Eliminado")
                        .HasColumnType("bit");

                    b.Property<byte[]>("Imagen")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("NombreImagen")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductoID")
                        .HasColumnType("int");

                    b.Property<int>("PromocionID")
                        .HasColumnType("int");

                    b.Property<string>("TipoImagen")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DetallePromocionID");

                    b.HasIndex("ProductoID");

                    b.HasIndex("PromocionID");

                    b.ToTable("DetallePromociones");
                });

            modelBuilder.Entity("DxY_AppSuplementos.Models.Producto", b =>
                {
                    b.Property<int>("ProductoID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductoID"));

                    b.Property<int>("CategoriaID")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<int>("Disponibilidad")
                        .HasColumnType("int");

                    b.Property<bool>("Eliminado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("Imagen")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("NombreImagen")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("PrecioCompra")
                        .HasMaxLength(25)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PrecioVenta")
                        .HasMaxLength(25)
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.Property<string>("TipoImagen")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductoID");

                    b.HasIndex("CategoriaID");

                    b.ToTable("Productos");
                });

            modelBuilder.Entity("DxY_AppSuplementos.Models.Promocion", b =>
                {
                    b.Property<int>("PromocionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PromocionID"));

                    b.Property<bool>("Eliminado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<decimal>("TotalAPagar")
                        .HasMaxLength(25)
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("PromocionID");

                    b.ToTable("Promociones");
                });

            modelBuilder.Entity("DxY_AppSuplementos.Models.DetallePromocion", b =>
                {
                    b.HasOne("DxY_AppSuplementos.Models.Producto", "Producto")
                        .WithMany("DetallePromociones")
                        .HasForeignKey("ProductoID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DxY_AppSuplementos.Models.Promocion", "Promocion")
                        .WithMany("DetallePromociones")
                        .HasForeignKey("PromocionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Producto");

                    b.Navigation("Promocion");
                });

            modelBuilder.Entity("DxY_AppSuplementos.Models.Producto", b =>
                {
                    b.HasOne("DxY_AppSuplementos.Models.Categoria", "Categoria")
                        .WithMany("Productos")
                        .HasForeignKey("CategoriaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categoria");
                });

            modelBuilder.Entity("DxY_AppSuplementos.Models.Categoria", b =>
                {
                    b.Navigation("Productos");
                });

            modelBuilder.Entity("DxY_AppSuplementos.Models.Producto", b =>
                {
                    b.Navigation("DetallePromociones");
                });

            modelBuilder.Entity("DxY_AppSuplementos.Models.Promocion", b =>
                {
                    b.Navigation("DetallePromociones");
                });
#pragma warning restore 612, 618
        }
    }
}
