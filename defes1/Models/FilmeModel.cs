using System;

namespace defes1.Models
{
    public class FilmeModel
    {
        public int FilmeID { get; set; }
        public string FilmeNome { get; set; }
        public DateTime FilmeCriacao { get; set; }
        public bool FilmeAtivo { get; set; }
        public string Genero { get; set; }
    }
}