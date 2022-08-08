using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.models
{
    public class User
    {
        [Key]
        public int IdUser { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string UserName { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string UserPassword { get; set; }
        [Required]
        public List<User_Role> User_Roles { get; set; }

    }
}
