namespace ShopManagement.Domain.DomainServices
{
    public interface IShopAccountAcl
    {
        (string name, string mobile) GetAccountBy(long id);
    }
}
