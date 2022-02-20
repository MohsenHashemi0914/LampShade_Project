using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using _0_Framework.Infrastructure;
using ShopManagement.Application.Contracts.Slide;
using ShopManagement.Domain.SlideAgg;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class SlideRepository : BaseRepository<long, Slide>, ISlideRepository
    {
        #region Constructor

        private readonly ShopContext _context;

        public SlideRepository(ShopContext context) : base(context)
        {
            _context = context;
        }

        #endregion

        public EditSlide GetDetails(long id)
        {
            return _context.Slides.Select(x => new EditSlide
            {
                Id = x.Id,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Title = x.Title,
                Heading = x.Heading,
                Text = x.Text,
                BtnText = x.BtnText
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<SlideViewModel> GetList()
        {
            return _context.Slides.Select(x => new SlideViewModel
            {
                Id = x.Id,
                Picture = x.Title,
                Heading = x.Heading,
                Title = x.Title,
                CreationDate = x.CreationDate.ToString(CultureInfo.InvariantCulture),
                IsRemoved = x.IsRemoved
            }).OrderByDescending(x => x.Id).ToList();
        }
    }
}