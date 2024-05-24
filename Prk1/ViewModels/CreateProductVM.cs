using Prk1.Models;
using System.ComponentModel.DataAnnotations;

namespace Prk1.ViewModels
{
    public class CreateProductVM
    {
        [Required]
        [StringLength(2000)] 
        public string Name { get; set; }
        public IFormFile Photo { get; set; }
        public int CategoryId { get; set; }
        public List<Category>? Categories {  get; set; }
    }
}
