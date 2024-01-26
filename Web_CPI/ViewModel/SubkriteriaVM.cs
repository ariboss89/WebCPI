using System;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web_CPI.Models;

namespace Web_CPI.ViewModel
{
	public class SubkriteriaVM
	{
        public Subkriteria Subkriteria { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> ListKriteria { get; set; }
    }
}

