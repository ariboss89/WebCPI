using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web_CPI.Models
{
	public class Kriteria
	{
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Nama Kriteria")]
        public string Nama { get; set; }
        [Required]
        [Display(Name = "Type Kriteria")]
        public string Type { get; set; }
        //[Required]
        //[Display(Name = "Bobot Kriteria")]
        //public double Bobot { get; set; }

    }
}

