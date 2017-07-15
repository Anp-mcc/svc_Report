using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using test2.Models;

namespace test2.Migrations
{
    [DbContext(typeof(DBReportContext))]
    [Migration("20170715072709_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("test2.Models.DBReport", b =>
                {
                    b.Property<string>("Hash");

                    b.Property<int>("Id");

                    b.Property<string>("Path");

                    b.HasKey("Hash");

                    b.ToTable("Reports");
                });
        }
    }
}
