#pragma checksum "E:\My-First-Real-Project\ServiceHost\Pages\Shared\Components\ProductCategoryWithProducts\Default.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8411df0f9cee440ff4e86ecc5e5c2c35675082ce"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(ServiceHost.Pages.Shared.Components.ProductCategoryWithProducts.Pages_Shared_Components_ProductCategoryWithProducts_Default), @"mvc.1.0.view", @"/Pages/Shared/Components/ProductCategoryWithProducts/Default.cshtml")]
namespace ServiceHost.Pages.Shared.Components.ProductCategoryWithProducts
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "E:\My-First-Real-Project\ServiceHost\Pages\_ViewImports.cshtml"
using ServiceHost;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8411df0f9cee440ff4e86ecc5e5c2c35675082ce", @"/Pages/Shared/Components/ProductCategoryWithProducts/Default.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d027006424b9e12b1709732f146fce9f1d78e6a1", @"/Pages/_ViewImports.cshtml")]
    #nullable restore
    public class Pages_Shared_Components_ProductCategoryWithProducts_Default : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<_01_LampshadeQuery.Contracts.ProductCategory.ProductCategoryQueryModel>>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("img-fluid"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"
<div class=""single-row-slider-tab-area section-space"">
    <div class=""container"">
        <div class=""row"">
            <div class=""col-lg-12"">
                <div class=""section-title-wrapper text-center section-space--half"">
                    <h2 class=""section-title"">محصولات ما</h2>
                    <p class=""section-subtitle"">
                        لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است
                    </p>
                </div>
            </div>
        </div>
        <div class=""row"">
            <div class=""col-lg-12"">
                <div class=""tab-slider-wrapper"">

                    <div class=""tab-product-navigation"">
                        <div class=""nav nav-tabs justify-content-center"" id=""nav-tab2"" role=""tablist"">
");
#nullable restore
#line 21 "E:\My-First-Real-Project\ServiceHost\Pages\Shared\Components\ProductCategoryWithProducts\Default.cshtml"
                             if (Model != null && Model.Any())
                            {
                                foreach (var category in Model)
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <a");
            BeginWriteAttribute("class", " class=\"", 1215, "\"", 1285, 3);
            WriteAttributeValue("", 1223, "nav-item", 1223, 8, true);
            WriteAttributeValue(" ", 1231, "nav-link", 1232, 9, true);
#nullable restore
#line 25 "E:\My-First-Real-Project\ServiceHost\Pages\Shared\Components\ProductCategoryWithProducts\Default.cshtml"
WriteAttributeValue(" ", 1240, category == Model.First() ? "active" : "", 1241, 44, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("id", " id=\"", 1286, "\"", 1315, 2);
            WriteAttributeValue("", 1291, "product-tab-", 1291, 12, true);
#nullable restore
#line 25 "E:\My-First-Real-Project\ServiceHost\Pages\Shared\Components\ProductCategoryWithProducts\Default.cshtml"
WriteAttributeValue("", 1303, category.Id, 1303, 12, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" data-toggle=\"tab\"");
            BeginWriteAttribute("href", "\r\n                                       href=\"", 1334, "\"", 1409, 2);
            WriteAttributeValue("", 1381, "#product-series-", 1381, 16, true);
#nullable restore
#line 26 "E:\My-First-Real-Project\ServiceHost\Pages\Shared\Components\ProductCategoryWithProducts\Default.cshtml"
WriteAttributeValue("", 1397, category.Id, 1397, 12, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" role=\"tab\" aria-selected=\"true\">");
#nullable restore
#line 26 "E:\My-First-Real-Project\ServiceHost\Pages\Shared\Components\ProductCategoryWithProducts\Default.cshtml"
                                                                                                      Write(category.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a>\r\n");
#nullable restore
#line 27 "E:\My-First-Real-Project\ServiceHost\Pages\Shared\Components\ProductCategoryWithProducts\Default.cshtml"
                                }
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("                        </div>\r\n                    </div>\r\n\r\n                    <div class=\"tab-content\">\r\n");
#nullable restore
#line 33 "E:\My-First-Real-Project\ServiceHost\Pages\Shared\Components\ProductCategoryWithProducts\Default.cshtml"
                         if (Model != null && Model.Any())
                        {
                            foreach (var category in Model)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <div");
            BeginWriteAttribute("class", " class=\"", 1853, "\"", 1924, 4);
            WriteAttributeValue("", 1861, "tab-pane", 1861, 8, true);
            WriteAttributeValue(" ", 1869, "fade", 1870, 5, true);
            WriteAttributeValue(" ", 1874, "show", 1875, 5, true);
#nullable restore
#line 37 "E:\My-First-Real-Project\ServiceHost\Pages\Shared\Components\ProductCategoryWithProducts\Default.cshtml"
WriteAttributeValue(" ", 1879, category == Model.First() ? "active" : "", 1880, 44, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("id", " id=\"", 1925, "\"", 1957, 2);
            WriteAttributeValue("", 1930, "product-series-", 1930, 15, true);
#nullable restore
#line 37 "E:\My-First-Real-Project\ServiceHost\Pages\Shared\Components\ProductCategoryWithProducts\Default.cshtml"
WriteAttributeValue("", 1945, category.Id, 1945, 12, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" role=\"tabpanel\"");
            BeginWriteAttribute("aria-labelledby", "\r\n                                     aria-labelledby=\"", 1974, "\"", 2054, 2);
            WriteAttributeValue("", 2030, "product-tab-", 2030, 12, true);
#nullable restore
#line 38 "E:\My-First-Real-Project\ServiceHost\Pages\Shared\Components\ProductCategoryWithProducts\Default.cshtml"
WriteAttributeValue("", 2042, category.Id, 2042, 12, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@">
                                    <div class=""single-row-slider-wrapper"">
                                        <div class=""ht-slick-slider"" data-slick-setting='{
                                    ""slidesToShow"": 4,
                                    ""slidesToScroll"": 1,
                                    ""arrows"": true,
                                    ""autoplay"": false,
                                    ""autoplaySpeed"": 5000,
                                    ""speed"": 1000,
                                    ""infinite"": true,
                                    ""rtl"": true,
                                    ""prevArrow"": {""buttonClass"": ""slick-prev"", ""iconClass"": ""ion-chevron-left"" },
                                    ""nextArrow"": {""buttonClass"": ""slick-next"", ""iconClass"": ""ion-chevron-right"" }
                                }' data-slick-responsive='[
                                    {""breakpoint"":1501, ""settings"": {""slidesToShow"": 4} },
                             ");
            WriteLiteral(@"       {""breakpoint"":1199, ""settings"": {""slidesToShow"": 4, ""arrows"": false} },
                                    {""breakpoint"":991, ""settings"": {""slidesToShow"": 3, ""arrows"": false} },
                                    {""breakpoint"":767, ""settings"": {""slidesToShow"": 2, ""arrows"": false} },
                                    {""breakpoint"":575, ""settings"": {""slidesToShow"": 2, ""arrows"": false} },
                                    {""breakpoint"":479, ""settings"": {""slidesToShow"": 1, ""arrows"": false} }
                                ]'>
");
#nullable restore
#line 59 "E:\My-First-Real-Project\ServiceHost\Pages\Shared\Components\ProductCategoryWithProducts\Default.cshtml"
                                             if (category.Products != null && category.Products.Any())
                                            {
                                                foreach (var product in category.Products)
                                                {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                                                    <div class=""col"">
                                                        <div class=""single-grid-product"">
                                                            <div class=""single-grid-product__image"">
");
#nullable restore
#line 66 "E:\My-First-Real-Project\ServiceHost\Pages\Shared\Components\ProductCategoryWithProducts\Default.cshtml"
                                                                 if (product.HasDiscount)
                                                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                                    <div class=\"single-grid-product__label\">\r\n                                                                        <span class=\"sale\">-");
#nullable restore
#line 69 "E:\My-First-Real-Project\ServiceHost\Pages\Shared\Components\ProductCategoryWithProducts\Default.cshtml"
                                                                                       Write(product.DiscountRate);

#line default
#line hidden
#nullable disable
            WriteLiteral("%</span>\r\n                                                                    </div>\r\n");
#nullable restore
#line 71 "E:\My-First-Real-Project\ServiceHost\Pages\Shared\Components\ProductCategoryWithProducts\Default.cshtml"
                                                                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                                <a href=\"single-product.html\">\r\n                                                                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "8411df0f9cee440ff4e86ecc5e5c2c35675082ce13291", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "src", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 4893, "~/ProductPictures/", 4893, 18, true);
#nullable restore
#line 73 "E:\My-First-Real-Project\ServiceHost\Pages\Shared\Components\ProductCategoryWithProducts\Default.cshtml"
AddHtmlAttributeValue("", 4911, product.Picture, 4911, 16, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "title", 1, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#nullable restore
#line 73 "E:\My-First-Real-Project\ServiceHost\Pages\Shared\Components\ProductCategoryWithProducts\Default.cshtml"
AddHtmlAttributeValue("", 4936, product.PictureTitle, 4936, 21, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "alt", 1, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#nullable restore
#line 74 "E:\My-First-Real-Project\ServiceHost\Pages\Shared\Components\ProductCategoryWithProducts\Default.cshtml"
AddHtmlAttributeValue("", 5056, product.PictureAlt, 5056, 19, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
                                                                </a>
                                                            </div>
                                                            <div class=""single-grid-product__content"">
                                                                <div class=""single-grid-product__category-rating"">
                                                                    <span class=""category"">
                                                                        <a href=""shop-left-sidebar.html"">");
#nullable restore
#line 80 "E:\My-First-Real-Project\ServiceHost\Pages\Shared\Components\ProductCategoryWithProducts\Default.cshtml"
                                                                                                    Write(product.Category);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</a>
                                                                    </span>
                                                                    <span class=""rating"">
                                                                        <i class=""ion-android-star active""></i>
                                                                        <i class=""ion-android-star active""></i>
                                                                        <i class=""ion-android-star active""></i>
                                                                        <i class=""ion-android-star active""></i>
                                                                        <i class=""ion-android-star-outline""></i>
                                                                    </span>
                                                                </div>

                                                                <h3 class=""single-grid-product__title"">
                            ");
            WriteLiteral("                                        <a href=\"single-product.html\">\r\n                                                                        ");
#nullable restore
#line 93 "E:\My-First-Real-Project\ServiceHost\Pages\Shared\Components\ProductCategoryWithProducts\Default.cshtml"
                                                                   Write(product.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                                                    </a>\r\n                                                                </h3>\r\n                                                                <p class=\"single-grid-product__price\">\r\n");
#nullable restore
#line 97 "E:\My-First-Real-Project\ServiceHost\Pages\Shared\Components\ProductCategoryWithProducts\Default.cshtml"
                                                                     if (product.HasDiscount)
                                                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                                        <span class=\"discounted-price\">");
#nullable restore
#line 99 "E:\My-First-Real-Project\ServiceHost\Pages\Shared\Components\ProductCategoryWithProducts\Default.cshtml"
                                                                                                  Write(product.PriceWithDiscount);

#line default
#line hidden
#nullable disable
            WriteLiteral(" تومان</span>\r\n");
#nullable restore
#line 100 "E:\My-First-Real-Project\ServiceHost\Pages\Shared\Components\ProductCategoryWithProducts\Default.cshtml"
                                                                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                                    <span");
            BeginWriteAttribute("class", " class=\"", 7538, "\"", 7599, 2);
            WriteAttributeValue("", 7546, "main-price", 7546, 10, true);
#nullable restore
#line 101 "E:\My-First-Real-Project\ServiceHost\Pages\Shared\Components\ProductCategoryWithProducts\Default.cshtml"
WriteAttributeValue(" ", 7556, product.HasDiscount ? "discounted" : "", 7557, 42, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 101 "E:\My-First-Real-Project\ServiceHost\Pages\Shared\Components\ProductCategoryWithProducts\Default.cshtml"
                                                                                                                                   Write(product.Price);

#line default
#line hidden
#nullable disable
            WriteLiteral(@" تومان</span>
                                                                </p>
                                                            </div>
                                                        </div>
                                                    </div>
");
#nullable restore
#line 106 "E:\My-First-Real-Project\ServiceHost\Pages\Shared\Components\ProductCategoryWithProducts\Default.cshtml"
                                                }
                                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                        </div>\r\n                                    </div>\r\n                                </div>\r\n");
#nullable restore
#line 112 "E:\My-First-Real-Project\ServiceHost\Pages\Shared\Components\ProductCategoryWithProducts\Default.cshtml"
                            }
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n\r\n");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<_01_LampshadeQuery.Contracts.ProductCategory.ProductCategoryQueryModel>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
