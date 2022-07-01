using _0_Framework.Application;

namespace ShopManagement.Application.Contracts.Order
{
    public interface IOrderApplication
    {
        long PlaceOrder(Cart cart);
        double GetAmountBy(long id);
        OperationResult Cancel(long id);
        string PaymentSucceeded(long orderId, long refId);
        List<OrderItemViewModel> GetItemsBy(long orderId);
        List<OrderViewModel> Search(OrderSearchModel searchModel);
    }
}
