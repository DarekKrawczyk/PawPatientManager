
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
using System.Windows.Input;

namespace PawPatientManager.Commands
{
    public struct ManageOwnersViewModelCommands
    {
        public class DeleteOwner : CommandBase
        {
            private ManageOwnersViewModel _manageOwnersVM;
            public DeleteOwner(ManageOwnersViewModel manageOwnersVM)
            {
                _manageOwnersVM = manageOwnersVM;
            }
            public override void Execute(object? parameter)
            {
                bool result = false;
                OwnerViewModel ownerVM = _manageOwnersVM.SelectedOwner;
                if(ownerVM != null)
                {
                    result = _manageOwnersVM.DeleteOwner(ownerVM);
                }
                string text = (result == true) ? "deleted succesfully!" : "not deleted!";
                MessageBox.Show($"Owner {text}", "PawPatientManager", MessageBoxButton.OK);
            }
        }
        public class EditOwner : CommandBase
        {
            private ManageOwnersViewModel _manageOwnersVM;
            private LayoutNavigationServiceParam<OwnerViewModel, EditOwnerViewModel> _navService;
            public EditOwner(ManageOwnersViewModel manageOwnersVM, LayoutNavigationServiceParam<OwnerViewModel, EditOwnerViewModel> navService)
            {
                _manageOwnersVM = manageOwnersVM;
                _navService = navService;
            }
            public override void Execute(object? parameter)
            {
                OwnerViewModel ownerVM = _manageOwnersVM.SelectedOwner;
                if (ownerVM is null || ownerVM.IsNull() == true)
                {
                    MessageBox.Show("Invalid owner selected", "PawPatientManager", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else _navService.Navigate(ownerVM);
            }
        }

        public class RelayCommand : CommandBase
        {
            private readonly Action<object> _execute;
            private readonly Func<object, bool> _canExecute;

            public RelayCommand(Action<object> execute, Func<object, bool> canExecute)
            {
                _execute = execute;
                _canExecute = canExecute;
            }

            public bool CanExecute(object parameter)
            {
                return _canExecute == null || _canExecute(parameter);
            }

            public override void Execute(object? parameter)
            {
                _execute(parameter);
            }
        }

        public class UpdateSelected : CommandBase
        {
            private ManageOwnersViewModel _manageOwnerVM;
            private readonly Action<object> _execute;
            private readonly Func<object, bool> _canExecute;

            public UpdateSelected(ManageOwnersViewModel manageOwnerVM)
            {
                _manageOwnerVM = manageOwnerVM;
            }

            public bool CanExecute(object parameter)
            {
                return _canExecute == null || _canExecute(parameter);
            }

            public override void Execute(object? parameter)
            {
                if (parameter is OwnerViewModel selectedItem)
                {
                    _manageOwnerVM.SelectedOwner = selectedItem;
                }
                else
                {
                    _manageOwnerVM.SelectedOwner = null;
                }
            }
        }

        //public class AddOwner : CommandBase
        //{
        //    private NavigationStore _navigator;
        //    //private NavigationBarViewModel _navigationBarVM;
        //    private VetSystem _vetSystem;

        //    public AddOwner(NavigationStore navigator, VetSystem vetSystem)
        //    {
        //        _navigator = navigator;
        //        _vetSystem = vetSystem;
        //        //_navigationBarVM = navigationBarVM;
        //    }
        //    public override void Execute(object? parameter)
        //    {
        //        _navigator.CurrentViewModel = new OwnerRegistrationViewModel(_vetSystem, _navigator);
        //        //_navigator.CurrentViewModel = new OwnerRegistrationViewModel(_vetSystem, _navigator, _navigationBarVM);
        //    }
        //}
    }
}
