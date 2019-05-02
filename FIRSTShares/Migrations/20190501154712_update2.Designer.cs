﻿// <auto-generated />
using System;
using FIRSTShares.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FIRSTShares.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20190501154712_update2")]
    partial class update2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FIRSTShares.Entities.Cargo", b =>
                {
                    b.Property<int>("CargoId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Excluido");

                    b.Property<int>("Tipo");

                    b.HasKey("CargoId");

                    b.ToTable("Cargos");
                });

            modelBuilder.Entity("FIRSTShares.Entities.Permissao", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CargoId");

                    b.Property<bool>("Excluido");

                    b.Property<string>("Nome");

                    b.HasKey("ID");

                    b.HasIndex("CargoId");

                    b.ToTable("Permissoes");
                });

            modelBuilder.Entity("FIRSTShares.Entities.Time", b =>
                {
                    b.Property<int>("TimeId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CodPais");

                    b.Property<bool>("Excluido");

                    b.Property<string>("Nome");

                    b.Property<string>("Numero");

                    b.Property<string>("Pais");

                    b.HasKey("TimeId");

                    b.ToTable("Times");
                });

            modelBuilder.Entity("FIRSTShares.Entities.Usuario", b =>
                {
                    b.Property<int>("UsuarioId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CargoId");

                    b.Property<int>("CargoTime");

                    b.Property<DateTime>("DataCriacao");

                    b.Property<string>("Email");

                    b.Property<bool>("Excluido");

                    b.Property<string>("Nome");

                    b.Property<string>("Senha");

                    b.Property<int>("TimeId");

                    b.HasKey("UsuarioId");

                    b.HasIndex("CargoId");

                    b.HasIndex("TimeId");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("FIRSTShares.Entities.Permissao", b =>
                {
                    b.HasOne("FIRSTShares.Entities.Cargo")
                        .WithMany("Permissoes")
                        .HasForeignKey("CargoId");
                });

            modelBuilder.Entity("FIRSTShares.Entities.Usuario", b =>
                {
                    b.HasOne("FIRSTShares.Entities.Cargo", "Cargo")
                        .WithMany()
                        .HasForeignKey("CargoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FIRSTShares.Entities.Time", "Time")
                        .WithMany()
                        .HasForeignKey("TimeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
