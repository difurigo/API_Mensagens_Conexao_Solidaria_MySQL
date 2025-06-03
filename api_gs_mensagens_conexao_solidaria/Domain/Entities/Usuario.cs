using System.ComponentModel.DataAnnotations;

namespace api_gs_mensagens_conexao_solidaria.Domain.Entities

{
    public class Usuario
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string Nome { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        public List<Mensagem> Mensagens { get; set; } = new();
    }
}
