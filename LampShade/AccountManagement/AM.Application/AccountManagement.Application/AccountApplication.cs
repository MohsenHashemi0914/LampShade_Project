using _0_Framework.Application;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Domain.AccountAgg;

namespace AccountManagement.Application
{
    public class AccountApplication : IAccountApplication
    {
        #region Constructor

        private readonly IAuthHelper _authHelper;
        private readonly IFileUploader _fileUploader;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAccountRepository _accountRepository;

        public AccountApplication(IAuthHelper authHelper, IFileUploader fileUploader,
            IAccountRepository accountRepository, IPasswordHasher passwordHasher)
        {
            _authHelper = authHelper;
            _fileUploader = fileUploader;
            _passwordHasher = passwordHasher;
            _accountRepository = accountRepository;
        }

        #endregion

        public OperationResult Register(RegisterAccount command)
        {
            var operation = new OperationResult();
            if (_accountRepository.IsExist(x => x.UserName == command.UserName || x.Mobile == command.Mobile))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var password = _passwordHasher.Hash(command.Password);
            var picturePath = "ProfilePhotos";
            var fileName = _fileUploader.Upload(command.ProfilePhoto, picturePath);

            var account = new Account(command.FullName, command.UserName, 
                password, command.Mobile, fileName, command.RoleId);
            _accountRepository.Add(account);
            _accountRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditAccount command)
        {
            var operation = new OperationResult();
            var account = _accountRepository.Get(command.Id);
            if(account is null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_accountRepository.IsExist(x => x.Id != command.Id
            && (x.UserName == command.UserName || x.Mobile == command.Mobile)))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var picturePath = "ProfilePhotos";
            var fileName = _fileUploader.Upload(command.ProfilePhoto, picturePath);
            account.Edit(command.FullName, command.UserName, 
                command.Mobile, fileName, command.RoleId);
            _accountRepository.SaveChanges();

            return operation.Succeeded();
        }

        public OperationResult ChangePassword(ChangePassword command)
        {
            var operation = new OperationResult();

            if(command.Password != command.RePassword)
                return operation.Failed(ApplicationMessages.PasswordsNotMatch);

            var account = _accountRepository.Get(command.Id);
            if(account is null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            var password = _passwordHasher.Hash(command.Password);
            account.ChangePassword(password);
            _accountRepository.SaveChanges();

            return operation.Succeeded();
        }

        public OperationResult Login(Login command)
        {
            var operation = new OperationResult();
            var account = _accountRepository.GetBy(command.UserName);
            if (account is null)
                return operation.Failed(ApplicationMessages.WrongData);

            (bool verified, bool needsUpgrade) checkResult = _passwordHasher.Check(account.Password, command.Password);
            if(!checkResult.verified)
                return operation.Failed(ApplicationMessages.WrongData);

            var authViewModel = new AuthViewModel(account.Id, account.RoleId, account.FullName, account.UserName);
            _authHelper.Signin(authViewModel);
            return operation.Succeeded();
        }

        public void Logout()
        {
            _authHelper.Signout();
        }

        public EditAccount GetDetails(long id)
        {
            return _accountRepository.GetDetails(id);
        }

        public List<AccountViewModel> Search(AccountSearchModel searchModel)
        {
            return _accountRepository.Search(searchModel);
        }
    }
}
