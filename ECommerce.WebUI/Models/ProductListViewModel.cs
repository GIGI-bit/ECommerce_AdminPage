using ECommerce.Entities.Models;

namespace ECommerce.WebUI.Models
{
    public class ProductListViewModel
    {
        public int CurrentCategory { get; set; }
        public int CurrentPage { get; set; }
        public bool AZOrder { get; set; }
        public bool HighLowOrder { get; set; }
        public List<Product>? Products { get; set; }
        public int PageSize { get;  set; }
        public int PageCount { get;set; }
    }
}