﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TypeLibrary.Data;

#nullable disable

namespace TypeLibrary.Data.Migrations
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

            modelBuilder.Entity("TypeLibrary.Core.Attributes.AttributeGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ContributedBy")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

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

                    b.HasKey("Id");

                    b.ToTable("AttributeGroup", (string)null);
                });

            modelBuilder.Entity("TypeLibrary.Core.Attributes.AttributeGroupAttributeJoin", b =>
                {
                    b.Property<Guid>("AttributeGroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AttributeId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AttributeGroupId", "AttributeId");

                    b.HasIndex("AttributeId");

                    b.ToTable("AttributeGroup_Attribute", (string)null);
                });

            modelBuilder.Entity("TypeLibrary.Core.Attributes.AttributeType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ContributedBy")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

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
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("RangeQualifier")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("RegularityQualifier")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ScopeQualifier")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("UnitMaxCount")
                        .HasColumnType("int");

                    b.Property<int>("UnitMinCount")
                        .HasColumnType("int");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Id");

                    b.HasIndex("PredicateId");

                    b.ToTable("Attribute", (string)null);
                });

            modelBuilder.Entity("TypeLibrary.Core.Attributes.AttributeUnitJoin", b =>
                {
                    b.Property<Guid>("AttributeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("UnitId")
                        .HasColumnType("int");

                    b.HasKey("AttributeId", "UnitId");

                    b.HasIndex("UnitId");

                    b.ToTable("Attribute_Unit", (string)null);
                });

            modelBuilder.Entity("TypeLibrary.Core.Attributes.RdlPredicate", b =>
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

                    b.ToTable("Predicate", (string)null);
                });

            modelBuilder.Entity("TypeLibrary.Core.Attributes.RdlUnit", b =>
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
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Unit", (string)null);
                });

            modelBuilder.Entity("TypeLibrary.Core.Attributes.ValueConstraint", b =>
                {
                    b.Property<Guid>("AttributeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConstraintType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("DataType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

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

                    b.ToTable("ValueConstraint", (string)null);
                });

            modelBuilder.Entity("TypeLibrary.Core.Attributes.ValueListEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("EntryValue")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<Guid>("ValueConstraintId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ValueConstraintId");

                    b.ToTable("ValueListEntry", (string)null);
                });

            modelBuilder.Entity("TypeLibrary.Core.Blocks.BlockAttributeTypeReference", b =>
                {
                    b.Property<Guid>("BlockId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AttributeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AttributeGroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("MaxCount")
                        .HasColumnType("int");

                    b.Property<int>("MinCount")
                        .HasColumnType("int");

                    b.HasKey("BlockId", "AttributeId");

                    b.HasIndex("AttributeGroupId");

                    b.HasIndex("AttributeId");

                    b.ToTable("Block_Attribute", (string)null);
                });

            modelBuilder.Entity("TypeLibrary.Core.Blocks.BlockClassifierJoin", b =>
                {
                    b.Property<Guid>("BlockId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ClassifierId")
                        .HasColumnType("int");

                    b.HasKey("BlockId", "ClassifierId");

                    b.HasIndex("ClassifierId");

                    b.ToTable("Block_Classifier", (string)null);
                });

            modelBuilder.Entity("TypeLibrary.Core.Blocks.BlockTerminalTypeReference", b =>
                {
                    b.Property<Guid>("BlockId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TerminalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Direction")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("MaxCount")
                        .HasColumnType("int");

                    b.Property<int>("MinCount")
                        .HasColumnType("int");

                    b.HasKey("BlockId", "TerminalId", "Direction");

                    b.HasIndex("TerminalId");

                    b.ToTable("Block_Terminal", (string)null);
                });

            modelBuilder.Entity("TypeLibrary.Core.Blocks.BlockType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Aspect")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ContributedBy")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

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

                    b.Property<string>("Notation")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("PurposeId")
                        .HasColumnType("int");

                    b.Property<string>("Symbol")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Id");

                    b.HasIndex("PurposeId");

                    b.ToTable("Block", (string)null);
                });

            modelBuilder.Entity("TypeLibrary.Core.Common.RdlClassifier", b =>
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

                    b.ToTable("Classifier", (string)null);
                });

            modelBuilder.Entity("TypeLibrary.Core.Common.RdlPurpose", b =>
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

                    b.ToTable("Purpose", (string)null);
                });

            modelBuilder.Entity("TypeLibrary.Core.Terminals.RdlMedium", b =>
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

                    b.ToTable("Medium", (string)null);
                });

            modelBuilder.Entity("TypeLibrary.Core.Terminals.TerminalAttributeTypeReference", b =>
                {
                    b.Property<Guid>("TerminalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AttributeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AttributeGroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("MaxCount")
                        .HasColumnType("int");

                    b.Property<int>("MinCount")
                        .HasColumnType("int");

                    b.HasKey("TerminalId", "AttributeId");

                    b.HasIndex("AttributeGroupId");

                    b.HasIndex("AttributeId");

                    b.ToTable("Terminal_Attribute", (string)null);
                });

            modelBuilder.Entity("TypeLibrary.Core.Terminals.TerminalClassifierJoin", b =>
                {
                    b.Property<Guid>("TerminalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ClassifierId")
                        .HasColumnType("int");

                    b.HasKey("TerminalId", "ClassifierId");

                    b.HasIndex("ClassifierId");

                    b.ToTable("Terminal_Classifier", (string)null);
                });

            modelBuilder.Entity("TypeLibrary.Core.Terminals.TerminalType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Aspect")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ContributedBy")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTimeOffset>("LastUpdateOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<int?>("MediumId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Notation")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("PurposeId")
                        .HasColumnType("int");

                    b.Property<string>("Qualifier")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Symbol")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Id");

                    b.HasIndex("MediumId");

                    b.HasIndex("PurposeId");

                    b.ToTable("Terminal", (string)null);
                });

            modelBuilder.Entity("TypeLibrary.Core.Attributes.AttributeGroupAttributeJoin", b =>
                {
                    b.HasOne("TypeLibrary.Core.Attributes.AttributeGroup", "AttributeGroup")
                        .WithMany("Attributes")
                        .HasForeignKey("AttributeGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TypeLibrary.Core.Attributes.AttributeType", "Attribute")
                        .WithMany()
                        .HasForeignKey("AttributeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Attribute");

                    b.Navigation("AttributeGroup");
                });

            modelBuilder.Entity("TypeLibrary.Core.Attributes.AttributeType", b =>
                {
                    b.HasOne("TypeLibrary.Core.Attributes.RdlPredicate", "Predicate")
                        .WithMany()
                        .HasForeignKey("PredicateId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Predicate");
                });

            modelBuilder.Entity("TypeLibrary.Core.Attributes.AttributeUnitJoin", b =>
                {
                    b.HasOne("TypeLibrary.Core.Attributes.AttributeType", "Attribute")
                        .WithMany("Units")
                        .HasForeignKey("AttributeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TypeLibrary.Core.Attributes.RdlUnit", "Unit")
                        .WithMany()
                        .HasForeignKey("UnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Attribute");

                    b.Navigation("Unit");
                });

            modelBuilder.Entity("TypeLibrary.Core.Attributes.ValueConstraint", b =>
                {
                    b.HasOne("TypeLibrary.Core.Attributes.AttributeType", "Attribute")
                        .WithOne("ValueConstraint")
                        .HasForeignKey("TypeLibrary.Core.Attributes.ValueConstraint", "AttributeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Attribute");
                });

            modelBuilder.Entity("TypeLibrary.Core.Attributes.ValueListEntry", b =>
                {
                    b.HasOne("TypeLibrary.Core.Attributes.ValueConstraint", "ValueConstraint")
                        .WithMany("ValueList")
                        .HasForeignKey("ValueConstraintId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ValueConstraint");
                });

            modelBuilder.Entity("TypeLibrary.Core.Blocks.BlockAttributeTypeReference", b =>
                {
                    b.HasOne("TypeLibrary.Core.Attributes.AttributeGroup", "AsPartOfAttributeGroup")
                        .WithMany()
                        .HasForeignKey("AttributeGroupId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("TypeLibrary.Core.Attributes.AttributeType", "Attribute")
                        .WithMany()
                        .HasForeignKey("AttributeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TypeLibrary.Core.Blocks.BlockType", "Block")
                        .WithMany("Attributes")
                        .HasForeignKey("BlockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AsPartOfAttributeGroup");

                    b.Navigation("Attribute");

                    b.Navigation("Block");
                });

            modelBuilder.Entity("TypeLibrary.Core.Blocks.BlockClassifierJoin", b =>
                {
                    b.HasOne("TypeLibrary.Core.Blocks.BlockType", "Block")
                        .WithMany("Classifiers")
                        .HasForeignKey("BlockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TypeLibrary.Core.Common.RdlClassifier", "Classifier")
                        .WithMany()
                        .HasForeignKey("ClassifierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Block");

                    b.Navigation("Classifier");
                });

            modelBuilder.Entity("TypeLibrary.Core.Blocks.BlockTerminalTypeReference", b =>
                {
                    b.HasOne("TypeLibrary.Core.Blocks.BlockType", "Block")
                        .WithMany("Terminals")
                        .HasForeignKey("BlockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TypeLibrary.Core.Terminals.TerminalType", "Terminal")
                        .WithMany()
                        .HasForeignKey("TerminalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Block");

                    b.Navigation("Terminal");
                });

            modelBuilder.Entity("TypeLibrary.Core.Blocks.BlockType", b =>
                {
                    b.HasOne("TypeLibrary.Core.Common.RdlPurpose", "Purpose")
                        .WithMany()
                        .HasForeignKey("PurposeId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Purpose");
                });

            modelBuilder.Entity("TypeLibrary.Core.Terminals.TerminalAttributeTypeReference", b =>
                {
                    b.HasOne("TypeLibrary.Core.Attributes.AttributeGroup", "AsPartOfAttributeGroup")
                        .WithMany()
                        .HasForeignKey("AttributeGroupId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("TypeLibrary.Core.Attributes.AttributeType", "Attribute")
                        .WithMany()
                        .HasForeignKey("AttributeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TypeLibrary.Core.Terminals.TerminalType", "Terminal")
                        .WithMany("Attributes")
                        .HasForeignKey("TerminalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AsPartOfAttributeGroup");

                    b.Navigation("Attribute");

                    b.Navigation("Terminal");
                });

            modelBuilder.Entity("TypeLibrary.Core.Terminals.TerminalClassifierJoin", b =>
                {
                    b.HasOne("TypeLibrary.Core.Common.RdlClassifier", "Classifier")
                        .WithMany()
                        .HasForeignKey("ClassifierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TypeLibrary.Core.Terminals.TerminalType", "Terminal")
                        .WithMany("Classifiers")
                        .HasForeignKey("TerminalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Classifier");

                    b.Navigation("Terminal");
                });

            modelBuilder.Entity("TypeLibrary.Core.Terminals.TerminalType", b =>
                {
                    b.HasOne("TypeLibrary.Core.Terminals.RdlMedium", "Medium")
                        .WithMany()
                        .HasForeignKey("MediumId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("TypeLibrary.Core.Common.RdlPurpose", "Purpose")
                        .WithMany()
                        .HasForeignKey("PurposeId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Medium");

                    b.Navigation("Purpose");
                });

            modelBuilder.Entity("TypeLibrary.Core.Attributes.AttributeGroup", b =>
                {
                    b.Navigation("Attributes");
                });

            modelBuilder.Entity("TypeLibrary.Core.Attributes.AttributeType", b =>
                {
                    b.Navigation("Units");

                    b.Navigation("ValueConstraint");
                });

            modelBuilder.Entity("TypeLibrary.Core.Attributes.ValueConstraint", b =>
                {
                    b.Navigation("ValueList");
                });

            modelBuilder.Entity("TypeLibrary.Core.Blocks.BlockType", b =>
                {
                    b.Navigation("Attributes");

                    b.Navigation("Classifiers");

                    b.Navigation("Terminals");
                });

            modelBuilder.Entity("TypeLibrary.Core.Terminals.TerminalType", b =>
                {
                    b.Navigation("Attributes");

                    b.Navigation("Classifiers");
                });
#pragma warning restore 612, 618
        }
    }
}
