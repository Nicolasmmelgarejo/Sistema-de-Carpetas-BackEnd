using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.models
{
    public class Archivo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "varchar(100)")]
        public string NombreArchivo { get; set; }
        [Required]
        [Column(TypeName = "varchar(max)")]
        public string ContenidoArchivo { get; set; }

        [Required]
        [ForeignKey("Carpeta")]
        [Column(TypeName = "int")]
        public int CarpetaId { get; set; }
    }
}
