using System.ComponentModel.DataAnnotations;

namespace Gooapp.Autenticacao.Data.Requests
{
    public class SolicitaResetRequest
    {
        [Required]
        public string Email { get; set; }
    }
}
