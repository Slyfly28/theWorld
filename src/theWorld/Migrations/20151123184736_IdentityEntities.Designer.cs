using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using theWorld.Models;

namespace theWorld.Migrations
{
    [DbContext(typeof(WorldContext))]
    [Migration("20151123184736_IdentityEntities")]
    partial class IdentityEntities
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Annotation("ProductVersion", "7.0.0-beta8-15964")
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("theWorld.Models.Stop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Arrival");

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<string>("Name");

                    b.Property<int>("Order");

                    b.Property<int?>("TripId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("theWorld.Models.Trip", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<string>("Name");

                    b.Property<string>("UserName");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("theWorld.Models.Stop", b =>
                {
                    b.HasOne("theWorld.Models.Trip")
                        .WithMany()
                        .ForeignKey("TripId");
                });
        }
    }
}
