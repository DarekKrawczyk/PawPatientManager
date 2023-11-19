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
        public class LoadVets : AsyncCommandBase
        {
            private VetSystem _system;
            private EditVisitViewModel _viewModel;
            public LoadVets(VetSystem system, EditVisitViewModel viewModel)
            {
                _system = system;
                _viewModel = viewModel;
            }

            public override async Task ExecuteAsync(object parameter)
            {
                try
                {
                    IEnumerable<Vet> vets = await _system.GetAllVetsAsync();
                    _viewModel.ReloadVets(vets);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "LoadMeds class");
                }
            }
        }
        public class LoadPets : AsyncCommandBase
        {
            private VetSystem _system;
            private EditVisitViewModel _viewModel;
            public LoadPets(VetSystem system, EditVisitViewModel viewModel)
            {
                _system = system;
                _viewModel = viewModel;
            }

            public override async Task ExecuteAsync(object parameter)
            {
                try
                {
                    IEnumerable<Pet> pets = await _system.GetAllPetsFromAllOwners();
                    _viewModel.ReloadPets(pets);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "LoadMeds class");
                }
            }
        }
        public class EditVisit : AsyncCommandBase
        {
            private VetSystem _vetSystem;
            private EditVisitViewModel _vm;
            public EditVisit(VetSystem vetSystem, EditVisitViewModel vm)
            {
                _vetSystem = vetSystem;
                _vm = vm;
            }

            //public override void Execute(object? parameter)
            //{
            //    DateTime date = Globals.GetVisitDateTime(_vm.SelectedDate, _vm.SelectedHour);
            //    Visit editedVisit = new Visit(
            //        new Guid(),
            //        _vm.SelectedPet.Pet,
            //        _vm.SelectedVet.Vet,
            //        date,
            //        null
            //        );
            //    _vetSystem.EditVisit(editedVisit);
            //}

            public override async Task ExecuteAsync(object parameter)
            {
                DateTime date = Globals.GetVisitDateTime(_vm.SelectedDate, _vm.SelectedHour);
                Guid newVetID = _vm.SelectedVet.ID; 
                Guid newPetID = _vm.SelectedPet.ID; 
                Visit editedVisit = new Visit(
                    new Guid(),
                    _vm.SelectedPet.Pet,
                    _vm.SelectedVet.Vet,
                    date,
                    null
                    );
                _vetSystem.EditVisit(_vm.SelectedVisit.Visit, editedVisit, newVetID, newPetID);
            }
        }
    }
    public struct RegisterVisitCommands 
    {
        public class LoadVets : AsyncCommandBase
        {
            private VetSystem _system;
            private RegisterVisitViewModel _viewModel;
            public LoadVets(VetSystem system, RegisterVisitViewModel viewModel)
            {
                _system = system;
                _viewModel = viewModel;
            }

            public override async Task ExecuteAsync(object parameter)
            {
                try
                {
                    IEnumerable<Vet> vets = await _system.GetAllVetsAsync();
                    _viewModel.ReloadVets(vets);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "LoadMeds class");
                }
            }
        }
        public class RegisterVisit : AsyncCommandBase
        {
            private static uint _idIterator;
            private VetSystem _vetSystem;
            private RegisterVisitViewModel _vm;
            public RegisterVisit(VetSystem vetSystem, RegisterVisitViewModel vm)
            {
                _vetSystem = vetSystem;
                _vm = vm;
            }

            public override async Task ExecuteAsync(object parameter)
            {
                try
                {
                    DateTime date = Globals.GetVisitDateTime(_vm.SelectedDate, _vm.SelectedHour);
                    await _vetSystem.AddVisit(_vm.SelectedVet.Vet, _vm.SelectedPet.Pet, date);
                } catch(Exception ex)
                {
                    MessageBox.Show(ex.Message,"RegisterVisitsCommands.RegisterVisit");
                }
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
        public class LoadPetsForVisits : AsyncCommandBase
        {
            private VetSystem _system;
            private RegisterVisitViewModel _viewModel;
            public LoadPetsForVisits(VetSystem system, RegisterVisitViewModel viewModel)
            {
                _system = system;
                _viewModel = viewModel;
            }

            public override async Task ExecuteAsync(object parameter)
            {
                try
                {
                    IEnumerable<Pet> pets = await _system.GetAllPetsFromAllOwners();
                    _viewModel.ReloadPets(pets);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "LoadMeds class");
                }
            }
        }
        public class LoadPets : AsyncCommandBase
        {
            private VetSystem _system;
            private PetsViewModel _viewModel;
            public LoadPets(VetSystem system, PetsViewModel viewModel)
            {
                _system = system;
                _viewModel = viewModel;
            }

            public override async Task ExecuteAsync(object parameter)
            {
                try
                {
                    IEnumerable<Pet> pets = await _system.GetAllPetsFromAllOwners();
                    _viewModel.ReloadPets(pets);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "LoadMeds class");
                }
            }
        }

        public class LoadOwners : AsyncCommandBase
        {
            private VetSystem _system;
            private RegisterPetViewModel _viewModel;
            public LoadOwners(VetSystem system, RegisterPetViewModel viewModel)
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
                    MessageBox.Show(ex.Message, "EditPetCommand.LoadOwners class");
                }
            }
        }
        public class RegisterPet : AsyncCommandBase
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

            //public override void Execute(object? parameter)
            //{
            //    if(_editPetVM == null || _editPetVM.Owners == null)
            //    {
            //        MessageBox.Show("Please select owner of the pet!", "PawPatientManager", MessageBoxButton.OK);
            //    }
            //    else
            //    {
            //        Pet newPet = new Pet(
            //            _editPetVM.ID,
            //            _editPetVM.Name,
            //            _editPetVM.Gender,
            //            new Owner(_editPetVM.SelectedOwner),
            //            _editPetVM.BirthDate,
            //            null,
            //            null,
            //            _editPetVM.Spieces,
            //            _editPetVM.Race,
            //            _editPetVM.MicrochipNumber
            //            );
            //        _vetSystem.AddPetToOwner(newPet);
            //    }
            //}

            public override async Task ExecuteAsync(object parameter)
            {
                if (_editPetVM == null || _editPetVM.Owners == null)
                {
                    MessageBox.Show("Please select owner of the pet!", "PawPatientManager", MessageBoxButton.OK);
                }
                else
                {
                    Owner owner = new Owner(_editPetVM.SelectedOwner);
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
                    await _vetSystem.AssignPetToOwner(owner, newPet);
                }
            }
        }
        public class EditPet : AsyncCommandBase
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

            public override async Task ExecuteAsync(object parameter)
            {
                try
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

                    await _vetSystem.EditPet(editedPet);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "EditPetCommand.EditPet class");
                }
            }
        }
    }
    public struct PetsCommands
    {
        public class DeletePet : AsyncCommandBase
        {
            private PetsViewModel _petsViewModel;
            private VetSystem _vetSystem;
            public DeletePet(VetSystem vetSystem, PetsViewModel petsViewModel)
            {
                _petsViewModel = petsViewModel;
                _vetSystem = vetSystem;
                _petsViewModel.PropertyChanged += _petsViewModel_PropertyChanged;
            }

            private void _petsViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == nameof(PetsViewModel.SelectedPet))
                {
                    OnCanExecutedChange();
                }
            }

            public override bool CanExecute(object? parameter)
            {
                return (_petsViewModel.SelectedPet != null) && (_petsViewModel.SelectedPet.IsNull() == false) && base.CanExecute(parameter);
            }
            public override async Task ExecuteAsync(object parameter)
            {
                try
                {
                    Pet petToDelete = new Pet(_petsViewModel.SelectedPet);
                    Owner petsOwner = _petsViewModel.SelectedPet.Owner;

                    await _vetSystem.DeletePet(petsOwner, petToDelete);

                    IEnumerable<Pet> pets = await _vetSystem.GetAllPetsFromAllOwners();
                    _petsViewModel.ReloadPets(pets);
                    MessageBox.Show($"Pet: {petToDelete.Name}, of {petsOwner.Name} {petsOwner.Surname} owner, has been deleted from database!", "Paw Patient Manager", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"There was an error while deleting pet: {_petsViewModel.SelectedPet.Name}","Paw Patient Manager", MessageBoxButton.OK, MessageBoxImage.Error);
                }
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
                _petsVM.PropertyChanged += _petsVM_PropertyChanged;
            }

            private void _petsVM_PropertyChanged(object? sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == nameof(PetsViewModel.SelectedPet))
                {
                    OnCanExecutedChange();
                }
            }

            public override bool CanExecute(object? parameter)
            {
                return (_petsVM.SelectedPet != null) && (_petsVM.SelectedPet.IsNull() == false) && base.CanExecute(parameter);
            }
            public override void Execute(object? parameter)
            {
                PetViewModel petVM = _petsVM.SelectedPet;
                 _navService.Navigate(petVM);
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
