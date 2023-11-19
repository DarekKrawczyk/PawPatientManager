using PawPatientManager.Models;
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
        private VetSystem _vetSystem;
        public string Username { get { return _username; } set { _username = value; OnPropertyChanged(nameof(Username)); } }
        public string Password { get { return _password; } set { _password = value; OnPropertyChanged(nameof(Password)); } }
        public ICommand CommandLogin { get; }
        public LoginViewModel(VetSystem vetSystem, AccountStore accountStore, INavigationService<HomeViewModel> navHomeService) 
        {
            _vetSystem = vetSystem;
            _navHomeService = navHomeService;
            _accountStore = accountStore;
            CommandLogin = new Commands.LoginVMCommands.Login(_navHomeService, this, _accountStore, _vetSystem);
        }
    }
}
