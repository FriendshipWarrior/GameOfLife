﻿// <auto-generated />
using GameOfLife.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GameOfLife.Migrations
{
    [DbContext(typeof(BoardContext))]
    partial class BoardContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.13");

            modelBuilder.Entity("GameOfLife.Domain.GameBoard", b =>
                {
                    b.Property<int>("BoardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("BoardJson")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Board");

                    b.HasKey("BoardId");

                    b.ToTable("GameBoards");
                });
#pragma warning restore 612, 618
        }
    }
}
