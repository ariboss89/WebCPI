using System;
using System.ComponentModel.DataAnnotations;

namespace Web_CPI.Models
{
	public class Subkriteria
	{
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Nama Kriteria")]
        public string Kriteria { get; set; }
        [Required]
        [Display(Name = "Nama Subkriteria")]
        public string Pilihan { get; set; }
        [Required]
        [Display(Name = "Nilai Subkriteria")]
        public int Nilai { get; set; }
    }
}

