using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTL_LTWNC.Models.EF
{
    [Table("tblUser")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int iUserID { get; set; }

        [Required]
        [StringLength(100)]
        public string? sUserName { get; set; }

        [Required]
        [StringLength(100)]
        [RegularExpression("^(?=[A-Z])[A-Za-z\\d@$!%*?&].{6,128}$", ErrorMessage = "Password must start with an uppercase")]
        public string? sUserPassword { get; set; }

        public int iUserRoleID { get; set; }

        [StringLength(100)]
        [NotMapped]
        public string? sUserRepeatPassword { get; set; }
    }
}
