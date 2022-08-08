using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace backend.models
{
    public class Carpeta
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "varchar(100)")]
        public string NombreCarpeta { get; set; }
        public List<Archivo> Archivos { get; set; }

        public List<TablaCarpetas> TablaCarpetas { get; set; }
    }
}
