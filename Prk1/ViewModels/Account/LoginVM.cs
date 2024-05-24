using System.ComponentModel.DataAnnotations;

namespace Prk1.ViewModels.Account
{
    public class LoginVM
    {
        [Required]
        public string UserName { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }


    }
}
