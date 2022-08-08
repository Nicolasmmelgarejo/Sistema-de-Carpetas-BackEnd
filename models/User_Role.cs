using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.models
{
    public class User_Role
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string UserRole { get; set; }

        [Required]
        [ForeignKey("IdUser")]
        [Column(TypeName = "int")]
        public int UserIdUser { get; set; }
    }
}
