using PawPatientManager.Models;
using PawPatientManager.Services;
using PawPatientManager.Stores;
using PawPatientManager.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PawPatientManager.Commands
{
    public struct HomeCommands
    {
        public class UpdateSelected : CommandBase
        {
            private HomeViewModel _petsViewModel;
            private readonly Action<object> _execute;
            private readonly Func<object, bool> _canExecute;

            public UpdateSelected(HomeViewModel petsViewModel)
            {
                _petsViewModel = petsViewModel;
            }

            public bool CanExecute(object parameter)
            {
                return _canExecute == null || _canExecute(parameter);
            }

            public override void Execute(object? parameter)
            {
                if (parameter is VisitViewModel selectedItem)
                {
                    _petsViewModel.SelectedVisit = selectedItem;
                }
                else
                {
                    _petsViewModel.SelectedVisit = null;
                }
            }
        }
        public class LoadVisits : AsyncCommandBase
        {
            private VetSystem _system;
            private HomeViewModel _viewModel;
            public LoadVisits(VetSystem system, HomeViewModel viewModel)
            {
                _system = system;
                _viewModel = viewModel;
            }

            public override async Task ExecuteAsync(object parameter)
            {
                try
                {
                    IEnumerable<Visit> visits = await _system.GetAllVisitsFromVetAsync(_viewModel.AccountStore.CurrentAccount);
                    _viewModel.ReloadVisits(visits);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "LoadVisits class");
                }
            }
        }
        public class Prescribe : AsyncCommandBase
        {
            private HomeViewModel _visitsVM;
            private VetSystem _vetSystem;
            public Prescribe(VetSystem vetSystem, HomeViewModel visitsVM)
            {
                _visitsVM = visitsVM;
                _vetSystem = vetSystem;
                _visitsVM.PropertyChanged += _visitsVM_PropertyChanged1;
            }

            private void _visitsVM_PropertyChanged1(object? sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == nameof(HomeViewModel.SelectedVisit))
                {
                    OnCanExecutedChange();
                }
            }

            public override bool CanExecute(object? parameter)
            {
                return (_visitsVM.SelectedVisit != null) && (!_visitsVM.SelectedVisit.IsNull()) && base.CanExecute(parameter);
            }
            public override async Task ExecuteAsync(object parameter)
            {
                try
                {
                    Visit editedMed = new Visit(_visitsVM.SelectedVisit);
                    MedicalReceipt receipt = new MedicalReceipt(new Guid(), DateTime.Now, _visitsVM.MedicalReceipt);
                    // First delete
                    await _vetSystem.AddReceiptToVisit(editedMed, receipt);

                    // Then refresh data
                    IEnumerable<Visit> meds = await _vetSystem.GetAllVisitsFromVetAsync(_visitsVM.AccountStore.CurrentAccount);
                    _visitsVM.ReloadVisits(meds);
                    _visitsVM.MedicalReceipt = string.Empty;
                    MessageBox.Show($"Medication prescribed!", "PawPatientManager", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "DeleteMed class");
                }
            }
        }
    }
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
                //_accountStore.CurrentAccount = new Vet(new Guid(), "Admin", "", "Admin", "Admin123");
                //_navHomeService.Navigate();
                try
                {
                    _loginViewModel.ErrorMessage = string.Empty;
                    _loginViewModel.LoginMessage = string.Empty;
                    string login = _loginViewModel.Username;
                    string password = _loginViewModel.Password;

                    Vet account = await _vetSystem.LoginVet(login, password);

                    if (account != null)
                    {
                        _loginViewModel.LoginMessage = $"User {account.Name} {account.Surname} logged in successfully!";
                        await Task.Delay(200);
                        _accountStore.CurrentAccount = account;
                        _navHomeService.Navigate();
                    }
                    else
                    {
                        _loginViewModel.Username = "";
                        _loginViewModel.Password = "";
                        _loginViewModel.ErrorMessage = "Failed to login this user!";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "LoginVMCommands.Login class");
                }
            }
        }
    }
}
