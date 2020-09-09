using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalWorkPlease.Models
{
    public class MovieAssign
    {
        public int MovieID { get; set; }
        public int RentalID { get; set; }
        public Movie Movie { get; set; }
        public Rental Rentals{ get; set; }

    }
}
