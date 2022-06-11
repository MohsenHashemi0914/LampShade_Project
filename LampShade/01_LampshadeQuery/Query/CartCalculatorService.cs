using _0_Framework.Application;
using _0_Framework.Infrastructure;
using _01_LampshadeQuery.Contracts;
using DiscountManagement.Infrastructure.EFCore;
using ShopManagement.Application.Contracts.Order;

namespace _01_LampshadeQuery.Query
{
    public class CartCalculatorService : ICartCalculatorService
    {
        #region Constructor

        private readonly DiscountContext _discountContext;
        private readonly IAuthHelper _authHelper;

        public CartCalculatorService(DiscountContext discountContext, IAuthHelper authHelper)
        {
            _discountContext = discountContext;
            _authHelper = authHelper;
        }

        #endregion

        public Cart ComputeCart(List<CartItem> cartItems)
        {
            var cart = new Cart();
            if (!cartItems.Any()) return cart;

            var colleagueDiscounts = _discountContext.ColleagueDiscounts
                .Where(x => !x.IsRemoved)
                .Select(x => new { x.ProductId, x.DiscountRate }).ToList();

            var customerDiscounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate }).ToList();

            foreach (var cartItem in cartItems)
            {
                if (_authHelper.CurrentAccountRole() == Roles.ColleagueUser)
                {
                    var colleagueDiscount = colleagueDiscounts.FirstOrDefault(x => x.ProductId == cartItem.Id);
                    if (colleagueDiscount is not null)
                        cartItem.CalculateItemDiscount(colleagueDiscount.DiscountRate);
                }
                else
                {
                    var customerDiscount = customerDiscounts.FirstOrDefault(x => x.ProductId == cartItem.Id);
                    if (customerDiscount is not null)
                        cartItem.CalculateItemDiscount(customerDiscount.DiscountRate);
                }
                cart.Add(cartItem);
            }

            return cart;
        }
    }
}
