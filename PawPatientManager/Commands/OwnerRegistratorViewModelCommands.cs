using PawPatientManager.Models;
using PawPatientManager.Services;
using PawPatientManager.Stores;
using PawPatientManager.Utility;
using PawPatientManager.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PawPatientManager.Commands
{
    public struct MedsCommands
    {
        public class LoadMeds : AsyncCommandBase
        {
            private VetSystem _system;
            private MedsViewModel _viewModel;
            public LoadMeds(VetSystem system, MedsViewModel viewModel)
            {
                _system = system;
                _viewModel = viewModel;
            }

            public override async Task ExecuteAsync(object parameter)
            {
                try
                {
                    _viewModel.IsLoading = true;
                    IEnumerable<Medication> meds = await _system.GetAllMedicationsAsync();
                    _viewModel.ReloadMeds(meds);
                }catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "LoadMeds class");
                } finally
                {
                    _viewModel.IsLoading = false;
                }
            }
        }
        public class AddMed : AsyncCommandBase
        {
            private VetSystem _vetSystem;
            private MedsViewModel _medVM;
            public AddMed(VetSystem vetSystem, MedsViewModel medVM)
            {
                _vetSystem = vetSystem;
                _medVM = medVM;
                _medVM.PropertyChanged += _medEditVM_PropertyChanged;
            }
            private void _medEditVM_PropertyChanged(object? sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == nameof(MedsViewModel.AddAmount) || e.PropertyName == nameof(MedsViewModel.AddDescription) || e.PropertyName == nameof(MedsViewModel.AddName))
                {
                    OnCanExecutedChange();
                }
            }
            public override bool CanExecute(object? parameter)
            {
                return !string.IsNullOrEmpty(_medVM.AddDescription) && !string.IsNullOrEmpty(_medVM.AddName) && base.CanExecute(parameter);
            }
            public override async Task ExecuteAsync(object parameter)
            {
                Medication newMed = new Medication(new Guid(),
                    _medVM.AddName,
                    _medVM.AddDescription,
                    _medVM.AddAmount
                    );
                try
                {
                    // Firstly add
                    await _vetSystem.AddMedication(newMed);

                    // Then refresh
                    IEnumerable<Medication> meds = await _vetSystem.GetAllMedicationsAsync();
                    _medVM.ReloadMeds(meds);
                    _medVM.AddName = string.Empty;
                    _medVM.AddDescription = string.Empty;
                    _medVM.AddAmount = 0;
                    MessageBox.Show($"Medication {newMed.Name} added successfully!", "Paw Patient Manager", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "AddMed class");
                }
            }
        }
        public class SelectedMedChanged : CommandBase
        {
            private MedsViewModel _medsVM;
            private readonly Action<object> _execute;
            private readonly Func<object, bool> _canExecute;

            public SelectedMedChanged(MedsViewModel medsVM)
            {
                _medsVM = medsVM;
            }

            public override void Execute(object? parameter)
            {
                if (parameter is MedViewModel selectedItem)
                {
                    _medsVM.SelectedMed = selectedItem;
                }
                else
                {
                    _medsVM.SelectedMed = null;
                }
            }
        }
        public class EditMed : AsyncCommandBase
        {
            private VetSystem _system;
            private MedsViewModel _medVM;
            public EditMed(VetSystem vetSystem, MedsViewModel medVM)
            {
                _system = vetSystem;
                _medVM = medVM;
                _medVM.PropertyChanged += _medEditVM_PropertyChanged;
            }
            private void _medEditVM_PropertyChanged(object? sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == nameof(MedsViewModel.EditAmount) || e.PropertyName == nameof(MedsViewModel.EditDescription) || e.PropertyName == nameof(MedsViewModel.EditName))
                {
                    OnCanExecutedChange();
                }
            }
            public override bool CanExecute(object? parameter)
            {
                return (_medVM.EditAmount > -1) && !string.IsNullOrEmpty(_medVM.EditDescription) && !string.IsNullOrEmpty(_medVM.EditName) && base.CanExecute(parameter);
            }

            public override async Task ExecuteAsync(object parameter)
            {
                Medication selectedMed = new Medication(_medVM.SelectedMed.ID, _medVM.SelectedMed.Name, _medVM.SelectedMed.Description, _medVM.SelectedMed.Amount);
                Medication editedMed = new Medication(_medVM.SelectedMed.ID, _medVM.EditName, _medVM.EditDescription, _medVM.EditAmount);
                try
                {
                    // First delete
                    await _system.EditMedication(selectedMed, editedMed);

                    // Then refresh data
                    IEnumerable<Medication> meds = await _system.GetAllMedicationsAsync();
                    _medVM.ReloadMeds(meds);

                    MessageBox.Show($"Medication: {editedMed.Name} edited","PawPatientManager",MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "EditMed class");
                }
            }
        }
        public class DeleteMed : AsyncCommandBase
        {
            private VetSystem _system;
            private MedsViewModel _medVM;
            public DeleteMed(VetSystem vetSystem, MedsViewModel medVM)
            {
                _system = vetSystem;
                _medVM = medVM;
                _medVM.PropertyChanged += _medVM_PropertyChanged;
            }

            private void _medVM_PropertyChanged(object? sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == nameof(MedsViewModel.SelectedMed))
                {
                    OnCanExecutedChange();
                }
            }

            public override bool CanExecute(object? parameter)
            {
                return (_medVM.SelectedMed != null) && (!_medVM.SelectedMed.IsNull()) && base.CanExecute(parameter);
            }
            public override async Task ExecuteAsync(object parameter)
            {
                try
                {
                    Medication editedMed = new Medication(_medVM.SelectedMed.ID, _medVM.SelectedMed.Name, _medVM.SelectedMed.Description, _medVM.SelectedMed.Amount);
                    // First delete
                    await _system.DeleteMedication(editedMed);

                    // Then refresh data
                    IEnumerable<Medication> meds = await _system.GetAllMedicationsAsync();
                    _medVM.ReloadMeds(meds);

                    MessageBox.Show("Medication deleted", "PawPatientManager", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "DeleteMed class");
                }
            }
        }
    }
    public struct VisitsCommandsCombobox
    {
        public class DeleteVisit : AsyncCommandBase
        {
            private VisitsViewModel _visitsVM;
            private VetSystem _vetSystem;
            public DeleteVisit(VetSystem vetSystem, VisitsViewModel visitsVM)
            {
                _visitsVM = visitsVM;
                _vetSystem = vetSystem;
                _visitsVM.PropertyChanged += _visitsVM_PropertyChanged1;
            }

            private void _visitsVM_PropertyChanged1(object? sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == nameof(VisitsViewModel.SelectedVisit))
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
                    // First delete
                    await _vetSystem.DeleteVisit(editedMed);

                    // Then refresh data
                    IEnumerable<Visit> meds = await _vetSystem.GetAllVisitsAsync();
                    _visitsVM.ReloadVisits(meds);
                    MessageBox.Show($"Visit with {editedMed.Pet.Name} and {editedMed.Vet.Name} vet, has been deleted!", "PawPatientManager",MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "DeleteMed class");
                }
            }
        }

        public class UpdateSelected : CommandBase
        {
            private VisitsViewModel _petsViewModel;
            private readonly Action<object> _execute;
            private readonly Func<object, bool> _canExecute;

            public UpdateSelected(VisitsViewModel petsViewModel)
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
            private VisitsViewModel _viewModel;
            public LoadVisits(VetSystem system, VisitsViewModel viewModel)
            {
                _system = system;
                _viewModel = viewModel;
            }

            public override async Task ExecuteAsync(object parameter)
            {
                try
                {
                    IEnumerable<Visit> visits = await _system.GetAllVisitsAsync();
                    _viewModel.ReloadVisits(visits);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "LoadVisits class");
                }
            }
        }

        public class EditVisit : CommandBase
        {
            private VisitsViewModel _visitsVM;
            private LayoutNavigationServiceParam<VisitViewModel, EditVisitViewModel> _navService;
            public EditVisit(VisitsViewModel visitsVM, LayoutNavigationServiceParam<VisitViewModel, EditVisitViewModel> navService)
            {
                _visitsVM = visitsVM;
                _navService = navService;
                _visitsVM.PropertyChanged += _visitsVM_PropertyChanged;
            }

            private void _visitsVM_PropertyChanged(object? sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == nameof(VisitsViewModel.SelectedVisit))
                {
                    OnCanExecutedChange();
                }
            }

            public override bool CanExecute(object? parameter)
            {
                return (_visitsVM.SelectedVisit != null) && (!_visitsVM.SelectedVisit.IsNull()) && base.CanExecute(parameter);
            }
            public override void Execute(object? parameter)
            {
                VisitViewModel visitVM = _visitsVM.SelectedVisit;
                if (visitVM is null || visitVM.IsNull() == true)
                {
                    MessageBox.Show("Invalid visit selected", "PawPatientManager", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else _navService.Navigate(visitVM);
            }
        }
    }
    public struct OwnerRegistratorViewModelCommands
    {
        // TODO: some kind of magic for initializing all of the commands within this structure.

        public class ClearData : CommandBase
        {
            private OwnerRegistrationViewModel _ownerRegistrationModel;
            public ClearData(OwnerRegistrationViewModel ownerRegistrationModel)
            {
                _ownerRegistrationModel = ownerRegistrationModel;
            }
            public override void Execute(object? parameter)
            {
                _ownerRegistrationModel.Name = string.Empty;
                _ownerRegistrationModel.Surname = string.Empty;
                _ownerRegistrationModel.Gender = false;
                _ownerRegistrationModel.BirthDate = DateTime.Now;
                _ownerRegistrationModel.Adress = string.Empty;
                _ownerRegistrationModel.PhoneNumber = string.Empty;
                _ownerRegistrationModel.Email = string.Empty;
                _ownerRegistrationModel.PESEL = string.Empty;
            }
        }
        public class EditOwner : AsyncCommandBase
        {
            private VetSystem _vetSystem;
            private EditOwnerViewModel _ownerRegistrationViewModel;
            public EditOwner(VetSystem vetSystem, EditOwnerViewModel ownerRegistrationViewModel)
            {
                _vetSystem = vetSystem;
                _ownerRegistrationViewModel = ownerRegistrationViewModel;
                _ownerRegistrationViewModel.PropertyChanged += _ownerRegistrationViewModel_PropertyChanged;
            }

            private void _ownerRegistrationViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == nameof(EditOwnerViewModel.Name) || e.PropertyName == nameof(EditOwnerViewModel.Surname) ||
                    e.PropertyName == nameof(EditOwnerViewModel.Adress) || e.PropertyName == nameof(EditOwnerViewModel.BirthDate) ||
                    e.PropertyName == nameof(EditOwnerViewModel.PhoneNumber) || e.PropertyName == nameof(EditOwnerViewModel.Email) ||
                    e.PropertyName == nameof(EditOwnerViewModel.PESEL))
                {
                    OnCanExecutedChange();
                }
            }

            public override bool CanExecute(object? parameter)
            {
                bool nameValid = Globals.IsNameValid(_ownerRegistrationViewModel.Name);
                bool surnameValid = Globals.IsSurnameValid(_ownerRegistrationViewModel.Surname);
                bool addressValid = _ownerRegistrationViewModel.Adress != string.Empty;
                bool phoneNumberValid = Globals.IsPhoneNumberValid(_ownerRegistrationViewModel.PhoneNumber);
                bool emailValid = Globals.IsEmailValid(_ownerRegistrationViewModel.Email);
                bool peselValid = Globals.IsPeselValid(_ownerRegistrationViewModel.PESEL);
                bool areFieldsValid = false;
                if (nameValid && surnameValid && addressValid && phoneNumberValid && emailValid && peselValid)
                {
                    areFieldsValid = true;
                }
                return areFieldsValid && !string.IsNullOrEmpty(_ownerRegistrationViewModel.Name) && base.CanExecute(parameter);
            }

            //public override void Execute(object? parameter)
            //{
            //    Owner selectedOwner = new Owner(_ownerRegistrationViewModel.OriginalOwner);

            //    Owner newOwner = new Owner(_ownerRegistrationViewModel.OriginalOwner.ID,
            //        _ownerRegistrationViewModel.Name,
            //        _ownerRegistrationViewModel.Surname,
            //        _ownerRegistrationViewModel.Gender,
            //        _ownerRegistrationViewModel.BirthDate,
            //        _ownerRegistrationViewModel.OriginalOwner.Pets,
            //        _ownerRegistrationViewModel.Adress,
            //        _ownerRegistrationViewModel.PhoneNumber,
            //        _ownerRegistrationViewModel.Email,
            //        _ownerRegistrationViewModel.PESEL
            //        );
            //    _vetSystem.EditOwner(selectedOwner, newOwner);
            //}

            public override async Task ExecuteAsync(object parameter)
            {
                try
                {
                    Owner selectedOwner = new Owner(_ownerRegistrationViewModel.OriginalOwner);

                    Owner newOwner = new Owner(_ownerRegistrationViewModel.OriginalOwner.ID,
                        _ownerRegistrationViewModel.Name,
                        _ownerRegistrationViewModel.Surname,
                        _ownerRegistrationViewModel.Gender,
                        _ownerRegistrationViewModel.BirthDate,
                        _ownerRegistrationViewModel.OriginalOwner.Pets,
                        _ownerRegistrationViewModel.Adress,
                        _ownerRegistrationViewModel.PhoneNumber,
                        _ownerRegistrationViewModel.Email,
                        _ownerRegistrationViewModel.PESEL
                        );
                    await _vetSystem.EditOwner(selectedOwner, newOwner);
                    _ownerRegistrationViewModel.OriginalOwner = new OwnerViewModel(newOwner);
                    MessageBox.Show($"Owner {newOwner.Name} {newOwner.Surname} has been edited!","Paw Patient Manager", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Something went wrong while editing owner!", "Paw Patient Manager", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        public class RegisterOwner : AsyncCommandBase
        {
            private static uint _idIterator;
            private VetSystem _vetSystem;
            private OwnerRegistrationViewModel _ownerRegistrationViewModel;
            public RegisterOwner(VetSystem vetSystem, OwnerRegistrationViewModel ownerRegistrationViewModel)
            {
                /*  We need reference to VetSystem because any operation requires access methods/fields
                 *  of out system to change/modify it. *ViewModelBase* is required because the data from
                 *  which for example *Owner* will be created is located there.
                 */
                _vetSystem = vetSystem;
                _ownerRegistrationViewModel = ownerRegistrationViewModel;
                _ownerRegistrationViewModel.PropertyChanged += _ownerRegistrationViewModel_PropertyChanged;
            }

            private void _ownerRegistrationViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
            {
                /*  Walk through - in line *_ownerRegistrationViewModel.PropertyChanged += _ownerRegistrationViewModel_PropertyChanged;*
                 *  we are subscribing to event *PropertyChanged* in *OwnerRegistrationViewModel* (BaseClass) wchich is called whenever
                 *  any property in UI has changed. So whenever it changes this method is launched. Next we check if *PropertyName*
                 *  that has changed is called like *OwnerRegistrationViewModel.Name* because we are trying to determine, did
                 *  *Name* changed. (We dont want to call this function on every property change...). If it is this property, then 
                 *  *OnCanExecutedChange()* function is called which causes *CanExecute()* event ti fire?? not sure.
                 */
                if (e.PropertyName == nameof(OwnerRegistrationViewModel.Name) || e.PropertyName == nameof(OwnerRegistrationViewModel.Surname) ||
                    e.PropertyName == nameof(OwnerRegistrationViewModel.Adress) || e.PropertyName == nameof(OwnerRegistrationViewModel.BirthDate) ||
                    e.PropertyName == nameof(OwnerRegistrationViewModel.PhoneNumber) || e.PropertyName == nameof(OwnerRegistrationViewModel.Email) ||
                    e.PropertyName == nameof(OwnerRegistrationViewModel.PESEL))
                {
                    OnCanExecutedChange();
                }
            }

            public override bool CanExecute(object? parameter)
            {
                bool nameValid = Globals.IsNameValid(_ownerRegistrationViewModel.Name);
                bool surnameValid = Globals.IsSurnameValid(_ownerRegistrationViewModel.Surname);
                bool addressValid = _ownerRegistrationViewModel.Adress != string.Empty;
                bool phoneNumberValid = Globals.IsPhoneNumberValid(_ownerRegistrationViewModel.PhoneNumber);
                bool emailValid = Globals.IsEmailValid(_ownerRegistrationViewModel.Email);
                bool peselValid = Globals.IsPeselValid(_ownerRegistrationViewModel.PESEL);
                bool areFieldsValid = false;
                if(nameValid && surnameValid && addressValid && phoneNumberValid && emailValid && peselValid)
                {
                    areFieldsValid = true;
                }
                return areFieldsValid && !string.IsNullOrEmpty(_ownerRegistrationViewModel.Name) && base.CanExecute(parameter);
            }

            public override async Task ExecuteAsync(object parameter)
            {
                try
                {
                    Owner newOwner = new Owner(_ownerRegistrationViewModel.ID,
                        _ownerRegistrationViewModel.Name,
                        _ownerRegistrationViewModel.Surname,
                        _ownerRegistrationViewModel.Gender,
                        _ownerRegistrationViewModel.BirthDate,
                        null,
                        _ownerRegistrationViewModel.Adress,
                        _ownerRegistrationViewModel.PhoneNumber,
                        _ownerRegistrationViewModel.Email,
                        _ownerRegistrationViewModel.PESEL
                        );
                    await _vetSystem.AddOwner(newOwner);
                    MessageBox.Show($"Owner {newOwner.Name} {newOwner.Surname} has been added!!", "PawPatientManager", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "RegisterOwner class");
                }
            }
        }
    }
}
