using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

#nullable disable

namespace RateMyMovie.Models
{
    public partial class MovieDbContext : IdentityDbContext<IdentityUser>
    {
        public MovieDbContext()
        {
        }

        public MovieDbContext(DbContextOptions<MovieDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CommentSet> CommentSets { get; set; }
        public virtual DbSet<GenreMovie> GenreMovies { get; set; }
        public virtual DbSet<GenreSet> GenreSets { get; set; }
        public virtual DbSet<MovieSet> MovieSets { get; set; }
        public virtual DbSet<UserSet> UserSets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=MovieDBConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "French_CI_AS");

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CommentSet>(entity =>
            {
                entity.ToTable("CommentSet");

                entity.HasIndex(e => e.MovieId, "IX_FK_NoteMovie");

                entity.HasIndex(e => e.UserId, "IX_FK_UserNote");

                entity.Property(e => e.Message).IsRequired();

                entity.Property(e => e.MovieId).HasColumnName("Movie_Id");

                entity.Property(e => e.UserId).HasColumnName("User_Id");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.CommentSets)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NoteMovie");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CommentSets)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserNote");
            });

            modelBuilder.Entity<GenreMovie>(entity =>
            {
                entity.HasKey(e => new { e.GenreId, e.MovieId });

                entity.ToTable("GenreMovie");

                entity.HasIndex(e => e.MovieId, "IX_FK_GenreMovie_Movie");

                entity.Property(e => e.GenreId).HasColumnName("Genre_Id");

                entity.Property(e => e.MovieId).HasColumnName("Movie_Id");

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.GenreMovies)
                    .HasForeignKey(d => d.GenreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GenreMovie_Genre");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.GenreMovies)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GenreMovie_Movie");
            });

            modelBuilder.Entity<GenreSet>(entity =>
            {
                entity.ToTable("GenreSet");

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<MovieSet>(entity =>
            {
                entity.ToTable("MovieSet");

                entity.Property(e => e.Title).IsRequired();

                entity.Property(e => e.Synopsis).IsRequired();
            });

            modelBuilder.Entity<GenreMovie>()
            .HasKey(t => new { t.GenreId, t.MovieId });

            modelBuilder.Entity<GenreMovie>()
                .HasOne(g => g.Genre)
                .WithMany(p => p.GenreMovies)
                .HasForeignKey(g => g.GenreId);

            modelBuilder.Entity<GenreMovie>()
                .HasOne(m => m.Movie)
                .WithMany(t => t.GenreMovies)
                .HasForeignKey(m => m.MovieId);

            modelBuilder.Entity<UserSet>(entity =>
            {
                entity.ToTable("UserSet");

                entity.Property(e => e.Password).IsRequired();

                entity.Property(e => e.Username).IsRequired();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
