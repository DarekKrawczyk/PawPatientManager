using PawPatientManager.Models;
using PawPatientManager.Services;
using PawPatientManager.ViewModels;
using PawPatientManager.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static PawPatientManager.Commands.ManageOwnersViewModelCommands;
using System.Windows.Input;

namespace PawPatientManager.Commands
{
    public struct EditPetCommand
    {
        public class RegisterPet : CommandBase
        {
            private static uint _idIterator;
            private VetSystem _vetSystem;
            private RegisterPetViewModel _editPetVM;
            public RegisterPet(VetSystem vetSystem, RegisterPetViewModel editPetVM)
            {
                _vetSystem = vetSystem;
                _editPetVM = editPetVM;
                //_editPetVM.PropertyChanged += _ownerRegistrationViewModel_PropertyChanged;
            }

            //THIS COMMENTED CODE IS FOR CHECKING VIEW'S FIELDS 

            //private void _ownerRegistrationViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
            //{
            //    if (e.PropertyName == nameof(OwnerRegistrationViewModel.Name))
            //    {
            //        OnCanExecutedChange();
            //    }
            //}
            //public override bool CanExecute(object? parameter)
            //{
            //    return !string.IsNullOrEmpty(_editPetVM.Name) && base.CanExecute(parameter);
            //}

            public override void Execute(object? parameter)
            {
                if(_editPetVM == null || _editPetVM.Owners == null)
                {
                    MessageBox.Show("Please select owner of the pet!", "PawPatientManager", MessageBoxButton.OK);
                }
                else
                {
                    Pet newPet = new Pet(
                        _editPetVM.ID,
                        _editPetVM.Name,
                        _editPetVM.Gender,
                        new Owner(_editPetVM.SelectedOwner),
                        _editPetVM.BirthDate,
                        null,
                        null,
                        _editPetVM.Spieces,
                        _editPetVM.Race,
                        _editPetVM.MicrochipNumber
                        );
                    _vetSystem.AddPetToOwner(newPet);
                }
            }
        }
        public class EditPet : CommandBase
        {
            private VetSystem _vetSystem;
            private EditPetViewModel _editPetVM;
            public EditPet(VetSystem vetSystem, EditPetViewModel editPetVM)
            {
                _vetSystem = vetSystem;
                _editPetVM = editPetVM;
            }
            public override bool CanExecute(object? parameter)
            {
                return !string.IsNullOrEmpty(_editPetVM.Name) && base.CanExecute(parameter);
            }

            public override void Execute(object? parameter)
            {
                Pet editedPet = new Pet(
                    _editPetVM.ID,
                    _editPetVM.Name,
                    _editPetVM.Gender,
                    _editPetVM.Owner,
                    _editPetVM.BirthDate,
                    _editPetVM.Visits,
                    _editPetVM.Medicals,
                    _editPetVM.Spiecies,
                    _editPetVM.Race,
                    _editPetVM.MicrochipNumber
                    );
                _vetSystem.EditPet(editedPet);
            }
        }
    }
    public struct PetsCommands
    {
        public class DeletePet : CommandBase
        {
            private PetsViewModel _petsViewModel;
            public DeletePet(PetsViewModel petsViewModel)
            {
                _petsViewModel = petsViewModel;
            }
            public override void Execute(object? parameter)
            {
                bool result = false;
                PetViewModel petVM = _petsViewModel.SelectedPet;
                if (petVM != null)
                {
                    result = _petsViewModel.DeletePet(petVM);
                }
                string text = (result == true) ? "deleted succesfully!" : "not deleted!";
                MessageBox.Show($"Pet {text}", "PawPatientManager", MessageBoxButton.OK);
            }
        }
        public class EditPet : CommandBase
        {
            private PetsViewModel _petsVM;
            private LayoutNavigationServiceParam<PetViewModel, EditPetViewModel> _navService;
            public EditPet(PetsViewModel petsVM, LayoutNavigationServiceParam<PetViewModel, EditPetViewModel> navService)
            {
                _petsVM = petsVM;
                _navService = navService;
            }
            public override void Execute(object? parameter)
            {
                PetViewModel petVM = _petsVM.SelectedPet;
                if (petVM is null || petVM.IsNull() == true)
                {
                    MessageBox.Show("Invalid pet selected", "PawPatientManager", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else _navService.Navigate(petVM);
            }
        }
        public class UpdateSelectedOwner : CommandBase
        {
            private RegisterPetViewModel _registerPetVM;
            private readonly Action<object> _execute;
            private readonly Func<object, bool> _canExecute;

            public UpdateSelectedOwner(RegisterPetViewModel registerPetVM)
            {
                _registerPetVM = registerPetVM;
            }

            public override void Execute(object? parameter)
            {
                if (parameter is OwnerViewModel selectedItem)
                {
                    _registerPetVM.SelectedOwner = selectedItem;
                }
                else
                {
                    _registerPetVM.SelectedOwner = null;
                }
            }
        }

        public class UpdateSelected : CommandBase
        {
            private PetsViewModel _petsViewModel;
            private readonly Action<object> _execute;
            private readonly Func<object, bool> _canExecute;

            public UpdateSelected(PetsViewModel petsViewModel)
            {
                _petsViewModel = petsViewModel;
            }

            public bool CanExecute(object parameter)
            {
                return _canExecute == null || _canExecute(parameter);
            }

            public override void Execute(object? parameter)
            {
                if (parameter is PetViewModel selectedItem)
                {
                    _petsViewModel.SelectedPet = selectedItem;
                }
                else
                {
                    _petsViewModel.SelectedPet = null;
                }
            }
        }
    }
}
