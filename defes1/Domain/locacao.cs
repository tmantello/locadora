using System;
using System.Collections.Generic;

namespace defes1.Domain
{
    public class Locacao
    {
        public int LocacaoID { get; set; }
        public Filme Filme { get; set; }
        public Cliente Cliente { get; set; }
        public DateTime LocacaoData { get; set; }
        public Usuario Usuario { get; set; }
    }
}