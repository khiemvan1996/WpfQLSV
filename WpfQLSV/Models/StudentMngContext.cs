using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WpfQLSV.Models;

public partial class StudentMngContext : DbContext
{
    public StudentMngContext()
    {
    }

    public StudentMngContext(DbContextOptions<StudentMngContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Score> Scores { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentsSubject> StudentsSubjects { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=StudentMNG;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Classes__3214EC07C12029FE");

            entity.Property(e => e.ClassName).HasMaxLength(255);
        });

        modelBuilder.Entity<Score>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Scores__3214EC0745D722D8");

            entity.Property(e => e.Dck).HasColumnName("DCK");
            entity.Property(e => e.Dgk).HasColumnName("DGK");
            entity.Property(e => e.Dkt).HasColumnName("DKT");
            entity.Property(e => e.Dtp).HasColumnName("DTP");

            entity.HasOne(d => d.Student).WithMany(p => p.Scores)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Scores__StudentI__628FA481");

            entity.HasOne(d => d.Subject).WithMany(p => p.Scores)
                .HasForeignKey(d => d.SubjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Scores__SubjectI__6383C8BA");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Students__3214EC07719B8EAA");

            entity.Property(e => e.FullName).HasMaxLength(255);

            entity.HasOne(d => d.IdClassNavigation).WithMany(p => p.Students)
                .HasForeignKey(d => d.IdClass)
                .HasConstraintName("FK__Students__IdClas__45F365D3");
        });

        modelBuilder.Entity<StudentsSubject>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Students__3214EC0754DABCE1");

            entity.ToTable("Students_Subjects");

            entity.HasOne(d => d.Student).WithMany(p => p.StudentsSubjects)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Students___Stude__5EBF139D");

            entity.HasOne(d => d.Subject).WithMany(p => p.StudentsSubjects)
                .HasForeignKey(d => d.SubjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Students___Subje__5FB337D6");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Subjects__3214EC071B674E6A");

            entity.Property(e => e.SubjectName).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
