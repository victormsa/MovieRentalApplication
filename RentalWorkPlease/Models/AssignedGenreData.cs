using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalWorkPlease.Models
{
    public class AssignedGenreData
    {
        public int GenreID{ get; set; }
        public string GenreName{ get; set; }
        public bool Assigned { get; set; }
    }
}
