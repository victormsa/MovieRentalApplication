using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalWorkPlease.Models
{
    public class Genre
    {
        public int GenreID { get; set; }
        public string GenreName { get; set; }
        public DateTime CreationDate{ get; set; }

        public bool ActiveGenre { get; set; }
        public ICollection<GenreAssign> GenreAssigns { get; set; }
    }
}
