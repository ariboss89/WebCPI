using System;
using Web_CPI.Models;

namespace Web_CPI.ViewModel
{
	public class HasilVM
	{
		public Hasil Hasil { get; set; }

        public List<Bobot> ListBobot { get; set; }
        public List<Tren> ListTren { get; set; }

        public List<Hasil> ListHasil { get; set; }
    }
}

