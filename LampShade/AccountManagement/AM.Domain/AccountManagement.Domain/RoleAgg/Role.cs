using _0_Framework.Domain;
using AccountManagement.Domain.AccountAgg;

namespace AccountManagement.Domain.RoleAgg
{
    public class Role : BaseEntity<long>
    {
        public string Name { get; private set; }
        public List<Account> Accounts { get; private set; }
        public List<Permission> Permissions { get; private set; }

        protected Role()
        {
        }

        public Role(string name, List<Permission> permissions)
        {
            Name = name;
            Accounts = new();
            Permissions = permissions;
        }

        public void Edit(string name, List<Permission> permissions)
        {
            Name = name;
            Permissions = permissions;
        }
    }
}
