#region Include Definition
using System.ComponentModel.DataAnnotations;
#endregion

namespace ProductService.Rest.Models.Resources
{
    #region Public Class Definition
    public class ProductResource
    {
        public long ProductID { get; set; }

        [Required(ErrorMessage = "Product Name Required.")]
        [StringLength(32, MinimumLength = 2, ErrorMessage = "Must be between 2 to 32 characters.")]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Specification { get; set; }

        public string Delivery { get; set; }

        [Required(ErrorMessage = "Product Model Number Required.")]
        [StringLength(16, MinimumLength = 2, ErrorMessage = "Mut be between 2 to 16 characters.")]
        public string ModelNumber { get; set; }

        [Required(ErrorMessage = "Status Required.")]
        [Range(0, 4, ErrorMessage = "Invalid Option.")]
        public byte Status { get; set; }
    }
    #endregion
}