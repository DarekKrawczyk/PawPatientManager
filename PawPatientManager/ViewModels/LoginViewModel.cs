using PawPatientManager.Services;
using PawPatientManager.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PawPatientManager.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private string _username;
        private string _password;
        private INavigationService<HomeViewModel> _navHomeService;
        private AccountViewModel _accountViewModel;
        private AccountStore _accountStore;
        public string Username { get { return _username; } set { _username = value; OnPropertyChanged(nameof(Username)); } }
        public string Password { get { return _password; } set { _password = value; OnPropertyChanged(nameof(Password)); } }
        public ICommand CommandLogin { get; }
        public LoginViewModel(AccountStore accountStore, INavigationService<HomeViewModel> navHomeService) 
        {
            _navHomeService = navHomeService;
            _accountStore = accountStore;
            CommandLogin = new Commands.LoginVMCommands.Login(_navHomeService, this, _accountStore);
        }
    }
}
