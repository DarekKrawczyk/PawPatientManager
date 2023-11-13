using PawPatientManager.Models;
using PawPatientManager.Services;
using PawPatientManager.Stores;
using PawPatientManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PawPatientManager.Commands
{
    public struct LoginVMCommands 
    {
        public class Login : CommandBase
        {
            private LoginViewModel _loginViewModel;
            private INavigationService<HomeViewModel> _navHomeService;
            private AccountStore _accountStore;
            public Login(INavigationService<HomeViewModel> navHomeService, LoginViewModel loginViewModel, AccountStore accountStore)
            {
                _navHomeService = navHomeService;
                _loginViewModel = loginViewModel;
                _accountStore = accountStore;
            }
            public override void Execute(object? parameter)
            {
                Account account = new Account(_loginViewModel.Username, _loginViewModel.Password);
                bool isValid = account.IsValid();

                if(isValid == true) 
                {
                    _accountStore.CurrentAccount = account;
                    _navHomeService.Navigate();
                }
                else
                {
                    MessageBox.Show("Invalid login!", "PetPatientManager", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
