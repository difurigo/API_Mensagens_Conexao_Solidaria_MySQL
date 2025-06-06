using api_gs_mensagens_conexao_solidaria.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace api_gs_mensagens_conexao_solidaria.Infrastructure.Data
{
    public class ApplicationContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public ApplicationContext(DbContextOptions<ApplicationContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<Mensagem> Mensagens { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Mensagem>(entity =>
            {
                entity.ToTable("MENSAGENS");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("ID").IsRequired();
                entity.Property(e => e.Titulo).HasColumnName("TITULO").IsRequired().HasMaxLength(200);
                entity.Property(e => e.Conteudo).HasColumnName("CONTEUDO").IsRequired().HasMaxLength(2000);
                entity.Property(e => e.Prioridade).HasColumnName("PRIORIDADE").IsRequired().HasMaxLength(50);
                entity.Property(e => e.Localizacao).HasColumnName("LOCALIZACAO").HasMaxLength(200);
                entity.Property(e => e.DataEnvio).HasColumnName("DATA_ENVIO").IsRequired();
                entity.Property(e => e.UUID).HasColumnName("UUID").HasMaxLength(100);
                entity.Property(e => e.TTL).HasColumnName("TTL");
                entity.Property(e => e.Status).HasColumnName("STATUS").HasMaxLength(50);
                entity.Property(e => e.UsuarioId).HasColumnName("USUARIO_ID").IsRequired().HasMaxLength(100);

                entity.HasOne(e => e.Usuario)
                    .WithMany(u => u.Mensagens)
                    .HasForeignKey(e => e.UsuarioId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("USUARIOS");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Nome)
                    .HasColumnName("NOME")
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Email)
                    .HasColumnName("EMAIL")
                    .IsRequired()
                    .HasMaxLength(200);
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

                /*
                optionsBuilder.UseOracle(connectionString, opt =>
                    opt.ExecutionStrategy(dependencies => new OracleExecutionStrategy(dependencies))
                );
                */
            }
        }
    }
}
