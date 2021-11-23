using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MVCShoppingCart.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        public string UserName { get; set; }

        [JsonIgnore]
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [JsonIgnore]
        [NotMapped]
        [Compare("Password", ErrorMessage = "Password not match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public string Type { get; set; }

        public Cart Cart { get; set; }
    }
}
