using System;
using System.ComponentModel.DataAnnotations;
namespace studentmanagement_webapi.Models.Users
{
    public class AuthenticateRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}

