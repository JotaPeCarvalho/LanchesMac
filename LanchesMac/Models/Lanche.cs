using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanchesMac.Models
{
    [Table("Lanches")]
    public class Lanche
    {
        [Key]
        public int LancheId { get; set; }
       
        [Required(ErrorMessage = "O nome do lanche deve ser informado")]
        [StringLength(80, MinimumLength = 10, ErrorMessage = "O {0} deve ter no mínimo {1} e no máximo {2}")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A descrição do lanche deve ser informada")]
        [MinLength(20, ErrorMessage = "A descrição deve ter no mínimo {1} caracteres")]
        [MaxLength(200, ErrorMessage = "A descrição deve ter no máximo {1} caracteres")]
        public string DescricaoCurta { get; set; }

        [Required(ErrorMessage = "A descrição detalhada do lanche deve ser informada")]
        [MinLength(20, ErrorMessage = "A descrição detalhada deve ter no mínimo {1} caracteres")]
        [MaxLength(200, ErrorMessage = "A descrição detalhada deve ter no máximo {1} caracteres")]
        public string DescricaoDetalhada { get; set; }

        [Required(ErrorMessage = "Informe o preço do lanche")]
        [Column(TypeName = "decimal(10,2)")]
        [Range(1, 999.99, ErrorMessage = "O preço deve estar entre R$ 1,00 e R$ 999,99")]
        public decimal Preco { get; set; }

        [StringLength(200, ErrorMessage = "A URL deve possuir 200 caracteres ou menos")]
        public string ImagemUrl { get; set; }

        [StringLength(200, ErrorMessage = "A URL deve possuir 200 caracteres ou menos")]
        public string ImagemThumbnailUrl { get; set; }

        public bool IsLanchePreferido { get; set; }

        public bool EmEstoque { get; set; }


        public int CategoriaId { get; set; }
        public virtual Categoria Categoria { get; set; }







    }
}
