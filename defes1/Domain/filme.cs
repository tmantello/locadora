using System;

namespace defes1.Domain
{
    public class Filme
    {
        public int FilmeID { get; set; }
        public string FilmeNome { get; set; }
        public DateTime FilmeCriacao { get; set; }
        public bool FilmeAtivo { get; set; }

        public Genero Genero { get; set; }
    }
}