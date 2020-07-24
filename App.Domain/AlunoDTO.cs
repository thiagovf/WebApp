using System.ComponentModel.DataAnnotations;

namespace App.Domain
{
    public class AlunoDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo nome é de Preenchimento Obrigatório.")]
        [StringLength(50, ErrorMessage = "O nome deve ter no mínimo 2 e no máximo 50 caracteres.", MinimumLength = 2)]
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O campo RA é de Preenchimento Obrigatório.")]
        [Range(1,9099, ErrorMessage = "O intervalo para cadastro de RA deve estar entre 1 e 9099.")]
        public int? RA { get; set; }

        [RegularExpression(@"[0-9]{4}\-[0-9]{2}", ErrorMessage = "A data está fora do formato YYYY-MM")]
        public string Data { get; set; }
    }
}