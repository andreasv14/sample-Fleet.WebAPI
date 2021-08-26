﻿// <auto-generated />
using System;
using Fleet.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Fleet.Data.Migrations
{
    [DbContext(typeof(FleetDbContext))]
    partial class FleetDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.7");

            modelBuilder.Entity("Fleet.Models.Container", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<int?>("TransportationId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("TransportationId");

                    b.ToTable("Containers");
                });

            modelBuilder.Entity("Fleet.Models.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("FromTransportationId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("IssueDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("ToTransportationId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("FromTransportationId");

                    b.HasIndex("ToTransportationId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("Fleet.Models.Transportation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<uint>("Capacity")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Transportations");
                });

            modelBuilder.Entity("Fleet.Models.Container", b =>
                {
                    b.HasOne("Fleet.Models.Transportation", "Transportation")
                        .WithMany("LoadContainers")
                        .HasForeignKey("TransportationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Transportation");
                });

            modelBuilder.Entity("Fleet.Models.Transaction", b =>
                {
                    b.HasOne("Fleet.Models.Transportation", "FromTransportation")
                        .WithMany()
                        .HasForeignKey("FromTransportationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Fleet.Models.Transportation", "ToTransportation")
                        .WithMany()
                        .HasForeignKey("ToTransportationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FromTransportation");

                    b.Navigation("ToTransportation");
                });

            modelBuilder.Entity("Fleet.Models.Transportation", b =>
                {
                    b.Navigation("LoadContainers");
                });
#pragma warning restore 612, 618
        }
    }
}