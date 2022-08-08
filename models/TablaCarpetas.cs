using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.models
{
    public class TablaCarpetas
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "varchar(100)")]
        public string NombreCarpeta { get; set; }
        [Required]
        [ForeignKey("Carpeta")]
        [Column(TypeName = "int")]
        public int CarpetaId { get; set; }
    }
}
