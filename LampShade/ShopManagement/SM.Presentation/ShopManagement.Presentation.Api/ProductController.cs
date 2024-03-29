﻿using _01_LampshadeQuery.Contracts.Product;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        #region Constructor

        private readonly IProductQuery _productQuery;

        public ProductController(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        #endregion

        [HttpGet]
        public List<ProductQueryModel> GetLatestArrivals()
        {
            return _productQuery.GetLatestArrivals();
        }
    }
}
