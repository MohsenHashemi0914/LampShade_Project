using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductPicture;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductPictures
{
    public class IndexModel : PageModel
    {
        [TempData] public string Message { get; set; }

        public SelectList Products;
        public ProductPictureSearchModel SearchModel;
        public List<ProductPictureViewModel> ProductPictures;

        #region Constructor

        private readonly IProductPictureApplication _productPictureApplication;
        private readonly IProductApplication _productApplication;

        public IndexModel(IProductPictureApplication productPictureApplication,
            IProductApplication productApplication)
        {
            _productPictureApplication = productPictureApplication;
            _productApplication = productApplication;
        }

        #endregion

        public void OnGet(ProductPictureSearchModel searchModel)
        {
            ProductPictures = _productPictureApplication.Search(searchModel);
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreateProductPicture
            {
                Products = _productApplication.GetProducts()
            };
            return Partial("./Create", command);
        }

        public IActionResult OnPostCreate(CreateProductPicture command)
        {
            var result = _productPictureApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var productPicture = _productPictureApplication.GetDetails(id);
            productPicture.Products = _productApplication.GetProducts();
            return Partial("./Edit", productPicture);
        }

        public IActionResult OnPostEdit(EditProductPicture command)
        {
            var result = _productPictureApplication.Edit(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetRemove(long id)
        {
            var result = _productPictureApplication.Remove(id);

            if (!result.IsSucceeded)
                Message = result.Message;

            return RedirectToPage("./Index");
        }

        public IActionResult OnGetRestore(long id)
        {
            var result = _productPictureApplication.Restore(id);

            if (!result.IsSucceeded)
                Message = result.Message;

            return RedirectToPage("./Index");
        }
    }
}