﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NewSIGASE.Models;

namespace NewSIGASE.Migrations
{
    [DbContext(typeof(SIGASEContext))]
    [Migration("20201103004553_Altera_Agendamento_Coluna_Status")]
    partial class Altera_Agendamento_Coluna_Status
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("NewSIGASE.Models.Agendamento", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AprovadorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataAgendada")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataAtualizacaoStatus")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Justificativa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Periodo")
                        .HasColumnType("int");

                    b.Property<Guid>("SalaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid>("UsuarioId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SalaId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Agendamentos");
                });

            modelBuilder.Entity("NewSIGASE.Models.Endereco", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Bairro")
                        .HasColumnType("VARCHAR(255)");

                    b.Property<string>("Cep")
                        .HasColumnType("VARCHAR(8)");

                    b.Property<string>("Cidade")
                        .HasColumnType("VARCHAR(255)");

                    b.Property<string>("Complemento")
                        .HasColumnType("VARCHAR(255)");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Logradouro")
                        .HasColumnType("VARCHAR(255)");

                    b.Property<string>("Numero")
                        .HasColumnType("VARCHAR(20)");

                    b.Property<string>("Pais")
                        .HasColumnType("VARCHAR(2)");

                    b.Property<string>("PontoReferencia")
                        .HasColumnType("VARCHAR(50)");

                    b.Property<string>("UF")
                        .HasColumnType("VARCHAR(2)");

                    b.HasKey("Id");

                    b.ToTable("Enderecos");
                });

            modelBuilder.Entity("NewSIGASE.Models.Equipamento", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Altura")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Comprimento")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Cor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Largura")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Modelo")
                        .IsRequired()
                        .HasColumnType("VARCHAR(255)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("VARCHAR(255)");

                    b.Property<decimal>("Peso")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid?>("SalaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Serial")
                        .IsRequired()
                        .HasColumnType("VARCHAR(255)");

                    b.HasKey("Id");

                    b.HasIndex("SalaId");

                    b.ToTable("Equipamentos");
                });

            modelBuilder.Entity("NewSIGASE.Models.Sala", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Andar")
                        .HasColumnType("int");

                    b.Property<decimal>("Area")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<int>("CapacidadeAlunos")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("IdentificadorSala")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Observacao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Tipo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Salas");
                });

            modelBuilder.Entity("NewSIGASE.Models.Usuario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Documento")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("VARCHAR(255)");

                    b.Property<Guid>("EnderecoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Matricula")
                        .IsRequired()
                        .HasColumnType("VARCHAR(50)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("VARCHAR(255)");

                    b.Property<int>("Perfil")
                        .HasColumnType("int");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("VARCHAR(50)");

                    b.Property<string>("Telefone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EnderecoId");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("NewSIGASE.Models.Agendamento", b =>
                {
                    b.HasOne("NewSIGASE.Models.Sala", "Sala")
                        .WithMany("Agendamentos")
                        .HasForeignKey("SalaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NewSIGASE.Models.Usuario", "Usuario")
                        .WithMany("Agendamentos")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NewSIGASE.Models.Equipamento", b =>
                {
                    b.HasOne("NewSIGASE.Models.Sala", "Sala")
                        .WithMany("Equipamentos")
                        .HasForeignKey("SalaId");
                });

            modelBuilder.Entity("NewSIGASE.Models.Usuario", b =>
                {
                    b.HasOne("NewSIGASE.Models.Endereco", "Endereco")
                        .WithMany()
                        .HasForeignKey("EnderecoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
