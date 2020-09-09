using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalWorkPlease.Models
{
    public class RentalIndexData
    {
        public IEnumerable<Movie> Movies { get; set; }
        public IEnumerable<Rental> Rentals { get; set; }
    }
}
