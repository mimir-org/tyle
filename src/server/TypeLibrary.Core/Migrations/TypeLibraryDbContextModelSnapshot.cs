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
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TypeLibrary.Data.Models.AttributeLibDm", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CompanyId")
                        .HasColumnType("int")
                        .HasColumnName("CompanyId");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2")
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
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AttributeId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDefault")
                        .HasColumnType("bit")
                        .HasColumnName("IsDefault");

                    b.Property<int>("UnitId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AttributeId");

                    b.HasIndex("UnitId");

                    b.ToTable("Attribute_Unit", (string)null);
                });

            modelBuilder.Entity("TypeLibrary.Data.Models.LogLibDm", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .HasMaxLength(511)
                        .HasColumnType("nvarchar(511)")
                        .HasColumnName("Comment");

                    b.Property<DateTime>("Created")
                        .HasMaxLength(63)
                        .HasColumnType("datetime2")
                        .HasColumnName("Created");

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

                    b.Property<int>("ObjectFirstVersionId")
                        .HasColumnType("int")
                        .HasColumnName("ObjectFirstVersionId");

                    b.Property<int>("ObjectId")
                        .HasColumnType("int")
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
                        .IsRequired()
                        .HasMaxLength(7)
                        .HasColumnType("nvarchar(7)")
                        .HasColumnName("ObjectVersion");

                    b.Property<string>("User")
                        .IsRequired()
                        .HasMaxLength(127)
                        .HasColumnType("nvarchar(127)")
                        .HasColumnName("User");

                    b.HasKey("Id");

                    b.HasIndex("LogType");

                    b.HasIndex("ObjectFirstVersionId");

                    b.HasIndex("ObjectId");

                    b.HasIndex("ObjectType");

                    b.HasIndex("ObjectId", "ObjectFirstVersionId", "ObjectType", "LogType");

                    b.ToTable("Log", (string)null);
                });

            modelBuilder.Entity("TypeLibrary.Data.Models.NodeAttributeLibDm", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AttributeId")
                        .HasColumnType("int");

                    b.Property<int>("NodeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AttributeId");

                    b.HasIndex("NodeId");

                    b.ToTable("Node_Attribute", (string)null);
                });

            modelBuilder.Entity("TypeLibrary.Data.Models.NodeLibDm", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

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
                        .HasMaxLength(63)
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

                    b.Property<int>("FirstVersionId")
                        .HasColumnType("int")
                        .HasColumnName("FirstVersionId");

                    b.Property<string>("Iri")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Iri");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(63)
                        .HasColumnType("nvarchar(63)")
                        .HasColumnName("Name");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int")
                        .HasColumnName("ParentId");

                    b.Property<string>("PurposeName")
                        .IsRequired()
                        .HasMaxLength(127)
                        .HasColumnType("nvarchar(127)")
                        .HasColumnName("PurposeName");

                    b.Property<string>("RdsCode")
                        .IsRequired()
                        .HasMaxLength(127)
                        .HasColumnType("nvarchar(127)")
                        .HasColumnName("RdsCode");

                    b.Property<string>("RdsName")
                        .IsRequired()
                        .HasMaxLength(127)
                        .HasColumnType("nvarchar(127)")
                        .HasColumnName("RdsName");

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

                    b.HasIndex("ParentId");

                    b.HasIndex("State");

                    b.HasIndex("State", "Aspect");

                    b.ToTable("Node", (string)null);
                });

            modelBuilder.Entity("TypeLibrary.Data.Models.NodeTerminalLibDm", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

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

                    b.Property<int>("NodeId")
                        .HasColumnType("int");

                    b.Property<int>("TerminalId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("NodeId");

                    b.HasIndex("TerminalId");

                    b.ToTable("Node_Terminal", (string)null);
                });

            modelBuilder.Entity("TypeLibrary.Data.Models.QuantityDatumLibDm", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CompanyId")
                        .HasColumnType("int")
                        .HasColumnName("CompanyId");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2")
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

            modelBuilder.Entity("TypeLibrary.Data.Models.SymbolLibDm", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

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

                    b.Property<int>("AttributeId")
                        .HasColumnType("int");

                    b.Property<int>("TerminalId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AttributeId");

                    b.HasIndex("TerminalId");

                    b.ToTable("Terminal_Attribute", (string)null);
                });

            modelBuilder.Entity("TypeLibrary.Data.Models.TerminalLibDm", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CompanyId")
                        .HasColumnType("int")
                        .HasColumnName("CompanyId");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2")
                        .HasColumnName("Created");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(127)
                        .HasColumnType("nvarchar(127)")
                        .HasColumnName("CreatedBy");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FirstVersionId")
                        .HasColumnType("int")
                        .HasColumnName("FirstVersionId");

                    b.Property<string>("Iri")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Iri");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(63)
                        .HasColumnType("nvarchar(63)")
                        .HasColumnName("Name");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int")
                        .HasColumnName("ParentId");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(31)
                        .HasColumnType("nvarchar(31)")
                        .HasColumnName("State");

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

                    b.HasIndex("ParentId");

                    b.HasIndex("State");

                    b.ToTable("Terminal", (string)null);
                });

            modelBuilder.Entity("TypeLibrary.Data.Models.UnitLibDm", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CompanyId")
                        .HasColumnType("int")
                        .HasColumnName("CompanyId");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2")
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

                    b.ToTable("Unit", (string)null);
                });

            modelBuilder.Entity("TypeLibrary.Data.Models.AttributeUnitLibDm", b =>
                {
                    b.HasOne("TypeLibrary.Data.Models.AttributeLibDm", "Attribute")
                        .WithMany("AttributeUnits")
                        .HasForeignKey("AttributeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("TypeLibrary.Data.Models.UnitLibDm", "Unit")
                        .WithMany("UnitAttributes")
                        .HasForeignKey("UnitId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Attribute");

                    b.Navigation("Unit");
                });

            modelBuilder.Entity("TypeLibrary.Data.Models.NodeAttributeLibDm", b =>
                {
                    b.HasOne("TypeLibrary.Data.Models.AttributeLibDm", "Attribute")
                        .WithMany("AttributeNodes")
                        .HasForeignKey("AttributeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("TypeLibrary.Data.Models.NodeLibDm", "Node")
                        .WithMany("NodeAttributes")
                        .HasForeignKey("NodeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Attribute");

                    b.Navigation("Node");
                });

            modelBuilder.Entity("TypeLibrary.Data.Models.NodeLibDm", b =>
                {
                    b.HasOne("TypeLibrary.Data.Models.NodeLibDm", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("TypeLibrary.Data.Models.NodeTerminalLibDm", b =>
                {
                    b.HasOne("TypeLibrary.Data.Models.NodeLibDm", "Node")
                        .WithMany("NodeTerminals")
                        .HasForeignKey("NodeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("TypeLibrary.Data.Models.TerminalLibDm", "Terminal")
                        .WithMany("TerminalNodes")
                        .HasForeignKey("TerminalId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Node");

                    b.Navigation("Terminal");
                });

            modelBuilder.Entity("TypeLibrary.Data.Models.TerminalAttributeLibDm", b =>
                {
                    b.HasOne("TypeLibrary.Data.Models.AttributeLibDm", "Attribute")
                        .WithMany("AttributeTerminals")
                        .HasForeignKey("AttributeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("TypeLibrary.Data.Models.TerminalLibDm", "Terminal")
                        .WithMany("TerminalAttributes")
                        .HasForeignKey("TerminalId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Attribute");

                    b.Navigation("Terminal");
                });

            modelBuilder.Entity("TypeLibrary.Data.Models.TerminalLibDm", b =>
                {
                    b.HasOne("TypeLibrary.Data.Models.TerminalLibDm", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("TypeLibrary.Data.Models.AttributeLibDm", b =>
                {
                    b.Navigation("AttributeNodes");

                    b.Navigation("AttributeTerminals");

                    b.Navigation("AttributeUnits");
                });

            modelBuilder.Entity("TypeLibrary.Data.Models.NodeLibDm", b =>
                {
                    b.Navigation("Children");

                    b.Navigation("NodeAttributes");

                    b.Navigation("NodeTerminals");
                });

            modelBuilder.Entity("TypeLibrary.Data.Models.TerminalLibDm", b =>
                {
                    b.Navigation("Children");

                    b.Navigation("TerminalAttributes");

                    b.Navigation("TerminalNodes");
                });

            modelBuilder.Entity("TypeLibrary.Data.Models.UnitLibDm", b =>
                {
                    b.Navigation("UnitAttributes");
                });
#pragma warning restore 612, 618
        }
    }
}
