﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Tyle.Persistence;

#nullable disable

namespace Tyle.Persistence.Migrations
{
    [DbContext(typeof(TyleDbContext))]
    partial class TyleDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Tyle.Persistence.Attributes.AttributeDao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTimeOffset>("LastUpdateOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("PredicateId")
                        .HasColumnType("int");

                    b.Property<string>("ProvenanceQualifier")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RangeQualifier")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RegularityQualifier")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ScopeQualifier")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UnitMaxCount")
                        .HasColumnType("int");

                    b.Property<int>("UnitMinCount")
                        .HasColumnType("int");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PredicateId");

                    b.ToTable("Attribute");
                });

            modelBuilder.Entity("Tyle.Persistence.Attributes.AttributeUnitDao", b =>
                {
                    b.Property<Guid>("AttributeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("UnitId")
                        .HasColumnType("int");

                    b.HasKey("AttributeId", "UnitId");

                    b.HasIndex("UnitId");

                    b.ToTable("Attribute_Unit");
                });

            modelBuilder.Entity("Tyle.Persistence.Attributes.PredicateDao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Iri")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Source")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Iri")
                        .IsUnique();

                    b.ToTable("Predicate");
                });

            modelBuilder.Entity("Tyle.Persistence.Attributes.UnitDao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Iri")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Source")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Symbol")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.HasIndex("Iri")
                        .IsUnique();

                    b.ToTable("Unit");
                });

            modelBuilder.Entity("Tyle.Persistence.Attributes.ValueConstraintDao", b =>
                {
                    b.Property<Guid>("AttributeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConstraintType")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("DataType")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int?>("MaxCount")
                        .HasColumnType("int");

                    b.Property<bool?>("MaxInclusive")
                        .HasColumnType("bit");

                    b.Property<decimal?>("MaxValue")
                        .HasPrecision(38, 19)
                        .HasColumnType("decimal(38,19)");

                    b.Property<int?>("MinCount")
                        .HasColumnType("int");

                    b.Property<bool?>("MinInclusive")
                        .HasColumnType("bit");

                    b.Property<decimal?>("MinValue")
                        .HasPrecision(38, 19)
                        .HasColumnType("decimal(38,19)");

                    b.Property<string>("Pattern")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Value")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("AttributeId");

                    b.ToTable("ValueConstraint");
                });

            modelBuilder.Entity("Tyle.Persistence.Attributes.ValueListEntryDao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<Guid>("ValueConstraintId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ValueListEntry")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.HasIndex("ValueConstraintId");

                    b.ToTable("ValueListEntry");
                });

            modelBuilder.Entity("Tyle.Persistence.Common.ClassifierDao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Iri")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Source")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Iri")
                        .IsUnique();

                    b.ToTable("Classifier");
                });

            modelBuilder.Entity("Tyle.Persistence.Common.PurposeDao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Iri")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Source")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Iri")
                        .IsUnique();

                    b.ToTable("Purpose");
                });

            modelBuilder.Entity("Tyle.Persistence.Terminals.MediumDao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Iri")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Source")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Iri")
                        .IsUnique();

                    b.ToTable("Medium");
                });

            modelBuilder.Entity("Tyle.Persistence.Attributes.AttributeDao", b =>
                {
                    b.HasOne("Tyle.Persistence.Attributes.PredicateDao", "Predicate")
                        .WithMany()
                        .HasForeignKey("PredicateId");

                    b.Navigation("Predicate");
                });

            modelBuilder.Entity("Tyle.Persistence.Attributes.AttributeUnitDao", b =>
                {
                    b.HasOne("Tyle.Persistence.Attributes.AttributeDao", "Attribute")
                        .WithMany("AttributeUnits")
                        .HasForeignKey("AttributeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tyle.Persistence.Attributes.UnitDao", "Unit")
                        .WithMany()
                        .HasForeignKey("UnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Attribute");

                    b.Navigation("Unit");
                });

            modelBuilder.Entity("Tyle.Persistence.Attributes.ValueConstraintDao", b =>
                {
                    b.HasOne("Tyle.Persistence.Attributes.AttributeDao", "Attribute")
                        .WithOne("ValueConstraint")
                        .HasForeignKey("Tyle.Persistence.Attributes.ValueConstraintDao", "AttributeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Attribute");
                });

            modelBuilder.Entity("Tyle.Persistence.Attributes.ValueListEntryDao", b =>
                {
                    b.HasOne("Tyle.Persistence.Attributes.ValueConstraintDao", "ValueConstraint")
                        .WithMany("ValueList")
                        .HasForeignKey("ValueConstraintId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ValueConstraint");
                });

            modelBuilder.Entity("Tyle.Persistence.Attributes.AttributeDao", b =>
                {
                    b.Navigation("AttributeUnits");

                    b.Navigation("ValueConstraint");
                });

            modelBuilder.Entity("Tyle.Persistence.Attributes.ValueConstraintDao", b =>
                {
                    b.Navigation("ValueList");
                });
#pragma warning restore 612, 618
        }
    }
}
