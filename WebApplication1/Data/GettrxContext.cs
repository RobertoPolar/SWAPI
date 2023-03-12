using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data.Models;

namespace WebApplication1.Data;

public partial class GettrxContext : DbContext
{
    public GettrxContext()
    {
    }

    public GettrxContext(DbContextOptions<GettrxContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<Planet> Planets { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=localhost;Database=GETTRX;User Id=sa;Password=n3gr014; Encrypt=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PEOPLE__3213E83F5F0A70FB");

            entity.ToTable("PEOPLE");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.BirthYear)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("birth_year");
            entity.Property(e => e.Created)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("created");
            entity.Property(e => e.Edited)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("edited");
            entity.Property(e => e.EyeColor)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("eye_color");
            entity.Property(e => e.Gender)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("gender");
            entity.Property(e => e.HairColor)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("hair_color");
            entity.Property(e => e.Height)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("height");
            entity.Property(e => e.Homeworld)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("homeworld");
            entity.Property(e => e.Mass)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("mass");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.SkinColor)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("skin_color");
            entity.Property(e => e.Url)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("url");
        });

        modelBuilder.Entity<Planet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PLANETS__3213E83F4F5C217B");

            entity.ToTable("PLANETS");

            entity.Property(e => e.Climate)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("climate");
            entity.Property(e => e.Diameter)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("diameter");
            entity.Property(e => e.Gravity)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("gravity");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Population)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("population");
            entity.Property(e => e.Residents)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("residents");
            entity.Property(e => e.Terrain)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("terrain");
            entity.Property(e => e.Url)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("url");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
