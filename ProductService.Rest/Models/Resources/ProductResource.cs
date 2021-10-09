namespace ProductService.Rest.Models.Resources
{
    #region Public Class Definition
    public class ProductResource
    {
        public long ProductID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Specification { get; set; }

        public string Delivery { get; set; }

        public string ModelNumber { get; set; }

        public byte Status { get; set; }
    }
    #endregion
}