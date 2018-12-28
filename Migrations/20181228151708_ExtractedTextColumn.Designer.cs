﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PDFRepositoryProject.Data;

namespace PDFRepositoryProject.Migrations
{
    [DbContext(typeof(PDFContext))]
    [Migration("20181228151708_ExtractedTextColumn")]
    partial class ExtractedTextColumn
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PDFRepositoryProject.Models.DbPDFDocument", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Category");

                    b.Property<string>("Description");

                    b.Property<string>("FileName");

                    b.Property<DateTime>("UploadDateTime");

                    b.HasKey("Id");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("PDFRepositoryProject.Models.PDFFileData", b =>
                {
                    b.Property<int>("Id");

                    b.Property<byte[]>("Content");

                    b.Property<string>("ExtractedText");

                    b.HasKey("Id");

                    b.ToTable("PDFFileData");
                });

            modelBuilder.Entity("PDFRepositoryProject.Models.PDFFileData", b =>
                {
                    b.HasOne("PDFRepositoryProject.Models.DbPDFDocument", "DbPDFDocument")
                        .WithOne("Data")
                        .HasForeignKey("PDFRepositoryProject.Models.PDFFileData", "Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
