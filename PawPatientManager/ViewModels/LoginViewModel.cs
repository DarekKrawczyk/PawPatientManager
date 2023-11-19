using PawPatientManager.Models;
using PawPatientManager.Services;
using PawPatientManager.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace PawPatientManager.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private string _username;
        private string _password;
        private string _errorMessage;
        private string _loginMessage;
        private bool _hasErrorMessage;
        private bool _hasLoginMessage;
        private INavigationService<HomeViewModel> _navHomeService;
        private AccountViewModel _accountViewModel;
        private AccountStore _accountStore;
        private VetSystem _vetSystem;
        public string ErrorMessage { get { return _errorMessage; } set { _errorMessage = value; OnPropertyChanged(nameof(ErrorMessage)); OnPropertyChanged(nameof(HasErrorMessage)); } }
        public string LoginMessage { get { return _loginMessage; } set { _loginMessage = value; OnPropertyChanged(nameof(LoginMessage)); OnPropertyChanged(nameof(HasLoginMessage)); } }
        public string Username { get { return _username; } set { _username = value; OnPropertyChanged(nameof(Username)); } }
        public string Password { get { return _password; } set { _password = value; OnPropertyChanged(nameof(Password)); } }
        public bool HasErrorMessage{ get { return !string.IsNullOrEmpty(_errorMessage); }  }
        public bool HasLoginMessage { get { return !string.IsNullOrEmpty(_loginMessage); }  }
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
