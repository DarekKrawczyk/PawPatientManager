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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
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
        public ICommand CommandLoadOwners { get; }
        #endregion
        public ManageOwnersViewModel(VetSystem vetSystem, INavigationService<OwnerRegistrationViewModel> navOwnerRegisterService,
            LayoutNavigationServiceParam<OwnerViewModel, EditOwnerViewModel> navEditOwnerService) 
        {
            _selectedOwnerViewModel = new OwnerViewModel(null);
            _vetSystem = vetSystem;
            _navEditOwnerService = navEditOwnerService;
            _navOwnerRegisterService = navOwnerRegisterService;

            _owners = new ObservableCollection<OwnerViewModel>();

            AddSelectLocomotifCommand = new Commands.ManageOwnersViewModelCommands.UpdateSelected(this);
            CommandLoadOwners = new Commands.ManageOwnersViewModelCommands.LoadOwners(_vetSystem, this);
            CommandAddOwner = new NavigateCommand<OwnerRegistrationViewModel>(_navOwnerRegisterService);
            CommandDeleteOwner = new Commands.ManageOwnersViewModelCommands.DeleteOwner(_vetSystem, this);
            CommandEditOwner = new Commands.ManageOwnersViewModelCommands.EditOwner(this, _navEditOwnerService);
        }

        public static ManageOwnersViewModel LoadMedsViewModel(VetSystem vetSystem, INavigationService<OwnerRegistrationViewModel> navOwnerRegisterService,
            LayoutNavigationServiceParam<OwnerViewModel, EditOwnerViewModel> navEditOwnerService)
        {
            ManageOwnersViewModel _vm = new ManageOwnersViewModel(vetSystem, navOwnerRegisterService, navEditOwnerService);

            _vm.CommandLoadOwners.Execute(null);

            return _vm;
        }

        public void ReloadOwners(IEnumerable<Owner> owners)
        {
            _owners.Clear();
            foreach (Owner owner in owners)
            {
                _owners.Add(new OwnerViewModel(owner));
            }
        }
    }
}
