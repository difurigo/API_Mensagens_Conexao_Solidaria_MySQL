using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_gs_mensagens_conexao_solidaria.Domain.Entities
{
    public class Mensagem
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string Titulo { get; set; } = string.Empty;
        [Required]
        public string Conteudo { get; set; } = string.Empty;
        [Required]
        public string Prioridade { get; set; } = string.Empty; // Baixa, Média, Alta
        public string? Localizacao { get; set; }
        public DateTime DataEnvio { get; set; } = DateTime.UtcNow;
        public string UUID { get; set; } = Guid.NewGuid().ToString();
        public int TTL { get; set; } = 5;
        public string Status { get; set; } = "Pendente";
        [Required]
        public string UsuarioId { get; set; } = string.Empty;
        [ForeignKey("UsuarioId")]
        public Usuario? Usuario { get; set; }
    }
}