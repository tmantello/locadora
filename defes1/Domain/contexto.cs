using System.Data.Entity;

namespace defes1.Domain
{
    public class contexto : DbContext
    {
        public contexto() : base("contexto")
        {
            this.Configuration.LazyLoadingEnabled = false;            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Filme>()
            .HasOptional(x => x.Genero)
            .WithOptionalDependent()
            .WillCascadeOnDelete(true);

            modelBuilder.Entity<Locacao>()
           .HasOptional(x => x.Filme)
           .WithOptionalDependent()
           .WillCascadeOnDelete(true);          
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Locacao> Locacoes { get; set; }
        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Genero> Generos { get; set; }
    }
}
