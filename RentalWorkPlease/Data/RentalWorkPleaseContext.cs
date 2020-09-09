using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RentalWorkPlease.Models;

namespace RentalWorkPlease.Data
{
    public class RentalWorkPleaseContext : DbContext
    {
        public RentalWorkPleaseContext (DbContextOptions<RentalWorkPleaseContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }

        public DbSet<Rental> Rentals { get; set; }

        public DbSet<GenreAssign> GenreAssigns { get; set; }
        public DbSet<GenreAssign> MovieAssigns { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().ToTable("Movie");
            modelBuilder.Entity<Genre>().ToTable("Genre");
            modelBuilder.Entity<GenreAssign>().ToTable("GenreAssign");
            modelBuilder.Entity<MovieAssign>().ToTable("MovieAssign");

            modelBuilder.Entity<GenreAssign>()
                .HasKey(c => new { c.GenreID, c.MovieID });
            modelBuilder.Entity<MovieAssign>()
                .HasKey(c => new { c.MovieID, c.RentalID });
        }

        public DbSet<RentalWorkPlease.Models.Rental> Rental { get; set; }

    }
}
