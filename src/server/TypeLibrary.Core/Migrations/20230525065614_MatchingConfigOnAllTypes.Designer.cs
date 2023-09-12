﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TypeLibrary.Data;

#nullable disable

namespace TypeLibrary.Core.Migrations
{
    [DbContext(typeof(TypeLibraryDbContext))]
    [Migration("20230525065614_MatchingConfigOnAllTypes")]
    partial class MatchingConfigOnAllTypes
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TypeLibrary.Data.Models.AspectObjectAttributeLibDm", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AspectObjectId")
                        .HasColumnType("nvarchar(63)");

                    b.Property<string>("AttributeId")
                        .HasColumnType("nvarchar(63)");

                    b.HasKey("Id");

                    b.HasIndex("AspectObjectId");

                    b.HasIndex("AttributeId");

                    b.ToTable("AspectObject_Attribute", (string)null);
                });

            modelBuilder.Entity("TypeLibrary.Data.Models.AspectObjectLibDm", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(63)
                        .HasColumnType("nvarchar(63)")
                        .HasColumnName("Id");

                    b.Property<string>("Aspect")
                        .IsRequired()
                        .HasMaxLength(31)
                        .HasColumnType("nvarchar(31)")
                        .HasColumnName("Aspect");

                    b.Property<int>("CompanyId")
                        .HasColumnType("int")
                        .HasColumnName("CompanyId");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc))
                        .HasColumnName("Created");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(127)
                        .HasColumnType("nvarchar(127)")
                        .HasColumnName("CreatedBy");

                    b.Property<string>("Description")
                        .HasMaxLength(511)
                        .HasColumnType("nvarchar(511)")
                        .HasColumnName("Description");

                    b.Property<string>("FirstVersionId")
                        .IsRequired()
                        .HasMaxLength(63)
                        .HasColumnType("nvarchar(63)")
                        .HasColumnName("FirstVersionId");

                    b.Property<string>("Iri")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Iri");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(127)
                        .HasColumnType("nvarchar(127)")
                        .HasColumnName("Name");

                    b.Property<string>("PurposeName")
                        .IsRequired()
                        .HasMaxLength(127)
                        .HasColumnType("nvarchar(127)")
                        .HasColumnName("PurposeName");

                    b.Property<string>("RdsId")
                        .IsRequired()
                        .HasColumnType("nvarchar(63)");

                    b.Property<string>("SelectedAttributePredefined")
                        .HasColumnType("nvarchar(MAX)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(31)
                        .HasColumnType("nvarchar(31)")
                        .HasColumnName("State");

                    b.Property<string>("Symbol")
                        .HasMaxLength(127)
                        .HasColumnType("nvarchar(127)")
                        .HasColumnName("Symbol");

                    b.Property<string>("TypeReference")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("TypeReference");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasMaxLength(7)
                        .HasColumnType("nvarchar(7)")
                        .HasColumnName("Version");

                    b.HasKey("Id");

                    b.HasIndex("FirstVersionId");

                    b.HasIndex("RdsId");

                    b.HasIndex("State");

                    b.HasIndex("State", "Aspect");

                    b.ToTable("AspectObject", (string)null);
                });

            modelBuilder.Entity("TypeLibrary.Data.Models.AspectObjectTerminalLibDm", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(63)
                        .HasColumnType("nvarchar(63)")
                        .HasColumnName("Id");

                    b.Property<string>("AspectObjectId")
                        .HasColumnType("nvarchar(63)");

                    b.Property<string>("ConnectorDirection")
                        .IsRequired()
                        .HasMaxLength(31)
                        .HasColumnType("nvarchar(31)")
                        .HasColumnName("ConnectorDirection");

                    b.Property<int>("MaxQuantity")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(2147483647)
                        .HasColumnName("MaxQuantity");

                    b.Property<int>("MinQuantity")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1)
                        .HasColumnName("MinQuantity");

                    b.Property<string>("TerminalId")
                        .HasColumnType("nvarchar(63)");

                    b.HasKey("Id");

                    b.HasIndex("AspectObjectId");

                    b.HasIndex("TerminalId");

                    b.ToTable("AspectObject_Terminal", (string)null);
                });

            modelBuilder.Entity("TypeLibrary.Data.Models.AttributeLibDm", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(63)
                        .HasColumnType("nvarchar(63)")
                        .HasColumnName("Id");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc))
                        .HasColumnName("Created");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(127)
                        .HasColumnType("nvarchar(127)")
                        .HasColumnName("CreatedBy");

                    b.Property<string>("Description")
                        .HasMaxLength(511)
                        .HasColumnType("nvarchar(511)")
                        .HasColumnName("Description");

                    b.Property<string>("Iri")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Iri");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(127)
                        .HasColumnType("nvarchar(127)")
                        .HasColumnName("Name");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(31)
                        .HasColumnType("nvarchar(31)")
                        .HasColumnName("State");

                    b.Property<string>("TypeReference")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("TypeReference");

                    b.HasKey("Id");

                    b.HasIndex("State");

                    b.ToTable("Attribute", (string)null);
                });

            modelBuilder.Entity("TypeLibrary.Data.Models.AttributePredefinedLibDm", b =>
                {
                    b.Property<string>("Key")
                        .HasMaxLength(127)
                        .HasColumnType("nvarchar(127)")
                        .HasColumnName("Key");

                    b.Property<string>("Aspect")
                        .IsRequired()
                        .HasMaxLength(31)
                        .HasColumnType("nvarchar(31)")
                        .HasColumnName("Aspect");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2")
                        .HasColumnName("Created");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(127)
                        .HasColumnType("nvarchar(127)")
                        .HasColumnName("CreatedBy");

                    b.Property<string>("Iri")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Iri");

                    b.Property<bool>("IsMultiSelect")
                        .HasColumnType("bit")
                        .HasColumnName("IsMultiSelect");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(31)
                        .HasColumnType("nvarchar(31)")
                        .HasColumnName("State");

                    b.Property<string>("TypeReference")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("TypeReference");

                    b.Property<string>("ValueStringList")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ValueStringList");

                    b.HasKey("Key");

                    b.ToTable("AttributePredefined", (string)null);
                });

            modelBuilder.Entity("TypeLibrary.Data.Models.AttributeUnitLibDm", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(63)
                        .HasColumnType("nvarchar(63)")
                        .HasColumnName("Id");

                    b.Property<string>("AttributeId")
                        .HasColumnType("nvarchar(63)");

                    b.Property<bool>("IsDefault")
                        .HasColumnType("bit")
                        .HasColumnName("IsDefault");

                    b.Property<string>("UnitId")
                        .HasColumnType("nvarchar(63)");

                    b.HasKey("Id");

                    b.HasIndex("AttributeId");

                    b.HasIndex("UnitId");

                    b.ToTable("Attribute_Unit", (string)null);
                });

            modelBuilder.Entity("TypeLibrary.Data.Models.CategoryLibDm", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(63)
                        .HasColumnType("nvarchar(63)")
                        .HasColumnName("Id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("Category", (string)null);
                });

            modelBuilder.Entity("TypeLibrary.Data.Models.LogLibDm", b =>
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
                        .HasMaxLength(63)
                        .HasColumnType("nvarchar(63)")
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

            modelBuilder.Entity("TypeLibrary.Data.Models.QuantityDatumLibDm", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(63)
                        .HasColumnType("nvarchar(63)")
                        .HasColumnName("Id");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc))
                        .HasColumnName("Created");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(127)
                        .HasColumnType("nvarchar(127)")
                        .HasColumnName("CreatedBy");

                    b.Property<string>("Description")
                        .HasMaxLength(511)
                        .HasColumnType("nvarchar(511)")
                        .HasColumnName("Description");

                    b.Property<string>("Iri")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Iri");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(127)
                        .HasColumnType("nvarchar(127)")
                        .HasColumnName("Name");

                    b.Property<string>("QuantityDatumType")
                        .IsRequired()
                        .HasMaxLength(63)
                        .HasColumnType("nvarchar(63)")
                        .HasColumnName("QuantityDatumType");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(31)
                        .HasColumnType("nvarchar(31)")
                        .HasColumnName("State");

                    b.Property<string>("TypeReference")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("TypeReference");

                    b.HasKey("Id");

                    b.HasIndex("State");

                    b.ToTable("QuantityDatum", (string)null);
                });

            modelBuilder.Entity("TypeLibrary.Data.Models.RdsLibDm", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(63)
                        .HasColumnType("nvarchar(63)")
                        .HasColumnName("Id");

                    b.Property<string>("CategoryId")
                        .IsRequired()
                        .HasColumnType("nvarchar(63)");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc))
                        .HasColumnName("Created");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(127)
                        .HasColumnType("nvarchar(127)")
                        .HasColumnName("CreatedBy");

                    b.Property<string>("Description")
                        .HasMaxLength(511)
                        .HasColumnType("nvarchar(511)")
                        .HasColumnName("Description");

                    b.Property<string>("Iri")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Iri");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(127)
                        .HasColumnType("nvarchar(127)")
                        .HasColumnName("Name");

                    b.Property<string>("RdsCode")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasColumnName("RdsCode");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(31)
                        .HasColumnType("nvarchar(31)")
                        .HasColumnName("State");

                    b.Property<string>("TypeReference")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("TypeReference");

                    b.HasKey("Id");

                    b.HasAlternateKey("RdsCode", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("State");

                    b.ToTable("Rds", (string)null);
                });

            modelBuilder.Entity("TypeLibrary.Data.Models.SymbolLibDm", b =>
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
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("TypeReference");

                    b.HasKey("Id");

                    b.ToTable("Symbol", (string)null);
                });

            modelBuilder.Entity("TypeLibrary.Data.Models.TerminalAttributeLibDm", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AttributeId")
                        .HasColumnType("nvarchar(63)");

                    b.Property<string>("TerminalId")
                        .HasColumnType("nvarchar(63)");

                    b.HasKey("Id");

                    b.HasIndex("AttributeId");

                    b.HasIndex("TerminalId");

                    b.ToTable("Terminal_Attribute", (string)null);
                });

            modelBuilder.Entity("TypeLibrary.Data.Models.TerminalLibDm", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(63)
                        .HasColumnType("nvarchar(63)")
                        .HasColumnName("Id");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasMaxLength(7)
                        .HasColumnType("nvarchar(7)")
                        .HasColumnName("Color");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc))
                        .HasColumnName("Created");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(127)
                        .HasColumnType("nvarchar(127)")
                        .HasColumnName("CreatedBy");

                    b.Property<string>("Description")
                        .HasMaxLength(511)
                        .HasColumnType("nvarchar(511)")
                        .HasColumnName("Description");

                    b.Property<string>("Iri")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Iri");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(127)
                        .HasColumnType("nvarchar(127)")
                        .HasColumnName("Name");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(31)
                        .HasColumnType("nvarchar(31)")
                        .HasColumnName("State");

                    b.Property<string>("TypeReference")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("TypeReference");

                    b.HasKey("Id");

                    b.HasIndex("State");

                    b.ToTable("Terminal", (string)null);
                });

            modelBuilder.Entity("TypeLibrary.Data.Models.UnitLibDm", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(63)
                        .HasColumnType("nvarchar(63)")
                        .HasColumnName("Id");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc))
                        .HasColumnName("Created");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(127)
                        .HasColumnType("nvarchar(127)")
                        .HasColumnName("CreatedBy");

                    b.Property<string>("Description")
                        .HasMaxLength(511)
                        .HasColumnType("nvarchar(511)")
                        .HasColumnName("Description");

                    b.Property<string>("Iri")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Iri");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(127)
                        .HasColumnType("nvarchar(127)")
                        .HasColumnName("Name");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(31)
                        .HasColumnType("nvarchar(31)")
                        .HasColumnName("State");

                    b.Property<string>("Symbol")
                        .HasMaxLength(31)
                        .HasColumnType("nvarchar(31)")
                        .HasColumnName("Symbol");

                    b.Property<string>("TypeReference")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("TypeReference");

                    b.HasKey("Id");

                    b.HasIndex("State");

                    b.HasIndex("TypeReference");

                    b.ToTable("Unit", (string)null);
                });

            modelBuilder.Entity("TypeLibrary.Data.Models.AspectObjectAttributeLibDm", b =>
                {
                    b.HasOne("TypeLibrary.Data.Models.AspectObjectLibDm", "AspectObject")
                        .WithMany("AspectObjectAttributes")
                        .HasForeignKey("AspectObjectId");

                    b.HasOne("TypeLibrary.Data.Models.AttributeLibDm", "Attribute")
                        .WithMany()
                        .HasForeignKey("AttributeId");

                    b.Navigation("AspectObject");

                    b.Navigation("Attribute");
                });

            modelBuilder.Entity("TypeLibrary.Data.Models.AspectObjectLibDm", b =>
                {
                    b.HasOne("TypeLibrary.Data.Models.RdsLibDm", "Rds")
                        .WithMany()
                        .HasForeignKey("RdsId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Rds");
                });

            modelBuilder.Entity("TypeLibrary.Data.Models.AspectObjectTerminalLibDm", b =>
                {
                    b.HasOne("TypeLibrary.Data.Models.AspectObjectLibDm", "AspectObject")
                        .WithMany("AspectObjectTerminals")
                        .HasForeignKey("AspectObjectId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("TypeLibrary.Data.Models.TerminalLibDm", "Terminal")
                        .WithMany("TerminalAspectObjects")
                        .HasForeignKey("TerminalId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("AspectObject");

                    b.Navigation("Terminal");
                });

            modelBuilder.Entity("TypeLibrary.Data.Models.AttributeUnitLibDm", b =>
                {
                    b.HasOne("TypeLibrary.Data.Models.AttributeLibDm", "Attribute")
                        .WithMany("AttributeUnits")
                        .HasForeignKey("AttributeId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("TypeLibrary.Data.Models.UnitLibDm", "Unit")
                        .WithMany("UnitAttributes")
                        .HasForeignKey("UnitId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Attribute");

                    b.Navigation("Unit");
                });

            modelBuilder.Entity("TypeLibrary.Data.Models.RdsLibDm", b =>
                {
                    b.HasOne("TypeLibrary.Data.Models.CategoryLibDm", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("TypeLibrary.Data.Models.TerminalAttributeLibDm", b =>
                {
                    b.HasOne("TypeLibrary.Data.Models.AttributeLibDm", "Attribute")
                        .WithMany()
                        .HasForeignKey("AttributeId");

                    b.HasOne("TypeLibrary.Data.Models.TerminalLibDm", "Terminal")
                        .WithMany("TerminalAttributes")
                        .HasForeignKey("TerminalId");

                    b.Navigation("Attribute");

                    b.Navigation("Terminal");
                });

            modelBuilder.Entity("TypeLibrary.Data.Models.AspectObjectLibDm", b =>
                {
                    b.Navigation("AspectObjectAttributes");

                    b.Navigation("AspectObjectTerminals");
                });

            modelBuilder.Entity("TypeLibrary.Data.Models.AttributeLibDm", b =>
                {
                    b.Navigation("AttributeUnits");
                });

            modelBuilder.Entity("TypeLibrary.Data.Models.TerminalLibDm", b =>
                {
                    b.Navigation("TerminalAspectObjects");

                    b.Navigation("TerminalAttributes");
                });

            modelBuilder.Entity("TypeLibrary.Data.Models.UnitLibDm", b =>
                {
                    b.Navigation("UnitAttributes");
                });
#pragma warning restore 612, 618
        }
    }
}
