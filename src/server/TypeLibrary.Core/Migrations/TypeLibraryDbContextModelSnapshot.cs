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
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

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
                        .HasMaxLength(31)
                        .HasColumnType("nvarchar(31)")
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

                    b.Property<string>("TypeReferences")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("TypeReferences");

                    b.Property<string>("ValueStringList")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ValueStringList");

                    b.HasKey("Key");

                    b.ToTable("AttributePredefined", (string)null);
                });

            modelBuilder.Entity("TypeLibrary.Data.Models.InterfaceLibDm", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(127)
                        .HasColumnType("nvarchar(127)")
                        .HasColumnName("Id");

                    b.Property<string>("Aspect")
                        .IsRequired()
                        .HasMaxLength(31)
                        .HasColumnType("nvarchar(31)")
                        .HasColumnName("Aspect");

                    b.Property<string>("Attributes")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Attributes");

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(63)
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc))
                        .HasColumnName("Created");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(63)
                        .HasColumnType("nvarchar(63)")
                        .HasColumnName("CreatedBy");

                    b.Property<string>("Description")
                        .HasMaxLength(511)
                        .HasColumnType("nvarchar(511)")
                        .HasColumnName("Description");

                    b.Property<string>("FirstVersionId")
                        .IsRequired()
                        .HasMaxLength(127)
                        .HasColumnType("nvarchar(127)")
                        .HasColumnName("FirstVersionId");

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

                    b.Property<string>("ParentId")
                        .HasMaxLength(127)
                        .HasColumnType("nvarchar(127)")
                        .HasColumnName("ParentId");

                    b.Property<string>("PurposeName")
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

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(31)
                        .HasColumnType("nvarchar(31)")
                        .HasColumnName("State");

                    b.Property<string>("TerminalId")
                        .HasColumnType("nvarchar(127)")
                        .HasColumnName("Interface_TerminalId");

                    b.Property<string>("TypeReferences")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("TypeReferences");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasMaxLength(7)
                        .HasColumnType("nvarchar(7)")
                        .HasColumnName("Version");

                    b.HasKey("Id");

                    b.HasIndex("FirstVersionId");

                    b.HasIndex("ParentId");

                    b.HasIndex("State");

                    b.HasIndex("TerminalId");

                    b.HasIndex("State", "Aspect");

                    b.ToTable("Interface", (string)null);
                });

            modelBuilder.Entity("TypeLibrary.Data.Models.LogLibDm", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

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

                    b.Property<string>("ObjectFirstVersionId")
                        .IsRequired()
                        .HasMaxLength(127)
                        .HasColumnType("nvarchar(127)")
                        .HasColumnName("ObjectFirstVersionId");

                    b.Property<string>("ObjectId")
                        .IsRequired()
                        .HasMaxLength(127)
                        .HasColumnType("nvarchar(127)")
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
                        .HasMaxLength(63)
                        .HasColumnType("nvarchar(63)")
                        .HasColumnName("User");

                    b.HasKey("Id");

                    b.HasIndex("LogType");

                    b.HasIndex("ObjectFirstVersionId");

                    b.HasIndex("ObjectId");

                    b.HasIndex("ObjectType");

                    b.HasIndex("ObjectId", "ObjectFirstVersionId", "ObjectType", "LogType");

                    b.ToTable("Log", (string)null);
                });

            modelBuilder.Entity("TypeLibrary.Data.Models.NodeLibDm", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(127)
                        .HasColumnType("nvarchar(127)")
                        .HasColumnName("Id");

                    b.Property<string>("Aspect")
                        .IsRequired()
                        .HasMaxLength(31)
                        .HasColumnType("nvarchar(31)")
                        .HasColumnName("Aspect");

                    b.Property<string>("Attributes")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Attributes");

                    b.Property<int>("CompanyId")
                        .HasMaxLength(127)
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
                        .HasMaxLength(63)
                        .HasColumnType("nvarchar(63)")
                        .HasColumnName("CreatedBy");

                    b.Property<string>("Description")
                        .HasMaxLength(511)
                        .HasColumnType("nvarchar(511)")
                        .HasColumnName("Description");

                    b.Property<string>("FirstVersionId")
                        .IsRequired()
                        .HasMaxLength(127)
                        .HasColumnType("nvarchar(127)")
                        .HasColumnName("FirstVersionId");

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

                    b.Property<string>("ParentId")
                        .HasMaxLength(127)
                        .HasColumnType("nvarchar(127)")
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

                    b.Property<string>("TypeReferences")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("TypeReferences");

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
                    b.Property<string>("Id")
                        .HasMaxLength(127)
                        .HasColumnType("nvarchar(127)")
                        .HasColumnName("Id");

                    b.Property<string>("ConnectorDirection")
                        .IsRequired()
                        .HasMaxLength(31)
                        .HasColumnType("nvarchar(31)")
                        .HasColumnName("ConnectorDirection");

                    b.Property<string>("NodeId")
                        .HasColumnType("nvarchar(127)");

                    b.Property<int>("Quantity")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1)
                        .HasColumnName("Quantity");

                    b.Property<string>("TerminalId")
                        .HasColumnType("nvarchar(127)");

                    b.HasKey("Id");

                    b.HasIndex("NodeId");

                    b.HasIndex("TerminalId");

                    b.ToTable("Node_Terminal", (string)null);
                });

            modelBuilder.Entity("TypeLibrary.Data.Models.SymbolLibDm", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(127)
                        .HasColumnType("nvarchar(127)")
                        .HasColumnName("Id");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2")
                        .HasColumnName("Created");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(31)
                        .HasColumnType("nvarchar(31)")
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

                    b.Property<string>("TypeReferences")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("TypeReferences");

                    b.HasKey("Id");

                    b.ToTable("Symbol", (string)null);
                });

            modelBuilder.Entity("TypeLibrary.Data.Models.TerminalLibDm", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(127)
                        .HasColumnType("nvarchar(127)")
                        .HasColumnName("Id");

                    b.Property<string>("Attributes")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Attributes");

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CompanyId")
                        .HasMaxLength(127)
                        .HasColumnType("int")
                        .HasColumnName("CompanyId");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2")
                        .HasColumnName("Created");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(31)
                        .HasColumnType("nvarchar(31)")
                        .HasColumnName("CreatedBy");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstVersionId")
                        .IsRequired()
                        .HasMaxLength(127)
                        .HasColumnType("nvarchar(127)")
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

                    b.Property<string>("ParentId")
                        .HasMaxLength(127)
                        .HasColumnType("nvarchar(127)")
                        .HasColumnName("ParentId");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(31)
                        .HasColumnType("nvarchar(31)")
                        .HasColumnName("State");

                    b.Property<string>("TypeReferences")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("TypeReferences");

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

            modelBuilder.Entity("TypeLibrary.Data.Models.TransportLibDm", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(127)
                        .HasColumnType("nvarchar(127)")
                        .HasColumnName("Id");

                    b.Property<string>("Aspect")
                        .IsRequired()
                        .HasMaxLength(31)
                        .HasColumnType("nvarchar(31)")
                        .HasColumnName("Aspect");

                    b.Property<string>("Attributes")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Attributes");

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(63)
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc))
                        .HasColumnName("Created");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(63)
                        .HasColumnType("nvarchar(63)")
                        .HasColumnName("CreatedBy");

                    b.Property<string>("Description")
                        .HasMaxLength(511)
                        .HasColumnType("nvarchar(511)")
                        .HasColumnName("Description");

                    b.Property<string>("FirstVersionId")
                        .IsRequired()
                        .HasMaxLength(127)
                        .HasColumnType("nvarchar(127)")
                        .HasColumnName("FirstVersionId");

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

                    b.Property<string>("ParentId")
                        .HasMaxLength(127)
                        .HasColumnType("nvarchar(127)")
                        .HasColumnName("ParentId");

                    b.Property<string>("PurposeName")
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

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(31)
                        .HasColumnType("nvarchar(31)")
                        .HasColumnName("State");

                    b.Property<string>("TerminalId")
                        .HasColumnType("nvarchar(127)")
                        .HasColumnName("Transport_TerminalId");

                    b.Property<string>("TypeReferences")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("TypeReferences");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasMaxLength(7)
                        .HasColumnType("nvarchar(7)")
                        .HasColumnName("Version");

                    b.HasKey("Id");

                    b.HasIndex("FirstVersionId");

                    b.HasIndex("ParentId");

                    b.HasIndex("State");

                    b.HasIndex("TerminalId");

                    b.HasIndex("State", "Aspect");

                    b.ToTable("Transport", (string)null);
                });

            modelBuilder.Entity("TypeLibrary.Data.Models.InterfaceLibDm", b =>
                {
                    b.HasOne("TypeLibrary.Data.Models.InterfaceLibDm", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("TypeLibrary.Data.Models.TerminalLibDm", "Terminal")
                        .WithMany("Interfaces")
                        .HasForeignKey("TerminalId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Parent");

                    b.Navigation("Terminal");
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
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("TypeLibrary.Data.Models.TerminalLibDm", "Terminal")
                        .WithMany("TerminalNodes")
                        .HasForeignKey("TerminalId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Node");

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

            modelBuilder.Entity("TypeLibrary.Data.Models.TransportLibDm", b =>
                {
                    b.HasOne("TypeLibrary.Data.Models.TransportLibDm", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("TypeLibrary.Data.Models.TerminalLibDm", "Terminal")
                        .WithMany("Transports")
                        .HasForeignKey("TerminalId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Parent");

                    b.Navigation("Terminal");
                });

            modelBuilder.Entity("TypeLibrary.Data.Models.InterfaceLibDm", b =>
                {
                    b.Navigation("Children");
                });

            modelBuilder.Entity("TypeLibrary.Data.Models.NodeLibDm", b =>
                {
                    b.Navigation("Children");

                    b.Navigation("NodeTerminals");
                });

            modelBuilder.Entity("TypeLibrary.Data.Models.TerminalLibDm", b =>
                {
                    b.Navigation("Children");

                    b.Navigation("Interfaces");

                    b.Navigation("TerminalNodes");

                    b.Navigation("Transports");
                });

            modelBuilder.Entity("TypeLibrary.Data.Models.TransportLibDm", b =>
                {
                    b.Navigation("Children");
                });
#pragma warning restore 612, 618
        }
    }
}
