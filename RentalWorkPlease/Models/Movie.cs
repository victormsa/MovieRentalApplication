using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RentalWorkPlease.Models
{
    public class Movie
    {
        [Key]
        public int MovieID{ get; set; }
        [Required]
        public string MovieName { get; set; }
        public DateTime CreationDate { get; set; }
        public bool Active { get; set; }

        public ICollection<GenreAssign> GenreAssigns 
        {
            get;


            set;
   
        }

    }
}
