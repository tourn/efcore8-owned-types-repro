﻿// <auto-generated />
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Repro;

#nullable disable

namespace Repro.Migrations
{
    [DbContext(typeof(ReproDbContext))]
    [Migration("20240226121736_Test")]
    partial class Test
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 60);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Repro.Models.Staffing", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.ComplexProperty<Dictionary<string, object>>("Oven", "Repro.Models.Staffing.Oven#StaffEntry<DefaultTasks>", b1 =>
                        {
                            b1.IsRequired();

                            b1.ComplexProperty<Dictionary<string, object>>("Tasks", "Repro.Models.Staffing.Oven#StaffEntry<DefaultTasks>.Tasks#DefaultTasks", b2 =>
                                {
                                    b2.IsRequired();

                                    b2.Property<double>("Maintenance")
                                        .HasColumnType("double precision");

                                    b2.Property<double>("Operation")
                                        .HasColumnType("double precision");
                                });
                        });

                    b.HasKey("Id");

                    b.ToTable("Staffings");
                });
#pragma warning restore 612, 618
        }
    }
}
