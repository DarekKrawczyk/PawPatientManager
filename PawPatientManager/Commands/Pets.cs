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
using static PawPatientManager.ViewModels.RegisterVisitViewModel;
using PawPatientManager.Utility;

namespace PawPatientManager.Commands
{
    public struct EditVisitCommands
    {
        public class EditVisit : CommandBase
        {
            private VetSystem _vetSystem;
            private EditVisitViewModel _vm;
            public EditVisit(VetSystem vetSystem, EditVisitViewModel vm)
            {
                _vetSystem = vetSystem;
                _vm = vm;
            }

            public override void Execute(object? parameter)
            {
                DateTime date = Globals.GetVisitDateTime(_vm.SelectedDate, _vm.SelectedHour);
                Visit editedVisit = new Visit(
                    _vm.ID,
                    _vm.SelectedPet.Pet,
                    _vm.SelectedVet.Vet,
                    date,
                    null
                    );
                _vetSystem.EditVisit(editedVisit);
            }
        }
    }
    public struct RegisterVisitCommands 
    {
        public class RegisterVisit : CommandBase
        {
            private static uint _idIterator;
            private VetSystem _vetSystem;
            private RegisterVisitViewModel _vm;
            public RegisterVisit(VetSystem vetSystem, RegisterVisitViewModel vm)
            {
                _vetSystem = vetSystem;
                _vm = vm;
            }

            public override void Execute(object? parameter)
            {
                DateTime date = Globals.GetVisitDateTime(_vm.SelectedDate, _vm.SelectedHour);
                Visit newVisit = new Visit(0, _vm.SelectedPet.Pet, _vm.SelectedVet.Vet, date, null);
                _vetSystem.AddVisit(newVisit);

                // TODO: if sucess -> clear data and messagebox; else messagebox?
            }
        }
        public class UpdatePet : CommandBase
        {
            private RegisterVisitViewModel _vm;
            private readonly Action<object> _execute;
            private readonly Func<object, bool> _canExecute;

            public UpdatePet(RegisterVisitViewModel vm)
            {
                _vm = vm;
            }

            public override void Execute(object? parameter)
            {
                if (parameter is PetViewModel selectedItem)
                {
                    _vm.SelectedPet = selectedItem;
                }
                else
                {
                    _vm.SelectedPet = null;
                }
            }
        }

        public class UpdateVet : CommandBase
        {
            private RegisterVisitViewModel _vm;
            private readonly Action<object> _execute;
            private readonly Func<object, bool> _canExecute;

            public UpdateVet(RegisterVisitViewModel vm)
            {
                _vm = vm;
            }

            public override void Execute(object? parameter)
            {
                if (parameter is VetViewModel selectedItem)
                {
                    _vm.SelectedVet = selectedItem;
                }
                else
                {
                    _vm.SelectedVet = null;
                }
            }
        }
        public class UpdateHour : CommandBase
        {
            private RegisterVisitViewModel _vm;
            private readonly Action<object> _execute;
            private readonly Func<object, bool> _canExecute;

            public UpdateHour(RegisterVisitViewModel vm)
            {
                _vm = vm;
            }

            public override void Execute(object? parameter)
            {
                if (parameter is HourViewModel selectedItem)
                {
                    _vm.SelectedHour = selectedItem;
                }
                else
                {
                    _vm.SelectedHour = null;
                }
            }
        }        
        public class UpdateDate : CommandBase
        {
            private RegisterVisitViewModel _vm;
            private readonly Action<object> _execute;
            private readonly Func<object, bool> _canExecute;

            public UpdateDate(RegisterVisitViewModel vm)
            {
                _vm = vm;
            }

            public override void Execute(object? parameter)
            {
                if (parameter is DateTime selectedItem)
                {
                    _vm.SelectedDate = selectedItem;
                }
                else
                {
                    _vm.SelectedDate = DateTime.Now;
                }
            }
        }
    }
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
