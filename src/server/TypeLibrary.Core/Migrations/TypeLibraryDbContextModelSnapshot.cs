﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TypeLibrary.Data;

#nullable disable

namespace TypeLibrary.Core.Migrations
{
    [DbContext(typeof(TypeLibraryDbContext))]
    partial class TypeLibraryDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Mimirorg.TypeLibrary.Models.Domain.AttributeType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<string>("ContributedBy")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)")
                        .HasColumnName("ContributedBy");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("CreatedBy");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset")
                        .HasColumnName("CreatedOn");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("Description");

                    b.Property<DateTimeOffset>("LastUpdateOn")
                        .HasColumnType("datetimeoffset")
                        .HasColumnName("LastUpdateOn");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Name");

                    b.Property<int?>("PredicateId")
                        .HasColumnType("int");

                    b.Property<string>("ProvenanceQualifier")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("ProvenanceQualifier");

                    b.Property<string>("RangeQualifier")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("RangeQualifier");

                    b.Property<string>("RegularityQualifier")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("RegularityQualifier");

                    b.Property<string>("ScopeQualifier")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("ScopeQualifier");

                    b.Property<int>("UnitMaxCount")
                        .HasColumnType("int")
                        .HasColumnName("UnitMaxCount");

                    b.Property<int>("UnitMinCount")
                        .HasColumnType("int")
                        .HasColumnName("UnitMinCount");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("Version");

                    b.HasKey("Id");

                    b.HasIndex("PredicateId");

                    b.ToTable("Attribute", (string)null);
                });

            modelBuilder.Entity("Mimirorg.TypeLibrary.Models.Domain.AttributeUnitMapping", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<Guid>("AttributeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("UnitId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AttributeId");

                    b.HasIndex("UnitId");

                    b.ToTable("AttributeUnitMapping");
                });

            modelBuilder.Entity("Mimirorg.TypeLibrary.Models.Domain.BlockAttributeTypeReference", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<Guid>("AttributeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BlockId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("MaxCount")
                        .HasColumnType("int")
                        .HasColumnName("MaxCount");

                    b.Property<int>("MinCount")
                        .HasColumnType("int")
                        .HasColumnName("MinCount");

                    b.HasKey("Id");

                    b.HasIndex("AttributeId");

                    b.HasIndex("BlockId");

                    b.ToTable("Block_Attribute", (string)null);
                });

            modelBuilder.Entity("Mimirorg.TypeLibrary.Models.Domain.BlockTerminalTypeReference", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<Guid>("BlockId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Direction")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("Direction");

                    b.Property<int?>("MaxCount")
                        .HasColumnType("int")
                        .HasColumnName("MaxCount");

                    b.Property<int>("MinCount")
                        .HasColumnType("int")
                        .HasColumnName("MinCount");

                    b.Property<Guid>("TerminalId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("BlockId");

                    b.HasIndex("TerminalId");

                    b.ToTable("Block_Terminal", (string)null);
                });

            modelBuilder.Entity("Mimirorg.TypeLibrary.Models.Domain.BlockType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<string>("Aspect")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("Aspect");

                    b.Property<string>("ContributedBy")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)")
                        .HasColumnName("ContributedBy");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("CreatedBy");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset")
                        .HasColumnName("CreatedOn");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("Description");

                    b.Property<DateTimeOffset>("LastUpdateOn")
                        .HasColumnType("datetimeoffset")
                        .HasColumnName("LastUpdateOn");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Name");

                    b.Property<string>("Notation")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Notation");

                    b.Property<int?>("PurposeId")
                        .HasColumnType("int");

                    b.Property<string>("Symbol")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("Symbol");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("Version");

                    b.HasKey("Id");

                    b.HasIndex("PurposeId");

                    b.ToTable("Block", (string)null);
                });

            modelBuilder.Entity("Mimirorg.TypeLibrary.Models.Domain.ClassifierReference", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("Description");

                    b.Property<string>("Iri")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("Iri");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Name");

                    b.Property<string>("Source")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Source");

                    b.HasKey("Id");

                    b.HasIndex("Iri")
                        .IsUnique();

                    b.ToTable("Classifier", (string)null);
                });

            modelBuilder.Entity("Mimirorg.TypeLibrary.Models.Domain.LogLibDm", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasMaxLength(63)
                        .HasColumnType("datetime2")
                        .HasColumnName("Created");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(127)
                        .HasColumnType("nvarchar(127)")
                        .HasColumnName("User");

                    b.Property<string>("LogType")
                        .IsRequired()
                        .HasMaxLength(31)
                        .HasColumnType("nvarchar(31)")
                        .HasColumnName("LogType");

                    b.Property<string>("LogTypeValue")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("LogTypeValue");

                    b.Property<string>("ObjectFirstVersionId")
                        .HasMaxLength(63)
                        .HasColumnType("nvarchar(63)")
                        .HasColumnName("ObjectFirstVersionId");

                    b.Property<string>("ObjectId")
                        .IsRequired()
                        .HasMaxLength(63)
                        .HasColumnType("nvarchar(63)")
                        .HasColumnName("ObjectId");

                    b.Property<string>("ObjectName")
                        .IsRequired()
                        .HasMaxLength(127)
                        .HasColumnType("nvarchar(127)")
                        .HasColumnName("ObjectName");

                    b.Property<string>("ObjectType")
                        .IsRequired()
                        .HasMaxLength(63)
                        .HasColumnType("nvarchar(63)")
                        .HasColumnName("ObjectType");

                    b.Property<string>("ObjectVersion")
                        .HasMaxLength(7)
                        .HasColumnType("nvarchar(7)")
                        .HasColumnName("ObjectVersion");

                    b.HasKey("Id");

                    b.HasIndex("LogType");

                    b.HasIndex("ObjectFirstVersionId");

                    b.HasIndex("ObjectId");

                    b.HasIndex("ObjectType");

                    b.HasIndex("ObjectId", "ObjectFirstVersionId", "ObjectType", "LogType");

                    b.ToTable("Log", (string)null);
                });

            modelBuilder.Entity("Mimirorg.TypeLibrary.Models.Domain.MediumReference", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("Description");

                    b.Property<string>("Iri")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("Iri");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Name");

                    b.Property<string>("Source")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Source");

                    b.HasKey("Id");

                    b.HasIndex("Iri")
                        .IsUnique();

                    b.ToTable("Medium", (string)null);
                });

            modelBuilder.Entity("Mimirorg.TypeLibrary.Models.Domain.PredicateReference", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("Description");

                    b.Property<string>("Iri")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("Iri");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Name");

                    b.Property<string>("Source")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Source");

                    b.HasKey("Id");

                    b.HasIndex("Iri")
                        .IsUnique();

                    b.ToTable("Predicate", (string)null);
                });

            modelBuilder.Entity("Mimirorg.TypeLibrary.Models.Domain.PurposeReference", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("Description");

                    b.Property<string>("Iri")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("Iri");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Name");

                    b.Property<string>("Source")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Source");

                    b.HasKey("Id");

                    b.HasIndex("Iri")
                        .IsUnique();

                    b.ToTable("Purpose", (string)null);
                });

            modelBuilder.Entity("Mimirorg.TypeLibrary.Models.Domain.SymbolLibDm", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(63)
                        .HasColumnType("nvarchar(63)")
                        .HasColumnName("Id");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2")
                        .HasColumnName("Created");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(127)
                        .HasColumnType("nvarchar(127)")
                        .HasColumnName("CreatedBy");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Data");

                    b.Property<string>("Iri")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Iri");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(63)
                        .HasColumnType("nvarchar(63)")
                        .HasColumnName("Name");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(31)
                        .HasColumnType("nvarchar(31)")
                        .HasColumnName("State");

                    b.Property<string>("TypeReference")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("TypeReference");

                    b.HasKey("Id");

                    b.ToTable("Symbol", (string)null);
                });

            modelBuilder.Entity("Mimirorg.TypeLibrary.Models.Domain.TerminalAttributeTypeReference", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<Guid>("AttributeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("MaxCount")
                        .HasColumnType("int")
                        .HasColumnName("MaxCount");

                    b.Property<int>("MinCount")
                        .HasColumnType("int")
                        .HasColumnName("MinCount");

                    b.Property<Guid>("TerminalId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AttributeId");

                    b.HasIndex("TerminalId");

                    b.ToTable("Terminal_Attribute", (string)null);
                });

            modelBuilder.Entity("Mimirorg.TypeLibrary.Models.Domain.TerminalClassifierMapping", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ClassifierId")
                        .HasColumnType("int");

                    b.Property<Guid>("TerminalId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ClassifierId");

                    b.HasIndex("TerminalId");

                    b.ToTable("TerminalClassifierMapping");
                });

            modelBuilder.Entity("Mimirorg.TypeLibrary.Models.Domain.TerminalType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<string>("Aspect")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("Aspect");

                    b.Property<string>("ContributedBy")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)")
                        .HasColumnName("ContributedBy");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("CreatedBy");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset")
                        .HasColumnName("CreatedOn");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("Description");

                    b.Property<DateTimeOffset>("LastUpdateOn")
                        .HasColumnType("datetimeoffset")
                        .HasColumnName("LastUpdateOn");

                    b.Property<int?>("MediumId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Name");

                    b.Property<string>("Notation")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Notation");

                    b.Property<int?>("PurposeId")
                        .HasColumnType("int");

                    b.Property<string>("Qualifier")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("Qualifier");

                    b.Property<string>("Symbol")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("Symbol");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("Version");

                    b.HasKey("Id");

                    b.HasIndex("MediumId");

                    b.HasIndex("PurposeId");

                    b.ToTable("Terminal", (string)null);
                });

            modelBuilder.Entity("Mimirorg.TypeLibrary.Models.Domain.UnitReference", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("Description");

                    b.Property<string>("Iri")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("Iri");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Name");

                    b.Property<string>("Source")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Source");

                    b.Property<string>("Symbol")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("Symbol");

                    b.HasKey("Id");

                    b.HasIndex("Iri")
                        .IsUnique();

                    b.ToTable("Unit", (string)null);
                });

            modelBuilder.Entity("Mimirorg.TypeLibrary.Models.Domain.ValueConstraint", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AllowedValues")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("AllowedValues");

                    b.Property<Guid>("AttributeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConstraintType")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("ConstraintType");

                    b.Property<string>("DataType")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("DataType");

                    b.Property<int?>("MaxCount")
                        .HasColumnType("int")
                        .HasColumnName("MaxCount");

                    b.Property<bool?>("MaxInclusive")
                        .HasColumnType("bit")
                        .HasColumnName("MaxInclusive");

                    b.Property<decimal?>("MaxValue")
                        .HasPrecision(38, 19)
                        .HasColumnType("decimal(38,19)")
                        .HasColumnName("MaxValue");

                    b.Property<int?>("MinCount")
                        .HasColumnType("int")
                        .HasColumnName("MinCount");

                    b.Property<bool?>("MinInclusive")
                        .HasColumnType("bit")
                        .HasColumnName("MinInclusive");

                    b.Property<decimal?>("MinValue")
                        .HasPrecision(38, 19)
                        .HasColumnType("decimal(38,19)")
                        .HasColumnName("MinValue");

                    b.Property<string>("Pattern")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("Pattern");

                    b.Property<string>("Value")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("Value");

                    b.HasKey("Id");

                    b.HasIndex("AttributeId")
                        .IsUnique();

                    b.ToTable("Value_Constraint", (string)null);
                });

            modelBuilder.Entity("Mimirorg.TypeLibrary.Models.Domain.AttributeType", b =>
                {
                    b.HasOne("Mimirorg.TypeLibrary.Models.Domain.PredicateReference", "Predicate")
                        .WithMany()
                        .HasForeignKey("PredicateId");

                    b.Navigation("Predicate");
                });

            modelBuilder.Entity("Mimirorg.TypeLibrary.Models.Domain.AttributeUnitMapping", b =>
                {
                    b.HasOne("Mimirorg.TypeLibrary.Models.Domain.AttributeType", "Attribute")
                        .WithMany("Units")
                        .HasForeignKey("AttributeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Mimirorg.TypeLibrary.Models.Domain.UnitReference", "Unit")
                        .WithMany()
                        .HasForeignKey("UnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Attribute");

                    b.Navigation("Unit");
                });

            modelBuilder.Entity("Mimirorg.TypeLibrary.Models.Domain.BlockAttributeTypeReference", b =>
                {
                    b.HasOne("Mimirorg.TypeLibrary.Models.Domain.AttributeType", "Attribute")
                        .WithMany()
                        .HasForeignKey("AttributeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Mimirorg.TypeLibrary.Models.Domain.BlockType", "Block")
                        .WithMany("BlockAttributes")
                        .HasForeignKey("BlockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Attribute");

                    b.Navigation("Block");
                });

            modelBuilder.Entity("Mimirorg.TypeLibrary.Models.Domain.BlockTerminalTypeReference", b =>
                {
                    b.HasOne("Mimirorg.TypeLibrary.Models.Domain.BlockType", "Block")
                        .WithMany("BlockTerminals")
                        .HasForeignKey("BlockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Mimirorg.TypeLibrary.Models.Domain.TerminalType", "Terminal")
                        .WithMany()
                        .HasForeignKey("TerminalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Block");

                    b.Navigation("Terminal");
                });

            modelBuilder.Entity("Mimirorg.TypeLibrary.Models.Domain.BlockType", b =>
                {
                    b.HasOne("Mimirorg.TypeLibrary.Models.Domain.PurposeReference", "Purpose")
                        .WithMany()
                        .HasForeignKey("PurposeId");

                    b.Navigation("Purpose");
                });

            modelBuilder.Entity("Mimirorg.TypeLibrary.Models.Domain.TerminalAttributeTypeReference", b =>
                {
                    b.HasOne("Mimirorg.TypeLibrary.Models.Domain.AttributeType", "Attribute")
                        .WithMany()
                        .HasForeignKey("AttributeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Mimirorg.TypeLibrary.Models.Domain.TerminalType", "Terminal")
                        .WithMany("TerminalAttributes")
                        .HasForeignKey("TerminalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Attribute");

                    b.Navigation("Terminal");
                });

            modelBuilder.Entity("Mimirorg.TypeLibrary.Models.Domain.TerminalClassifierMapping", b =>
                {
                    b.HasOne("Mimirorg.TypeLibrary.Models.Domain.ClassifierReference", "Classifier")
                        .WithMany()
                        .HasForeignKey("ClassifierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Mimirorg.TypeLibrary.Models.Domain.TerminalType", "Terminal")
                        .WithMany("Classifiers")
                        .HasForeignKey("TerminalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Classifier");

                    b.Navigation("Terminal");
                });

            modelBuilder.Entity("Mimirorg.TypeLibrary.Models.Domain.TerminalType", b =>
                {
                    b.HasOne("Mimirorg.TypeLibrary.Models.Domain.MediumReference", "Medium")
                        .WithMany()
                        .HasForeignKey("MediumId");

                    b.HasOne("Mimirorg.TypeLibrary.Models.Domain.PurposeReference", "Purpose")
                        .WithMany()
                        .HasForeignKey("PurposeId");

                    b.Navigation("Medium");

                    b.Navigation("Purpose");
                });

            modelBuilder.Entity("Mimirorg.TypeLibrary.Models.Domain.ValueConstraint", b =>
                {
                    b.HasOne("Mimirorg.TypeLibrary.Models.Domain.AttributeType", "Attribute")
                        .WithOne("ValueConstraint")
                        .HasForeignKey("Mimirorg.TypeLibrary.Models.Domain.ValueConstraint", "AttributeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Attribute");
                });

            modelBuilder.Entity("Mimirorg.TypeLibrary.Models.Domain.AttributeType", b =>
                {
                    b.Navigation("Units");

                    b.Navigation("ValueConstraint");
                });

            modelBuilder.Entity("Mimirorg.TypeLibrary.Models.Domain.BlockType", b =>
                {
                    b.Navigation("BlockAttributes");

                    b.Navigation("BlockTerminals");
                });

            modelBuilder.Entity("Mimirorg.TypeLibrary.Models.Domain.TerminalType", b =>
                {
                    b.Navigation("Classifiers");

                    b.Navigation("TerminalAttributes");
                });
#pragma warning restore 612, 618
        }
    }
}
