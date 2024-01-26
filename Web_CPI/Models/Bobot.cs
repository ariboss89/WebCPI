using System;
using System.ComponentModel.DataAnnotations;

namespace Web_CPI.Models
{
	public class Bobot
	{
        public int Id { get; set; }
        public string Nama { get; set; }
        public string Type { get; set; }
        public double Nilai { get; set; }
    }
}

