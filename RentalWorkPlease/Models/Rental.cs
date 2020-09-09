using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RentalWorkPlease.Models
{
    public class Rental
    {
        [Key]
        public int RentalID { get; set; }
        public int Cpf { get; set; }
        public DateTime RentalDate{ get; set; }

        public ICollection<MovieAssign> MovieAssigns { get; set; }
    }
}
