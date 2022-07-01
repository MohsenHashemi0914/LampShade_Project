using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Domain.AccountAgg;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure.EFCore.Repository
{
    public class AccountRepository : BaseRepository<long, Account>, IAccountRepository
    {
        #region Constructor

        private readonly AccountContext _context;

        public AccountRepository(AccountContext context) : base(context)
        {
            _context = context;
        }

        #endregion

        public Account GetBy(string userName)
        {
            return _context.Accounts.FirstOrDefault(a => a.UserName == userName);
        }

        public EditAccount GetDetails(long id)
        {
            return _context.Accounts.Select(x => new EditAccount
            {
                Id = x.Id,
                RoleId = x.RoleId,
                FullName = x.FullName,
                UserName = x.UserName,
                Mobile = x.Mobile
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<AccountViewModel> GetAccouts()
        {
            return _context.Accounts.Select(x => new AccountViewModel
            {
                Id = x.Id,
                FullName = x.FullName
            }).ToList();
        }

        public List<AccountViewModel> Search(AccountSearchModel searchModel)
        {
            var query = _context.Accounts
                .Include(x => x.Role)
                .Select(x => new AccountViewModel
                {
                    Id = x.Id,
                    Role = x.Role.Name,
                    RoleId = x.RoleId,
                    Mobile = x.Mobile,
                    FullName = x.FullName,
                    UserName = x.UserName,
                    ProfilePhoto = x.ProfilePhoto,
                    CreationDate = x.CreationDate.ToFarsi()
                });

            if (searchModel.RoleId > 0)
                query = query.Where(x => x.RoleId == searchModel.RoleId);

            if (!string.IsNullOrWhiteSpace(searchModel.Mobile))
                query = query.Where(x => x.Mobile.Contains(searchModel.Mobile));

            if (!string.IsNullOrWhiteSpace(searchModel.FullName))
                query = query.Where(x => x.FullName.Contains(searchModel.FullName));

            if (!string.IsNullOrWhiteSpace(searchModel.UserName))
                query = query.Where(x => x.UserName.Contains(searchModel.UserName));

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
