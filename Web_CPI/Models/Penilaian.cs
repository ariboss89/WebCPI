using System;
using System.ComponentModel.DataAnnotations;

namespace Web_CPI.Models
{
	public class Penilaian
	{
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Nama Alternatif")]
        public string Alternatif { get; set; }
        [Required]
        [Display(Name = "Nama Kriteria")]
        public string Kriteria { get; set; }
        [Required]
        [Display(Name = "Type Kriteria")]
        public string Type { get; set; }
        [Required]
        [Display(Name = "Nama SubKriteria")]
        public string Subkriteria { get; set; }
        [Required]
        [Display(Name = "Nilai Kriteria")]
        [Range(1, 100)]
        public double Nilai { get; set; }
    }
}

