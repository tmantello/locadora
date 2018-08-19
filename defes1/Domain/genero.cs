using System;
using System.ComponentModel.DataAnnotations;

namespace defes1.Domain
{
    public class Genero
    {
        public int GeneroID { get; set; }
        public string GeneroNome { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime GeneroCriacao { get; set; }
        public bool GeneroAtivo { get; set; }
    }
}