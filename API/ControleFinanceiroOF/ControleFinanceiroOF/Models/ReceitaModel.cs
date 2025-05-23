using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleFinanceiroOF.Models
{
    [Table("Receitas", Schema = "Sistema")]
    public class ReceitaModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "uniqueidentifier")]
        public Guid TokenId { get; set; } = Guid.NewGuid();

        [Required]
        [Column(TypeName = "money")]
        public decimal Valor { get; set; }

        [Required]
        public DateTime Data { get; set; }

        public bool Extra { get; set; } = false;

        [MaxLength(256)]
        public string? Descricao { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        public UsuarioModel? Usuario { get; set; }
    }
}
