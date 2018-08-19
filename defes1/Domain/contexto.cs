using System.Data.Entity;

namespace defes1.Domain
{
    public class contexto : DbContext
    {
        public contexto() : base("contexto")
        {
            this.Configuration.LazyLoadingEnabled = false;
            
            /* ------ web config ------
             
             <add name="contexto"
             connectionString="Data Source=.\SQLEXPRESS;Initial Catalog=contexto;Integrated Security=SSPI;AttachDBFilename=|DataDirectory|\contexto.mdf"
             providerName="System.Data.EntityClient" />

             <add name="contexto"       
             connectionString="server=.;database=contexto;integrated security=false;user id=base;password=12345678;"
             providerName="System.Data.SqlClient" />

             <add name="contexto" 
             connectionString="Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\contexto.mdf;Initial Catalog=contexto;Integrated Security=true;User ID=base;Password=12345678"
             providerName="System.Data.SqlClient" />
       
             <add name="contexto" 
             connectionString="server=localhost;database=contexto;uid=base;password=12345678;"
             providerName="System.Data.SqlClient" />
       
             <add name="contexto" 
             connectionString="server=localhost;database=contexto;uid=base;password=12345678;" 
             providerName="System.Data.SqlClient" />

            */
            
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

            //modelBuilder.Entity<Filme>()
            //.HasRequired(s => s.Genero)
            //.WithMany()
            //.HasForeignKey(d => d.Genero.GeneroID)
            //.WillCascadeOnDelete(true);
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Locacao> Locacoes { get; set; }
        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Genero> Generos { get; set; }
    }
}
