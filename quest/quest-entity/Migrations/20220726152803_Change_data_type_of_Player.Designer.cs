﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using quest_entity;

#nullable disable

namespace quest_entity.Migrations
{
    [DbContext(typeof(QuestContext))]
    [Migration("20220726152803_Change_data_type_of_Player")]
    partial class Change_data_type_of_Player
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("quest_entity.Models.Player", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("EarnedPoints")
                        .HasColumnType("int");

                    b.Property<int?>("LastMilestoneIndexCompleted")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Player", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}