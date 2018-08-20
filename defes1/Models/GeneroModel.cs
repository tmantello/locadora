using System;

namespace defes1.Models
{
    public class GeneroModel
    {
        public bool GeneroSelecionado { get; set; }
        public int GeneroID { get; set; }
        public string GeneroNome { get; set; }       
        public DateTime GeneroCriacao { get; set; }
        public bool GeneroAtivo { get; set; }
    }
}
