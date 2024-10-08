﻿
using PawPatientManager.Models;
using PawPatientManager.Services;
using PawPatientManager.Stores;
using PawPatientManager.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PawPatientManager.Commands
{
    public struct ManageOwnersViewModelCommands
    {
        public class DeleteOwner : AsyncCommandBase
        {
            private VetSystem _system;
            private ManageOwnersViewModel _manageOwnersVM;
            public DeleteOwner(VetSystem vetSystem, ManageOwnersViewModel manageOwnersVM)
            {
                _system = vetSystem;
                _manageOwnersVM = manageOwnersVM;
                _manageOwnersVM.PropertyChanged += _ownerDeleteVM_PropertyChanged;
            }
            private void _ownerDeleteVM_PropertyChanged(object? sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == nameof(ManageOwnersViewModel.SelectedOwner))
                {
                    OnCanExecutedChange();
                }
            }
            public override bool CanExecute(object? parameter)
            {
                return (_manageOwnersVM.SelectedOwner != null && !_manageOwnersVM.SelectedOwner.IsNull()) && base.CanExecute(parameter);
            }

            public override async Task ExecuteAsync(object parameter)
            {
                try
                {
                    Owner ownerToBeDeleted = new Owner(_manageOwnersVM.SelectedOwner);
                    // First delete
                    await _system.DeleteOwner(ownerToBeDeleted);

                    // Then refresh data
                    IEnumerable<Owner> meds = await _system.GetAllOwnersAsync();
                    _manageOwnersVM.ReloadOwners(meds);

                    MessageBox.Show("Owner deleted", "PawPatientManager", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "DeleteMed class");
                }
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
                _manageOwnersVM.PropertyChanged += _ownerEditVM_PropertyChanged;
            }
            private void _ownerEditVM_PropertyChanged(object? sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == nameof(ManageOwnersViewModel.SelectedOwner))
                {
                    OnCanExecutedChange();
                }
            }
            public override bool CanExecute(object? parameter)
            {
                return (_manageOwnersVM.SelectedOwner != null && !_manageOwnersVM.SelectedOwner.IsNull()) && base.CanExecute(parameter);
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

        public class LoadOwners : AsyncCommandBase
        {
            private VetSystem _system;
            private ManageOwnersViewModel _viewModel;
            public LoadOwners(VetSystem system, ManageOwnersViewModel viewModel)
            {
                _system = system;
                _viewModel = viewModel;
            }

            public override async Task ExecuteAsync(object parameter)
            {
                try
                {
                    IEnumerable<Owner> owners = await _system.GetAllOwnersAsync();
                    _viewModel.ReloadOwners(owners);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "LoadMeds class");
                }
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
    }
}
