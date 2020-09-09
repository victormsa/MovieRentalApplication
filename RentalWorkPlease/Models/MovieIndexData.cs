using RentalWorkPlease.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalWorkPlease.Models
{
    public class MovieIndexData
    {
        public IEnumerable<Movie> Movies { get; set; }
        public IEnumerable<Genre> Genres { get; set; }

    }
}
