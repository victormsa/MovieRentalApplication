using Microsoft.AspNetCore.Mvc.Rendering;
using RentalWorkPlease.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalWorkPlease.Models
{
    public class MovieIndexData
    {
        //Modelo de exibição para a página Movie/Index e cria objeto iteráveis das classes Movie e Genre
        //RentalIndexData funciona de forma análoga
        //Essa implementação é necessária, pois a página de exibição destas classes 
        //faz referência a objetos que não estão incluídos na mesma classe, como Genre que não está em Movie
        //e como Movie que não está em Rental
        public IEnumerable<Movie> Movies { get; set; }
        public IEnumerable<Genre> Genres { get; set; }


    }
}
