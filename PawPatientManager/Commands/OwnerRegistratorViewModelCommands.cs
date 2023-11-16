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

namespace PawPatientManager.Commands
{
    public struct MedsCommands
    {
        public class AddMed : CommandBase
        {
            private VetSystem _vetSystem;
            private MedsViewModel _medVM;
            public AddMed(VetSystem vetSystem, MedsViewModel medVM)
            {
                _vetSystem = vetSystem;
                _medVM = medVM;
            }

            public override void Execute(object? parameter)
            {
                Medication newMed = new Medication((uint)_vetSystem.Meds.Count,
                    _medVM.AddName,
                    _medVM.AddDescription,
                    _medVM.AddAmount
                    );
                _vetSystem.AddMed(newMed);
                _medVM.ReloadMeds();

                // TODO: if sucess -> clear data and messagebox; else messagebox?
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
        public class EditMed : CommandBase
        {
            private VetSystem _system;
            private MedsViewModel _medVM;
            public EditMed(VetSystem vetSystem, MedsViewModel medVM)
            {
                _system = vetSystem;
                _medVM = medVM;
            }
            public override void Execute(object? parameter)
            {
                Medication editedMed = new Medication(_medVM.SelectedMed.ID,
                    _medVM.EditName,
                    _medVM.EditDescription,
                    _medVM.EditAmount
                    );
                _system.EditMed(editedMed);
                _medVM.ReloadMeds();
            }
        }
        public class DeleteMed : CommandBase
        {
            private VetSystem _system;
            private MedsViewModel _medVM;
            public DeleteMed(VetSystem vetSystem, MedsViewModel medVM)
            {
                _system = vetSystem;
                _medVM = medVM;
            }
            public override void Execute(object? parameter)
            {
                Medication editedMed = new Medication(_medVM.SelectedMed.ID,
                    _medVM.SelectedMed.Name,
                    _medVM.SelectedMed.Description,
                    _medVM.SelectedMed.Amount
                    );
                _system.DeleteMed(editedMed);
                _medVM.ReloadMeds();
            }
        }
    }
    public struct VisitsCommandsCombobox
    {
        public class DeleteVisit : CommandBase
        {
            private VisitsViewModel _visitsVM;
            public DeleteVisit(VisitsViewModel visitsVM)
            {
                _visitsVM = visitsVM;
            }
            public override void Execute(object? parameter)
            {
                bool result = false;
                VisitViewModel visitVM = _visitsVM.SelectedVisit;
                if (visitVM != null)
                {
                    result = _visitsVM.DeleteVisit(visitVM);
                }
                string text = (result == true) ? "deleted succesfully!" : "not deleted!";
                MessageBox.Show($"Visit {text}", "PawPatientManager", MessageBoxButton.OK);
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

        public class EditVisit : CommandBase
        {
            private VisitsViewModel _visitsVM;
            private LayoutNavigationServiceParam<VisitViewModel, EditVisitViewModel> _navService;
            public EditVisit(VisitsViewModel visitsVM, LayoutNavigationServiceParam<VisitViewModel, EditVisitViewModel> navService)
            {
                _visitsVM = visitsVM;
                _navService = navService;
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
        public class EditOwner : CommandBase
        {
            private VetSystem _vetSystem;
            private EditOwnerViewModel _ownerRegistrationViewModel;
            public EditOwner(VetSystem vetSystem, EditOwnerViewModel ownerRegistrationViewModel)
            {
                _vetSystem = vetSystem;
                _ownerRegistrationViewModel = ownerRegistrationViewModel;
            }
            public override bool CanExecute(object? parameter)
            {
                return !string.IsNullOrEmpty(_ownerRegistrationViewModel.Name) && base.CanExecute(parameter);
            }

            public override void Execute(object? parameter)
            {
                Owner newOwner = new Owner(_ownerRegistrationViewModel.ID,
                    _ownerRegistrationViewModel.Name,
                    _ownerRegistrationViewModel.Surname,
                    _ownerRegistrationViewModel.Gender,
                    _ownerRegistrationViewModel.BirthDate,
                    _ownerRegistrationViewModel.Adress,
                    _ownerRegistrationViewModel.PhoneNumber,
                    _ownerRegistrationViewModel.Email,
                    _ownerRegistrationViewModel.PESEL
                    );
                _vetSystem.EditOwner(newOwner);
            }
        }
        public class RegisterOwner : CommandBase
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
                if (e.PropertyName == nameof(OwnerRegistrationViewModel.Name))
                {
                    OnCanExecutedChange();
                }
            }

            public override bool CanExecute(object? parameter)
            {
                return !string.IsNullOrEmpty(_ownerRegistrationViewModel.Name) && base.CanExecute(parameter);
            }

            public override void Execute(object? parameter)
            {
                Owner newOwner = new Owner(_ownerRegistrationViewModel.ID,
                    _ownerRegistrationViewModel.Name,
                    _ownerRegistrationViewModel.Surname,
                    _ownerRegistrationViewModel.Gender,
                    _ownerRegistrationViewModel.BirthDate,
                    _ownerRegistrationViewModel.Adress,
                    _ownerRegistrationViewModel.PhoneNumber,
                    _ownerRegistrationViewModel.Email,
                    _ownerRegistrationViewModel.PESEL
                    );
                _vetSystem.AddOwner(newOwner);

                // TODO: if sucess -> clear data and messagebox; else messagebox?
            }
        }
        //public class Return : CommandBase
        //{
        //    private NavigationStore _navigator;
        //    //private NavigationBarViewModel _navigationBarVM;
        //    private VetSystem _vetSystem;
        //    public NavigationStore Navigator { get { return _navigator; } }
        //    public Return(NavigationStore navigator, VetSystem vetSystem)
        //    {
        //        _navigator = navigator;
        //        _vetSystem = vetSystem;
        //        //_navigationBarVM = navigationBarVM;
        //    }

        //    public override void Execute(object? parameter)
        //    {
        //        _navigator.CurrentViewModel = new ManageOwnersViewModel(_navigator,_vetSystem);
        //        //_navigator.CurrentViewModel = new ManageOwnersViewModel(_navigator,_vetSystem, _navigationBarVM);
        //    }
        //}
    }
}
