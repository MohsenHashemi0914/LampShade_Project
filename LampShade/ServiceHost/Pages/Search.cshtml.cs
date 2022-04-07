using _01_LampshadeQuery.Contracts.Product;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace ServiceHost.Pages
{
    public class SearchModel : PageModel
    {
        public string Value;
        public List<ProductQueryModel> Products { get; set; }

        #region Constructor

        private readonly IProductQuery _productQuery;

        public SearchModel(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        #endregion

        public void OnGet(string value)
        {
            Value = value;
            if (string.IsNullOrWhiteSpace(value))
                Value = "Â„Â „Õ’Ê·« ";
            Products = _productQuery.Search(value);
        }
    }
}
