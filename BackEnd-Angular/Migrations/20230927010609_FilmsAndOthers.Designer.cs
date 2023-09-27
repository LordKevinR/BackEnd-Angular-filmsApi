﻿// <auto-generated />
using System;
using BackEnd_Angular;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetTopologySuite.Geometries;

#nullable disable

namespace BackEnd_Angular.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230927010609_FilmsAndOthers")]
    partial class FilmsAndOthers
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BackEnd_Angular.Entities.Actor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Biography")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Photo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Actors");
                });

            modelBuilder.Entity("BackEnd_Angular.Entities.Film", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("InTheaters")
                        .HasColumnType("bit");

                    b.Property<string>("Poster")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Summary")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Trailer")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Films");
                });

            modelBuilder.Entity("BackEnd_Angular.Entities.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("BackEnd_Angular.Entities.RelationshipEntities.ActorsFilms", b =>
                {
                    b.Property<int>("ActorId")
                        .HasColumnType("int");

                    b.Property<int>("FilmId")
                        .HasColumnType("int");

                    b.Property<string>("Character")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.HasKey("ActorId", "FilmId");

                    b.HasIndex("FilmId");

                    b.ToTable("ActorsFilms");
                });

            modelBuilder.Entity("BackEnd_Angular.Entities.RelationshipEntities.GenresFilms", b =>
                {
                    b.Property<int>("GenreId")
                        .HasColumnType("int");

                    b.Property<int>("FilmId")
                        .HasColumnType("int");

                    b.HasKey("GenreId", "FilmId");

                    b.HasIndex("FilmId");

                    b.ToTable("GenresFilms");
                });

            modelBuilder.Entity("BackEnd_Angular.Entities.RelationshipEntities.TheatersFilms", b =>
                {
                    b.Property<int>("TheaterId")
                        .HasColumnType("int");

                    b.Property<int>("FilmId")
                        .HasColumnType("int");

                    b.HasKey("TheaterId", "FilmId");

                    b.HasIndex("FilmId");

                    b.ToTable("TheatersFilms");
                });

            modelBuilder.Entity("BackEnd_Angular.Entities.Theater", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<Point>("Location")
                        .HasColumnType("geography");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("nvarchar(75)");

                    b.HasKey("Id");

                    b.ToTable("Theaters");
                });

            modelBuilder.Entity("BackEnd_Angular.Entities.RelationshipEntities.ActorsFilms", b =>
                {
                    b.HasOne("BackEnd_Angular.Entities.Actor", "Actor")
                        .WithMany("ActorsFilms")
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BackEnd_Angular.Entities.Film", "Film")
                        .WithMany("ActorsFilms")
                        .HasForeignKey("FilmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Actor");

                    b.Navigation("Film");
                });

            modelBuilder.Entity("BackEnd_Angular.Entities.RelationshipEntities.GenresFilms", b =>
                {
                    b.HasOne("BackEnd_Angular.Entities.Film", "Film")
                        .WithMany("GenresFilms")
                        .HasForeignKey("FilmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BackEnd_Angular.Entities.Genre", "Genre")
                        .WithMany("GenresFilms")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Film");

                    b.Navigation("Genre");
                });

            modelBuilder.Entity("BackEnd_Angular.Entities.RelationshipEntities.TheatersFilms", b =>
                {
                    b.HasOne("BackEnd_Angular.Entities.Film", "Film")
                        .WithMany("TheatersFilms")
                        .HasForeignKey("FilmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BackEnd_Angular.Entities.Theater", "Theater")
                        .WithMany("TheatersFilms")
                        .HasForeignKey("TheaterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Film");

                    b.Navigation("Theater");
                });

            modelBuilder.Entity("BackEnd_Angular.Entities.Actor", b =>
                {
                    b.Navigation("ActorsFilms");
                });

            modelBuilder.Entity("BackEnd_Angular.Entities.Film", b =>
                {
                    b.Navigation("ActorsFilms");

                    b.Navigation("GenresFilms");

                    b.Navigation("TheatersFilms");
                });

            modelBuilder.Entity("BackEnd_Angular.Entities.Genre", b =>
                {
                    b.Navigation("GenresFilms");
                });

            modelBuilder.Entity("BackEnd_Angular.Entities.Theater", b =>
                {
                    b.Navigation("TheatersFilms");
                });
#pragma warning restore 612, 618
        }
    }
}