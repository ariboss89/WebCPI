using System;
using System.ComponentModel.DataAnnotations;

namespace Web_CPI.Models
{
	public class Alternatif
	{
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Nama Alternatif")]
        public string NamaAlternatif { get; set; }
        [Required]
        [Display(Name = "Alamat Alternatif")]
        public string Alamat { get; set; }
    }
}

