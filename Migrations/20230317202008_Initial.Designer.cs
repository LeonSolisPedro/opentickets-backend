﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using opentickets_backend.Data;

#nullable disable

namespace opentickets_backend.Migrations
{
    [DbContext(typeof(OpenTicketsContext))]
    [Migration("20230317202008_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("opentickets_backend.Data.Computadora", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Disco")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MarcaModel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumeroSerie")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Procesador")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RAM")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SistemaOperativo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TipoComputadora")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Computadoras");
                });

            modelBuilder.Entity("opentickets_backend.Data.Empleado", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("IdComputadora")
                        .HasColumnType("int");

                    b.Property<string>("NombreDepartamento")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NombreEmpleado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdComputadora");

                    b.ToTable("Empleados");
                });

            modelBuilder.Entity("opentickets_backend.Data.Solucion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("IdTicket")
                        .HasColumnType("int");

                    b.Property<bool>("ModificoCompu")
                        .HasColumnType("bit");

                    b.Property<string>("SolucionCampo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdTicket")
                        .IsUnique();

                    b.ToTable("Soluciones");
                });

            modelBuilder.Entity("opentickets_backend.Data.Ticket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("DescripcionProblema")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdComputadora")
                        .HasColumnType("int");

                    b.Property<int>("Prioridad")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdComputadora");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("opentickets_backend.Data.Empleado", b =>
                {
                    b.HasOne("opentickets_backend.Data.Computadora", "Computadora")
                        .WithMany()
                        .HasForeignKey("IdComputadora")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Computadora");
                });

            modelBuilder.Entity("opentickets_backend.Data.Solucion", b =>
                {
                    b.HasOne("opentickets_backend.Data.Ticket", "Ticket")
                        .WithOne("Solucion")
                        .HasForeignKey("opentickets_backend.Data.Solucion", "IdTicket")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ticket");
                });

            modelBuilder.Entity("opentickets_backend.Data.Ticket", b =>
                {
                    b.HasOne("opentickets_backend.Data.Computadora", "Computadora")
                        .WithMany("Tickets")
                        .HasForeignKey("IdComputadora")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Computadora");
                });

            modelBuilder.Entity("opentickets_backend.Data.Computadora", b =>
                {
                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("opentickets_backend.Data.Ticket", b =>
                {
                    b.Navigation("Solucion");
                });
#pragma warning restore 612, 618
        }
    }
}
