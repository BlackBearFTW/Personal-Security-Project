﻿// <auto-generated />
using System;
using DigiChoiceBackend.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DigiChoiceBackend.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230608195256_abbreviation_to_gender")]
    partial class abbreviation_to_gender
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.HasKey("Id");

                    b.HasIndex("PartyId");

                    b.ToTable("PartyMembers");
                });

            modelBuilder.Entity("DigiChoiceBackend.Models.Vote", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("PartyMemberId")
                        .HasColumnType("char(36)");

                    b.Property<string>("VoterIdentifier")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("PartyMemberId");

                    b.ToTable("Votes");
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

            modelBuilder.Entity("DigiChoiceBackend.Models.Vote", b =>
                {
                    b.HasOne("DigiChoiceBackend.Models.PartyMember", "PartyMember")
                        .WithMany()
                        .HasForeignKey("PartyMemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PartyMember");
                });

            modelBuilder.Entity("DigiChoiceBackend.Models.Party", b =>
                {
                    b.Navigation("PartyMembers");
                });
#pragma warning restore 612, 618
        }
    }
}