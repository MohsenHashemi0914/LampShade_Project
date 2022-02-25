using _01_LampshadeQuery.Contracts.Slide;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class SlideViewComponent : ViewComponent
    {
        #region Constructor

        private readonly ISlideQuery _slideQuery;

        public SlideViewComponent(ISlideQuery slideQuery)
        {
            _slideQuery = slideQuery;
        }

        #endregion

        public IViewComponentResult Invoke()
        {
            var slides = _slideQuery.GetSlides();
            return View("Default", slides);
        }
    }
}