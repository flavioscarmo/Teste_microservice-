using System.ComponentModel.DataAnnotations;

namespace Gooapp.Autenticacao.Data.Dtos
{
    public class AtivaContaRequest
    {
        [Required]
        public int UsuarioId { get; set; }
        [Required]
        public string CodigoDeAtivacao { get; set; }
    }
}
