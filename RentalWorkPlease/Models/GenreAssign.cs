using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RentalWorkPlease.Models
{
    public class GenreAssign
    {
        public int MovieID { get; set; }
        public int GenreID { get; set; }
        public Movie Movie { get; set; }
        public Genre Genre { get; set; }
    }
}
