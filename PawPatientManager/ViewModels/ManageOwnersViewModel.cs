using PawPatientManager.Commands;
using PawPatientManager.Models;
using PawPatientManager.Services;
using PawPatientManager.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;
using static PawPatientManager.Commands.ManageOwnersViewModelCommands;

namespace PawPatientManager.ViewModels
{
    public class ManageOwnersViewModel : ViewModelBase
    {
        /*  To display list of owners, list type *ObservableCollection* will be used because it can
         *  provide notifications whenever list is updated - this means whenever we add item or remove it
         *  from list, UI is going to be automatically updated.
         */
        #region 
        private ObservableCollection<OwnerViewModel> _owners;
        private NavigationStore _navigationStore;
        private INavigationService<OwnerRegistrationViewModel> _navOwnerRegisterService;
        private LayoutNavigationServiceParam<OwnerViewModel, EditOwnerViewModel> _navEditOwnerService;
        private OwnerViewModel _selectedOwnerViewModel;
        private VetSystem _vetSystem;
        #endregion
        #region Properties
        /*  Just a Property to get all of the owners, in this case notification is not required, 
         *  because it will be mostly used to iterate through and check smth. Plus it provides interface.
         */
        public IEnumerable<OwnerViewModel> Owners {  get { return _owners; } set { OnPropertyChanged(nameof(Owners)); } }
        public OwnerViewModel SelectedOwner { get { return _selectedOwnerViewModel; } set { _selectedOwnerViewModel = value; } }    
        #endregion
        #region Commands
        public ICommand CommandAddOwner { get; }
        public ICommand CommandDeleteOwner { get; }
        public ICommand CommandEditOwner { get; }
        public ICommand AddSelectLocomotifCommand { get; }
        #endregion
        public ManageOwnersViewModel(VetSystem vetSystem, INavigationService<OwnerRegistrationViewModel> navOwnerRegisterService,
            LayoutNavigationServiceParam<OwnerViewModel, EditOwnerViewModel> navEditOwnerService) 
        {
            _selectedOwnerViewModel = new OwnerViewModel(null);
            _vetSystem = vetSystem;
            _navEditOwnerService = navEditOwnerService;
            _navOwnerRegisterService = navOwnerRegisterService;

            _owners = new ObservableCollection<OwnerViewModel>();
            ReloadOwners();
            // TODO: Commands!!
            //OwnerViewModel os = OwnersList
            AddSelectLocomotifCommand = new Commands.ManageOwnersViewModelCommands.UpdateSelected(this);
            //AddSelectLocomotifCommand = new RelayCommand(AddSelectLocomotif, CanAddSelectLocomotif);
            CommandAddOwner = new NavigateCommand<OwnerRegistrationViewModel>(_navOwnerRegisterService);
            CommandDeleteOwner = new Commands.ManageOwnersViewModelCommands.DeleteOwner(this);
            CommandEditOwner = new Commands.ManageOwnersViewModelCommands.EditOwner(this, _navEditOwnerService);
            //CommandEditOwner = new NavigateCommandParam<OwnerViewModel, EditOwnerViewModel>(_navEditOwnerService, SelectedOwnerViewModel);
            //CommandAddOwner = new NavigateCommand<OwnerRegistrationViewModel>(new NavigationService<OwnerRegistrationViewModel>(_navigationStore, ()=> new OwnerRegistrationViewModel(_vetSystem, _navigationStore)));
        }

        public bool DeleteOwner(OwnerViewModel ownerVM)
        {
            bool result = false;
            if (ownerVM == null)
            {
                result = _vetSystem.Owners.Remove(_selectedOwnerViewModel.Owner);
            }
            else result = _vetSystem.Owners.Remove(ownerVM.Owner);
            ReloadOwners();
            OnPropertyChanged(nameof(Owners));
            return result;
        }

        private void ReloadOwners()
        {
            _owners.Clear();
            foreach (Owner owner in _vetSystem.Owners)
            {
                _owners.Add(new OwnerViewModel(owner));
            }
        }

        private void AddSelectLocomotif(object parameter)
        {
            if (parameter is OwnerViewModel selectedItem)
            {
                _selectedOwnerViewModel = selectedItem;
            }
        }

        private bool CanAddSelectLocomotif(object parameter)
        {
            // Optionally implement a condition to enable or disable the command
            return true;
        }
    }
}
