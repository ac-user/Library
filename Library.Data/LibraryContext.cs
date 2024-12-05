using System;
using System.Collections.Generic;
using Library.Data.Entities;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace Library.Data;

public partial class LibraryContext : DbContext
{
    public LibraryContext()
    {
    }

    public LibraryContext(DbContextOptions<LibraryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Collection> Collections { get; set; }

    public virtual DbSet<CollectionAssociation> CollectionAssociations { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<Music> Musics { get; set; }

    public virtual DbSet<SubCollectionAssociation> SubCollectionAssociations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.ToTable("Book");
            entity.HasKey("BookId");

            entity.Property(e => e.Artist).IsUnicode(false);
            entity.Property(e => e.Author).IsUnicode(false);
            entity.Property(e => e.Publisher).IsUnicode(false);
            entity.Property(e => e.BookId).ValueGeneratedOnAdd();
            entity.Property(e => e.Description).IsUnicode(false);
            entity.Property(e => e.Isbn)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ISBN");
            entity.Property(e => e.Language)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Series).IsUnicode(false);
            entity.Property(e => e.SubTitle).IsUnicode(false);
            entity.Property(e => e.Summary).IsUnicode(false);
            entity.Property(e => e.Title).IsUnicode(false);
            entity.Property(e => e.Ongoing).HasColumnType("bit");
            entity.Property(e => e.IsActivelyReading).HasColumnType("bit");
            entity.Property(e => e.Genre).IsUnicode(false);
            entity.Property(i => i.Image).HasColumnType("VARBINARY(MAX)");
        });

        modelBuilder.Entity<Collection>(entity =>
        {
            entity.ToTable("Collection");
            entity.HasKey("CollectionId");

            entity.Property(e => e.CollectionId).ValueGeneratedOnAdd();
            entity.Property(e => e.Title).IsUnicode(false);

            entity.HasMany(h => h.SubCollectionAssociations)
                  .WithOne(o => o.Collection)
                  .HasForeignKey(f => f.CollectionId);
            entity.HasMany(h => h.CollectionAssociations)
                  .WithOne(o => o.Collection)
                  .HasForeignKey(f => f.CollectionId);
        });

        modelBuilder.Entity<CollectionAssociation>(entity =>
        {
            entity.ToTable("CollectionAssociation");
            entity.HasKey("CollectionAssociationId");

            entity.Property(e => e.CollectionAssociationId).ValueGeneratedOnAdd();
            entity.Property(e => e.MediaType)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(h => h.Book)
                  .WithMany(o => o.CollectionAssociations)
                  .HasForeignKey(f => f.MediaId)
                  .HasPrincipalKey(p => p.BookId);

            entity.HasOne(h => h.Music)
                  .WithMany(o => o.CollectionAssociations)
                  .HasForeignKey(f => f.MediaId)
                  .HasPrincipalKey(p => p.MusicId);

            entity.HasOne(h => h.Movie)
                  .WithMany(o => o.CollectionAssociations)
                  .HasForeignKey(f => f.MediaId)
                  .HasPrincipalKey(p => p.MovieId);
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.ToTable("Movie");
            entity.HasKey("MovieId");

            entity.Property(e => e.Language)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DateReleased).HasColumnType("datetime");
            entity.Property(e => e.MovieId).ValueGeneratedOnAdd();
            entity.Property(e => e.Series).IsUnicode(false);
            entity.Property(e => e.Summary).IsUnicode(false);
            entity.Property(e => e.Title).IsUnicode(false);
            entity.Property(e => e.Writer).IsUnicode(false);
            entity.Property(e => e.Ongoing).HasColumnType("bit");
            entity.Property(e => e.IsActivelyWatching).HasColumnType("bit");
            entity.Property(e => e.Genre).IsUnicode(false);
            entity.Property(i => i.Image).HasColumnType("VARBINARY(MAX)");

        });

        modelBuilder.Entity<Music>(entity =>
        {
            entity.ToTable("Music");
            entity.HasKey("MusicId");

            entity.Property(e => e.Language)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Album).IsUnicode(false);
            entity.Property(e => e.DatePublished).HasColumnType("datetime");
            entity.Property(e => e.MusicId).ValueGeneratedOnAdd();
            entity.Property(e => e.Singer).IsUnicode(false);
            entity.Property(e => e.Title).IsUnicode(false);
            entity.Property(e => e.Writer).IsUnicode(false);
            entity.Property(e => e.Genre).IsUnicode(false);
            entity.Property(i => i.Image).HasColumnType("VARBINARY(MAX)");

        });

        modelBuilder.Entity<SubCollectionAssociation>(entity =>
        {
            entity.ToTable("SubCollectionAssociation");
            entity.HasKey("SubCollectionAssociationId");

            entity.Property(e => e.SubCollectionAssociationId).ValueGeneratedOnAdd();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
