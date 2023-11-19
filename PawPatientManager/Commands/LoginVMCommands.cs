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
        public class Login : AsyncCommandBase
        {
            private LoginViewModel _loginViewModel;
            private INavigationService<HomeViewModel> _navHomeService;
            private AccountStore _accountStore;
            private VetSystem _vetSystem;
            public Login(INavigationService<HomeViewModel> navHomeService, LoginViewModel loginViewModel, AccountStore accountStore,
                VetSystem vetSystem)
            {
                _vetSystem = vetSystem;
                _navHomeService = navHomeService;
                _loginViewModel = loginViewModel;
                _accountStore = accountStore;
            }

            public override async Task ExecuteAsync(object parameter)
            {
                try
                {
                    string login = _loginViewModel.Username;
                    string password = _loginViewModel.Password;

                    Vet account = await _vetSystem.LoginVet(login, password);

                    if (account != null)
                    {
                        MessageBox.Show($"User {account.Name} {account.Surname} logged in sucessfully!", "PetPatientManager", MessageBoxButton.OK);
                        _accountStore.CurrentAccount = account;
                        _navHomeService.Navigate();
                    }
                    else
                    {
                        MessageBox.Show("Invalid login!", "PetPatientManager", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                } catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "LoginVMCommands.Login class");
                }
            }
        }
    }
}
