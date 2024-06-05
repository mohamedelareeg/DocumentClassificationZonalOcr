﻿// <auto-generated />
using System;
using DocumentClassificationZonalOcr.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DocumentClassificationZonalOcr.Api.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DocumentClassificationZonalOcr.Api.Models.Document", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Cancelled")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<int>("FormId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModifiedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Documents", (string)null);
                });

            modelBuilder.Entity("DocumentClassificationZonalOcr.Api.Models.ExportedMetaData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Cancelled")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<int>("FieldId")
                        .HasColumnType("int");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ModifiedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<int>("PaperId")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ExportedMetaData", (string)null);
                });

            modelBuilder.Entity("DocumentClassificationZonalOcr.Api.Models.Field", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Cancelled")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<int>("FormId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModifiedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FormId");

                    b.ToTable("Fields", (string)null);
                });

            modelBuilder.Entity("DocumentClassificationZonalOcr.Api.Models.Form", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Cancelled")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ModifiedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Forms", (string)null);
                });

            modelBuilder.Entity("DocumentClassificationZonalOcr.Api.Models.FormDetectionSetting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("ActivePerspectiveTransform")
                        .HasColumnType("bit");

                    b.Property<bool>("Blurring")
                        .HasColumnType("bit");

                    b.Property<bool>("Cancelled")
                        .HasColumnType("bit");

                    b.Property<bool>("ConvertToGrayscale")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<int>("DetectAlgorithm")
                        .HasColumnType("int");

                    b.Property<int>("DetectOptions")
                        .HasColumnType("int");

                    b.Property<bool>("EdgeDetection")
                        .HasColumnType("bit");

                    b.Property<decimal>("FormSimilarity")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("HistogramEqualization")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Normalization")
                        .HasColumnType("bit");

                    b.Property<int>("OcrEngine")
                        .HasColumnType("int");

                    b.Property<bool>("ResizeImage")
                        .HasColumnType("bit");

                    b.Property<double>("ZoneAllowedHeight")
                        .HasColumnType("float");

                    b.Property<double>("ZoneAllowedWidth")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("FormDetectionSettings", (string)null);
                });

            modelBuilder.Entity("DocumentClassificationZonalOcr.Api.Models.FormSample", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Cancelled")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<int>("FormId")
                        .HasColumnType("int");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ModifiedOnUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("FormId");

                    b.ToTable("FormSamples", (string)null);
                });

            modelBuilder.Entity("DocumentClassificationZonalOcr.Api.Models.Paper", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Cancelled")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DocumentId")
                        .HasColumnType("int");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("FormId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModifiedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DocumentId");

                    b.ToTable("Papers", (string)null);
                });

            modelBuilder.Entity("DocumentClassificationZonalOcr.Api.Models.Zone", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("ActualHeight")
                        .HasColumnType("float");

                    b.Property<double>("ActualImageHeight")
                        .HasColumnType("float");

                    b.Property<double>("ActualImageWidth")
                        .HasColumnType("float");

                    b.Property<double>("ActualWidth")
                        .HasColumnType("float");

                    b.Property<string>("AnchorPointPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Cancelled")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<int?>("FieldId")
                        .HasColumnType("int");

                    b.Property<int>("FormSampleId")
                        .HasColumnType("int");

                    b.Property<bool>("IsAnchorPoint")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDuplicated")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Regex")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WhiteList")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("X")
                        .HasColumnType("float");

                    b.Property<double>("Y")
                        .HasColumnType("float");

                    b.Property<int>("ZoneFieldType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FormSampleId");

                    b.ToTable("Zones", (string)null);
                });

            modelBuilder.Entity("DocumentClassificationZonalOcr.Api.Models.Field", b =>
                {
                    b.HasOne("DocumentClassificationZonalOcr.Api.Models.Form", null)
                        .WithMany("Fields")
                        .HasForeignKey("FormId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DocumentClassificationZonalOcr.Api.Models.FormSample", b =>
                {
                    b.HasOne("DocumentClassificationZonalOcr.Api.Models.Form", null)
                        .WithMany("Samples")
                        .HasForeignKey("FormId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DocumentClassificationZonalOcr.Api.Models.Paper", b =>
                {
                    b.HasOne("DocumentClassificationZonalOcr.Api.Models.Document", null)
                        .WithMany("Papers")
                        .HasForeignKey("DocumentId");
                });

            modelBuilder.Entity("DocumentClassificationZonalOcr.Api.Models.Zone", b =>
                {
                    b.HasOne("DocumentClassificationZonalOcr.Api.Models.FormSample", null)
                        .WithMany("Zones")
                        .HasForeignKey("FormSampleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DocumentClassificationZonalOcr.Api.Models.Document", b =>
                {
                    b.Navigation("Papers");
                });

            modelBuilder.Entity("DocumentClassificationZonalOcr.Api.Models.Form", b =>
                {
                    b.Navigation("Fields");

                    b.Navigation("Samples");
                });

            modelBuilder.Entity("DocumentClassificationZonalOcr.Api.Models.FormSample", b =>
                {
                    b.Navigation("Zones");
                });
#pragma warning restore 612, 618
        }
    }
}
