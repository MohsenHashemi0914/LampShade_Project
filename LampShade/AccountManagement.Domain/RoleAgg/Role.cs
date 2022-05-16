using _0_Framework.Domain;
using AccountManagement.Domain.AccountAgg;

namespace AccountManagement.Domain.RoleAgg
{
    public class Role : BaseEntity<long>
    {
        public string Name { get; private set; }
        public List<Account> Accounts { get; private set; }

        public Role(string name)
        {
            Name = name;
            Accounts = new();
        }

        public void Edit(string name)
        {
            Name = name;
        }
    }
}
