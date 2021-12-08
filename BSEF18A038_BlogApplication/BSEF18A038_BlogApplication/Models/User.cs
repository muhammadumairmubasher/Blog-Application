using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace BSEF18A038_BlogApplication.Models
{
    public class User
    {
        
        public int Id { get; set; }

        [Required(ErrorMessage = "* Required Field")]
        [MinLength(3)]
        [MaxLength(20)]
        public string Username { get; set; }

        [Required(ErrorMessage = "* Required Field")]
        public string Email { get; set; }

        [Required(ErrorMessage = "* Required Field")]
        [MinLength(4)]
        public string Password { get; set; }

        public string pictureFileName { get; set; }
    }
}
