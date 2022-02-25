using System.Collections.Generic;
using System.Linq;
using _01_LampshadeQuery.Contracts.Slide;
using ShopManagement.Infrastructure.EFCore;

namespace _01_LampshadeQuery.Query
{
    public class SlideQuery : ISlideQuery
    {
        #region Constructor

        private readonly ShopContext _shopContext;

        public SlideQuery(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        #endregion

        public List<SlideQueryModel> GetSlides()
        {
            return _shopContext.Slides
                .Where(x => !x.IsRemoved)
                .Select(x => new SlideQueryModel
                {
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Heading = x.Heading,
                    Title = x.Title,
                    Text = x.Text,
                    BtnText = x.BtnText,
                    Link = x.Link
                }).ToList();
        }
    }
}