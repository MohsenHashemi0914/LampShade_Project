using _01_LampshadeQuery.Contracts.Product;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        #region Constructor

        public FooterViewComponent()
        {
            
        }

        #endregion

        public IViewComponentResult Invoke()
        {
            return View("Default");
        }
    }
}