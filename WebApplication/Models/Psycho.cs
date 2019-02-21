using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public class Psycho
    {
        [Required] [Display(Name = "用户名")] public string Name { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }
    }
}