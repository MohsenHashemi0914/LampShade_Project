using _01_LampshadeQuery.Contracts.Product;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class LatestArrivalsViewComponent : ViewComponent
    {
        #region Constructor

        private readonly IProductQuery _productQuery;

        public LatestArrivalsViewComponent(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        #endregion

        public IViewComponentResult Invoke()
        {
            var products = _productQuery.GetLatestArrivals();
            return View("Default", products);
        }
    }
}