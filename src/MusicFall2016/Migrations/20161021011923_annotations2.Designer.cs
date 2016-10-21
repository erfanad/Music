using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using MusicFall2016.Models;

namespace MusicFall2016.Migrations
{
    [DbContext(typeof(MusicDbContext))]
    [Migration("20161021011923_annotations2")]
    partial class annotations2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MusicFall2016.Models.Album", b =>
                {
                    b.Property<int>("AlbumID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ArtistID");

                    b.Property<int>("GenreID");

                    b.Property<decimal>("Price");

                    b.Property<string>("Title");

                    b.HasKey("AlbumID");

                    b.HasIndex("ArtistID");

                    b.HasIndex("GenreID");

                    b.ToTable("Albums");
                });

            modelBuilder.Entity("MusicFall2016.Models.Artist", b =>
                {
                    b.Property<int>("ArtistID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Bio");

                    b.Property<string>("Name");

                    b.HasKey("ArtistID");

                    b.ToTable("Artists");
                });

            modelBuilder.Entity("MusicFall2016.Models.Genre", b =>
                {
                    b.Property<int>("GenreID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 20);

                    b.HasKey("GenreID");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("MusicFall2016.Models.Album", b =>
                {
                    b.HasOne("MusicFall2016.Models.Artist", "Artist")
                        .WithMany()
                        .HasForeignKey("ArtistID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MusicFall2016.Models.Genre", "Genre")
                        .WithMany()
                        .HasForeignKey("GenreID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
