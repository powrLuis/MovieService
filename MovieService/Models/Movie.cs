using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieService.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        public string Titulo { get; set; }
        public int Año { get; set; }
        public decimal Precio { get; set; }


    }
}

namespace MovieService.Models
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int Year { get; set; }
        public string Status { get; set; }
    }
}