using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalWorkPlease.Models
{
    public class AssignedMovieData
    {
        public int MovieID { get; set; }
        public string MovieName { get; set; }
        public bool Assigned { get; set; }
    }
}
