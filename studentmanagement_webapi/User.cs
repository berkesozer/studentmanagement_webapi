using System;
using System.Data;
using System.Text.Json.Serialization;

namespace studentmanagement_webapi
{
    public class User
    {
        public string Username { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}

