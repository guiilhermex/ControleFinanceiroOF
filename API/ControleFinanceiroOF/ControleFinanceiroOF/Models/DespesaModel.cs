using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleFinanceiroOF.Models
{
    [Table("Despesas", Schema = "Sistema")]
    public class DespesaModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "uniqueidentifier")]
        public Guid TokenID { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(256)]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "money")]
        public decimal Valor { get; set; }

        [MaxLength(50)]
        public string? Categoria { get; set; }

        [Required]
        public DateTime Data { get; set; }

        public string? Descricao { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        public UsuarioModel? Usuario { get; set; }
    }
}
