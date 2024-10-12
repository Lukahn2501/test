﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MovieRanker.Models;

#nullable disable

namespace MovieRanker.Migrations
{
    [DbContext(typeof(MovieRankerContext))]
    [Migration("20241012135526_UpdateRatingCount")]
    partial class UpdateRatingCount
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MovieActor", b =>
                {
                    b.Property<int>("MovieId")
                        .HasColumnType("int")
                        .HasColumnName("movie_id");

                    b.Property<int>("ActorId")
                        .HasColumnType("int")
                        .HasColumnName("actor_id");

                    b.HasKey("MovieId", "ActorId")
                        .HasName("PK__MovieAct__DB7FB33290C43304");

                    b.HasIndex("ActorId");

                    b.ToTable("MovieActors", (string)null);
                });

            modelBuilder.Entity("MovieDirector", b =>
                {
                    b.Property<int>("MovieId")
                        .HasColumnType("int")
                        .HasColumnName("movie_id");

                    b.Property<int>("DirectorId")
                        .HasColumnType("int")
                        .HasColumnName("director_id");

                    b.HasKey("MovieId", "DirectorId")
                        .HasName("PK__MovieDir__0C9FF2ADA4AF6375");

                    b.HasIndex("DirectorId");

                    b.ToTable("MovieDirectors", (string)null);
                });

            modelBuilder.Entity("MovieRanker.Models.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Duration")
                        .HasColumnType("int")
                        .HasColumnName("duration");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("genre");

                    b.Property<double?>("Rating")
                        .HasColumnType("float")
                        .HasColumnName("rating");

                    b.Property<int>("RatingCount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0)
                        .HasColumnName("rating_count");

                    b.Property<string>("Synopsis")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("synopsis");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("title");

                    b.Property<int>("Year")
                        .HasColumnType("int")
                        .HasColumnName("year");

                    b.HasKey("Id")
                        .HasName("PK__Movies__3213E83FA8008C0D");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("MovieRanker.Models.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("last_name");

                    b.HasKey("Id")
                        .HasName("PK__Persons__3213E83F6263A891");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("MovieActor", b =>
                {
                    b.HasOne("MovieRanker.Models.Person", null)
                        .WithMany()
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__MovieActo__actor__4BAC3F29");

                    b.HasOne("MovieRanker.Models.Movie", null)
                        .WithMany()
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__MovieActo__movie__4AB81AF0");
                });

            modelBuilder.Entity("MovieDirector", b =>
                {
                    b.HasOne("MovieRanker.Models.Person", null)
                        .WithMany()
                        .HasForeignKey("DirectorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__MovieDire__direc__47DBAE45");

                    b.HasOne("MovieRanker.Models.Movie", null)
                        .WithMany()
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__MovieDire__movie__46E78A0C");
                });
#pragma warning restore 612, 618
        }
    }
}