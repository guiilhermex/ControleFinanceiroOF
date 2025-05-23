using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleFinanceiroOF.Models
{
    [Table("Usuarios", Schema = "Seguranca")]
    public class UsuarioModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUsuario { get; set; }

        [Required]
        [Column(TypeName = "uniqueidentifier")]
        public Guid TokenID { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(256)]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [MaxLength(256)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(256)]
        public string Senha { get; set; } = string.Empty;

        // Relacionamentos (coleções)
        public ICollection<DespesaModel>? Despesas { get; set; }
        public ICollection<ReceitaModel>? Receitas { get; set; }
    }
}
