using System;
using System.ComponentModel.DataAnnotations;

namespace Web_CPI.Models
{
	public class Pengguna
	{
        [Key]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string Role { get; set; }
    }
}

