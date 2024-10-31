using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Rocky.Models.ViewModels
{
    public class ProductVM
    {
        
        public required Product Product { get; set; }

        public IEnumerable<SelectListItem>? CategorySelectList { get; set; }

        public IEnumerable<SelectListItem>? ApplicationTypeSelectList { get; set; }
    }
}
