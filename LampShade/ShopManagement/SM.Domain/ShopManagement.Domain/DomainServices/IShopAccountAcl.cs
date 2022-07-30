namespace ShopManagement.Domain.DomainServices
{
    public interface IShopAccountAcl
    {
        KeyValuePair<string, string> GetAccountBy(long id);
    }
}
