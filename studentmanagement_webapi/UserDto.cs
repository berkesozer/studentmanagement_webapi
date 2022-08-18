using System;
using Microsoft.AspNetCore.Http;
namespace studentmanagement_webapi
{
    public class UserDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}

