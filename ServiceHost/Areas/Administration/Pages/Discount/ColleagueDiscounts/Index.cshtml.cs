using DiscountManagement.Application.Contracts.ColleagueDiscount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using System.Collections.Generic;
using System.Linq;
using ProductViewModel = DiscountManagement.Application.Contracts.CustomerDiscount.ProductViewModel;

namespace ServiceHost.Areas.Administration.Pages.Discount.ColleagueDiscounts
{
    public class IndexModel : PageModel
    {
        [TempData] public string Message { get; set; }

        public SelectList Products;
        public ColleagueDiscountSearchModel SearchModel;
        public List<ColleagueDiscountViewModel> ColleagueDiscounts;

        #region Constructor

        private readonly IProductApplication _productApplication;
        private readonly IColleagueDiscountApplication _colleagueDiscountApplication;

        public IndexModel(IProductApplication productApplication, IColleagueDiscountApplication colleagueDiscountApplication)
        {
            _productApplication = productApplication;
            _colleagueDiscountApplication = colleagueDiscountApplication;
        }

        #endregion

        public void OnGet(ColleagueDiscountSearchModel searchModel)
        {
            ColleagueDiscounts = _colleagueDiscountApplication.Search(searchModel);
            Products = new SelectList(GetProductsForSelect(), "Id", "Name");
        }

        public IActionResult OnGetCreate()
        {
            var command = new DefineColleagueDiscount
            {
                Products = GetProductsForSelect()
            };
            return Partial("./Create", command);
        }

        public IActionResult OnPostCreate(DefineColleagueDiscount command)
        {
            var result = _colleagueDiscountApplication.Define(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var colleagueDiscount = _colleagueDiscountApplication.GetDetails(id);
            colleagueDiscount.Products = GetProductsForSelect();
            return Partial("./Edit", colleagueDiscount);
        }

        public IActionResult OnPostEdit(EditColleagueDiscount command)
        {
            var result = _colleagueDiscountApplication.Edit(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetRemove(long id)
        {
            var result = _colleagueDiscountApplication.Remove(id);

            if (!result.IsSucceeded)
                Message = result.Message;

            return RedirectToPage("./Index");
        }

        public IActionResult OnGetRestore(long id)
        {
            var result = _colleagueDiscountApplication.Restore(id);

            if (!result.IsSucceeded)
                Message = result.Message;

            return RedirectToPage("./Index");
        }

        #region Utilities

        private List<ProductViewModel> GetProductsForSelect()
        {
            return _productApplication.GetProducts().Select(x => new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }

        #endregion
    }
}