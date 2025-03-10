﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WpfQLSV.Models;

#nullable disable

namespace WpfQLSV.Migrations
{
    [DbContext(typeof(StudentMngContext))]
    partial class StudentMngContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WpfQLSV.Models.Class", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClassName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id")
                        .HasName("PK__Classes__3214EC07C12029FE");

                    b.ToTable("Classes");
                });

            modelBuilder.Entity("WpfQLSV.Models.Score", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Dck")
                        .HasColumnType("float")
                        .HasColumnName("DCK");

                    b.Property<double>("Dgk")
                        .HasColumnType("float")
                        .HasColumnName("DGK");

                    b.Property<double>("Dkt")
                        .HasColumnType("float")
                        .HasColumnName("DKT");

                    b.Property<double>("Dtp")
                        .HasColumnType("float")
                        .HasColumnName("DTP");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK__Scores__3214EC0745D722D8");

                    b.HasIndex("StudentId");

                    b.HasIndex("SubjectId");

                    b.ToTable("Scores");
                });

            modelBuilder.Entity("WpfQLSV.Models.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("IdClass")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK__Students__3214EC07719B8EAA");

                    b.HasIndex("IdClass");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("WpfQLSV.Models.StudentsSubject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK__Students__3214EC0754DABCE1");

                    b.HasIndex("StudentId");

                    b.HasIndex("SubjectId");

                    b.ToTable("Students_Subjects", (string)null);
                });

            modelBuilder.Entity("WpfQLSV.Models.Subject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Credit")
                        .HasColumnType("int");

                    b.Property<string>("SubjectName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id")
                        .HasName("PK__Subjects__3214EC071B674E6A");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("WpfQLSV.Models.Score", b =>
                {
                    b.HasOne("WpfQLSV.Models.Student", "Student")
                        .WithMany("Scores")
                        .HasForeignKey("StudentId")
                        .IsRequired()
                        .HasConstraintName("FK__Scores__StudentI__628FA481");

                    b.HasOne("WpfQLSV.Models.Subject", "Subject")
                        .WithMany("Scores")
                        .HasForeignKey("SubjectId")
                        .IsRequired()
                        .HasConstraintName("FK__Scores__SubjectI__6383C8BA");

                    b.Navigation("Student");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("WpfQLSV.Models.Student", b =>
                {
                    b.HasOne("WpfQLSV.Models.Class", "IdClassNavigation")
                        .WithMany("Students")
                        .HasForeignKey("IdClass")
                        .HasConstraintName("FK__Students__IdClas__45F365D3");

                    b.Navigation("IdClassNavigation");
                });

            modelBuilder.Entity("WpfQLSV.Models.StudentsSubject", b =>
                {
                    b.HasOne("WpfQLSV.Models.Student", "Student")
                        .WithMany("StudentsSubjects")
                        .HasForeignKey("StudentId")
                        .IsRequired()
                        .HasConstraintName("FK__Students___Stude__5EBF139D");

                    b.HasOne("WpfQLSV.Models.Subject", "Subject")
                        .WithMany("StudentsSubjects")
                        .HasForeignKey("SubjectId")
                        .IsRequired()
                        .HasConstraintName("FK__Students___Subje__5FB337D6");

                    b.Navigation("Student");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("WpfQLSV.Models.Class", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("WpfQLSV.Models.Student", b =>
                {
                    b.Navigation("Scores");

                    b.Navigation("StudentsSubjects");
                });

            modelBuilder.Entity("WpfQLSV.Models.Subject", b =>
                {
                    b.Navigation("Scores");

                    b.Navigation("StudentsSubjects");
                });
#pragma warning restore 612, 618
        }
    }
}
