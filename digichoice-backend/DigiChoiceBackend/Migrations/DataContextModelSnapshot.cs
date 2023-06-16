﻿// <auto-generated />
using System;
using DigiChoiceBackend.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DigiChoiceBackend.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("DigiChoiceBackend.Models.Party", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Abbreviation")
                        .HasColumnType("longtext");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("PositionNr")
                        .HasColumnType("int");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Parties");
                });

            modelBuilder.Entity("DigiChoiceBackend.Models.PartyMember", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Initials")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("PartyId")
                        .HasColumnType("char(36)");

                    b.Property<int>("PositionNr")
                        .HasColumnType("int");

                    b.Property<string>("ResidentCity")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("VoteCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PartyId");

                    b.ToTable("PartyMembers");
                });

            modelBuilder.Entity("DigiChoiceBackend.Models.Voter", b =>
                {
                    b.Property<string>("VoterId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("VoterId");

                    b.ToTable("Voters");
                });

            modelBuilder.Entity("DigiChoiceBackend.Models.PartyMember", b =>
                {
                    b.HasOne("DigiChoiceBackend.Models.Party", "Party")
                        .WithMany("PartyMembers")
                        .HasForeignKey("PartyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Party");
                });

            modelBuilder.Entity("DigiChoiceBackend.Models.Party", b =>
                {
                    b.Navigation("PartyMembers");
                });
#pragma warning restore 612, 618
        }
    }
}
