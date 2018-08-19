using System;

namespace defes1.Models
{
    public class LocacaoModel
    {
        public int LocacaoID { get; set; }
        public int FilmeID { get; set; }
        public string FilmeNome { get; set; }
        public int ClienteID { get; set; }
        public string ClienteNome { get; set; }
        public DateTime LocacaoData { get; set; }
        public int UsuarioID { get; set; }
        public string UsuarioLogin { get; set; }
    }
}