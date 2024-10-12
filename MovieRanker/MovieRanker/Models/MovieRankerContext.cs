using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MovieRanker.Models;

public partial class MovieRankerContext : DbContext
{
    public MovieRankerContext()
    {
    }

    public MovieRankerContext(DbContextOptions<MovieRankerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<Person> Persons { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-1T6TSP0\\SQLEXPRESS;Database=MovieRanker;Trusted_Connection=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Movies__3213E83FA8008C0D");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.Genre)
                .HasMaxLength(50)
                .HasColumnName("genre");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.RatingCount)
                .HasDefaultValue(0)
                .HasColumnName("rating_count");
            entity.Property(e => e.Synopsis).HasColumnName("synopsis");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.Year).HasColumnName("year");

            entity.HasMany(d => d.Actors).WithMany(p => p.Movies)
                .UsingEntity<Dictionary<string, object>>(
                    "MovieActor",
                    r => r.HasOne<Person>().WithMany()
                        .HasForeignKey("ActorId")
                        .HasConstraintName("FK__MovieActo__actor__4BAC3F29"),
                    l => l.HasOne<Movie>().WithMany()
                        .HasForeignKey("MovieId")
                        .HasConstraintName("FK__MovieActo__movie__4AB81AF0"),
                    j =>
                    {
                        j.HasKey("MovieId", "ActorId").HasName("PK__MovieAct__DB7FB33290C43304");
                        j.ToTable("MovieActors");
                        j.IndexerProperty<int>("MovieId").HasColumnName("movie_id");
                        j.IndexerProperty<int>("ActorId").HasColumnName("actor_id");
                    });

            entity.HasMany(d => d.Directors).WithMany(p => p.MoviesNavigation)
                .UsingEntity<Dictionary<string, object>>(
                    "MovieDirector",
                    r => r.HasOne<Person>().WithMany()
                        .HasForeignKey("DirectorId")
                        .HasConstraintName("FK__MovieDire__direc__47DBAE45"),
                    l => l.HasOne<Movie>().WithMany()
                        .HasForeignKey("MovieId")
                        .HasConstraintName("FK__MovieDire__movie__46E78A0C"),
                    j =>
                    {
                        j.HasKey("MovieId", "DirectorId").HasName("PK__MovieDir__0C9FF2ADA4AF6375");
                        j.ToTable("MovieDirectors");
                        j.IndexerProperty<int>("MovieId").HasColumnName("movie_id");
                        j.IndexerProperty<int>("DirectorId").HasColumnName("director_id");
                    });
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Persons__3213E83F6263A891");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .HasColumnName("last_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
