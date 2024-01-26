using System;
using System.ComponentModel.DataAnnotations;

namespace Web_CPI.Models
{
	public class Tren
	{
        public string Alternatif { get; set; }
        public string Kriteria { get; set; }
        public string Type { get; set; }
        public string Subkriteria { get; set; }
        public double NilaiTrend { get; set; }
        public double NilaiIndeksAlt { get; set; }
    }
}

