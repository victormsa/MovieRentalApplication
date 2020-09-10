using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RentalWorkPlease.Models
{
    //métodos para criação da tabela Genre no banco de dados, definindo quais serão as colunas 
    //As classes Movie e Rental se assemelham a esta
    public class Genre
    {
        [Key]
        public int GenreID { get; set; }
        public string GenreName { get; set; }
        public DateTime CreationDate{ get; set; }

        public bool ActiveGenre { get; set; }
        //Collection para fazer integração entre a tabela de relacionamento n para n de Movie e Genre
        public ICollection<GenreAssign> GenreAssigns { get; set; }
    }
}
