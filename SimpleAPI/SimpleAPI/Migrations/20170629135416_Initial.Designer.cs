using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using SimpleAPI.Models;

namespace SimpleAPI.Migrations
{
    [DbContext(typeof(ReportContext))]
    [Migration("20170629135416_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SimpleAPI.Models.Report", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BarChartData");

                    b.Property<string>("ImagePath");

                    b.Property<string>("Name");

                    b.Property<string>("PieChartData");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.ToTable("Reports");
                });
        }
    }
}
