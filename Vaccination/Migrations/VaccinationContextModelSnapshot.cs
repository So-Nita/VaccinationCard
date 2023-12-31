﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Vaccination.EnityConfiguration;

#nullable disable

namespace Vaccination.Migrations
{
    [DbContext(typeof(VaccinationContext))]
    partial class VaccinationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Vaccination.EnityModel.CardType", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("varchar");

                    b.Property<string>("CardName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar");

                    b.Property<DateTime>("Create")
                        .HasColumnType("date");

                    b.Property<bool?>("Deleted")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("CardName");

                    b.ToTable("CardTypes");
                });

            modelBuilder.Entity("Vaccination.EnityModel.Customer", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("varchar");

                    b.Property<DateTime>("DOB")
                        .HasColumnType("date");

                    b.Property<int>("IdentityId")
                        .HasMaxLength(10)
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar");

                    b.Property<string>("ProvinceId")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("varchar");

                    b.HasKey("Id");

                    b.HasIndex("ProvinceId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Vaccination.EnityModel.Province", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("varchar");

                    b.Property<bool?>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("ProvinceName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar");

                    b.HasKey("Id");

                    b.HasIndex("ProvinceName");

                    b.ToTable("Provinces");
                });

            modelBuilder.Entity("Vaccination.EnityModel.VaccinatedRecord", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("varchar");

                    b.Property<string>("Card_Id")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("varchar");

                    b.Property<string>("Customer_Id")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("varchar");

                    b.Property<DateTime>("DateVaccinated")
                        .HasColumnType("date");

                    b.Property<int>("DoeseReceived")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Customer_Id");

                    b.ToTable("VaccinatedRecords");
                });

            modelBuilder.Entity("Vaccination.EnityModel.Customer", b =>
                {
                    b.HasOne("Vaccination.EnityModel.Province", "Province")
                        .WithMany()
                        .HasForeignKey("ProvinceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Province");
                });

            modelBuilder.Entity("Vaccination.EnityModel.VaccinatedRecord", b =>
                {
                    b.HasOne("Vaccination.EnityModel.Customer", "Customer")
                        .WithMany("VaccinatedRecords")
                        .HasForeignKey("Customer_Id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Vaccination.EnityModel.Customer", b =>
                {
                    b.Navigation("VaccinatedRecords");
                });
#pragma warning restore 612, 618
        }
    }
}
