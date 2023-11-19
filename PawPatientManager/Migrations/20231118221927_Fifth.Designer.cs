﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PawPatientManager.DbContextsFiles;

#nullable disable

namespace PawPatientManager.Migrations
{
    [DbContext(typeof(MyDbContent))]
    [Migration("20231118221927_Fifth")]
    partial class Fifth
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.0");

            modelBuilder.Entity("PawPatientManager.DTOs.MedicalReceiptDTO", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Recommendation")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Signed")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("VisitID")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("VisitID");

                    b.ToTable("MedicalReceipts");
                });

            modelBuilder.Entity("PawPatientManager.DTOs.MedicationDTO", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("Amount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Medications");
                });

            modelBuilder.Entity("PawPatientManager.DTOs.OwnerDTO", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Adress")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Gender")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PESEL")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Owners");
                });

            modelBuilder.Entity("PawPatientManager.DTOs.PetDTO", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Gender")
                        .HasColumnType("INTEGER");

                    b.Property<string>("MicrochipNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("OwnerID")
                        .HasColumnType("TEXT");

                    b.Property<string>("Race")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Species")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("OwnerID");

                    b.ToTable("Pets");
                });

            modelBuilder.Entity("PawPatientManager.DTOs.VetDTO", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Vets");
                });

            modelBuilder.Entity("PawPatientManager.DTOs.VisitDTO", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("PetID")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("VetID")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("PetID");

                    b.HasIndex("VetID");

                    b.ToTable("Visits");
                });

            modelBuilder.Entity("PawPatientManager.DTOs.MedicalReceiptDTO", b =>
                {
                    b.HasOne("PawPatientManager.DTOs.VisitDTO", "Visit")
                        .WithMany("MedicalReceipts")
                        .HasForeignKey("VisitID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Visit");
                });

            modelBuilder.Entity("PawPatientManager.DTOs.PetDTO", b =>
                {
                    b.HasOne("PawPatientManager.DTOs.OwnerDTO", "Owner")
                        .WithMany("Pets")
                        .HasForeignKey("OwnerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("PawPatientManager.DTOs.VisitDTO", b =>
                {
                    b.HasOne("PawPatientManager.DTOs.PetDTO", "Pet")
                        .WithMany("Visits")
                        .HasForeignKey("PetID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PawPatientManager.DTOs.VetDTO", "Vet")
                        .WithMany("Visits")
                        .HasForeignKey("VetID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pet");

                    b.Navigation("Vet");
                });

            modelBuilder.Entity("PawPatientManager.DTOs.OwnerDTO", b =>
                {
                    b.Navigation("Pets");
                });

            modelBuilder.Entity("PawPatientManager.DTOs.PetDTO", b =>
                {
                    b.Navigation("Visits");
                });

            modelBuilder.Entity("PawPatientManager.DTOs.VetDTO", b =>
                {
                    b.Navigation("Visits");
                });

            modelBuilder.Entity("PawPatientManager.DTOs.VisitDTO", b =>
                {
                    b.Navigation("MedicalReceipts");
                });
#pragma warning restore 612, 618
        }
    }
}
