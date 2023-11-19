using PawPatientManager.Models;
using PawPatientManager.Services;
using PawPatientManager.Stores;
using PawPatientManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
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
                _accountStore.CurrentAccount = new Vet(new Guid(), "Admin", "", "Admin", "Admin123");
                _navHomeService.Navigate();
                //try
                //{
                //    _loginViewModel.ErrorMessage = string.Empty;
                //    _loginViewModel.LoginMessage = string.Empty;
                //    string login = _loginViewModel.Username;
                //    string password = _loginViewModel.Password;

                //    Vet account = await _vetSystem.LoginVet(login, password);

                //    if (account != null)
                //    {
                //        _loginViewModel.LoginMessage = $"User {account.Name} {account.Surname} logged in successfully!";
                //        await Task.Delay(200);
                //        _accountStore.CurrentAccount = account;
                //        _navHomeService.Navigate();
                //    }
                //    else
                //    {
                //        _loginViewModel.Username = "";
                //        _loginViewModel.Password = "";
                //        _loginViewModel.ErrorMessage = "Failed to login this user!";
                //    }
                //} catch (Exception ex)
                //{
                //    MessageBox.Show(ex.Message, "LoginVMCommands.Login class");
                //}
            }
        }
    }
}
