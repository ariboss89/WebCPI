using System;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web_CPI.Models;

namespace Web_CPI.ViewModel
{
	public class PenilaianVM
	{
        public Penilaian Penilaian { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> ListAlternatif { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> ListKriteria { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> ListSubKriteria { get; set; }

        public List<Subkriteria> ListSub { get; set; }
    }
}

