﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanchesMac.Models
{
    [Table("Categorias")]
    public class Categoria
    {

        [Key]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Defina um nome para a categoria")]
        [StringLength(100, ErrorMessage = "O tamanho máximo da categoria é 100 caracteres")]
        public string CategoriaNome { get; set; }

        [Required(ErrorMessage = "Informe a descrição da categoria")]
        [StringLength(200, ErrorMessage = "O tamanho máximo da categoria é 200 caracteres")]
        public string Descricao { get; set; }

        public List<Lanche> Lanches { get; set; }

    }
}
