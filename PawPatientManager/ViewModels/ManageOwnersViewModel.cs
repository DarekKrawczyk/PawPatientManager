using PawPatientManager.Commands;
using PawPatientManager.Models;
using PawPatientManager.Services;
using PawPatientManager.Stores;
using PawPatientManager.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
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
        // -- Filters --
        private string _nameFilter = string.Empty;
        private string _surnameFilter = string.Empty;
        private string _phoneNumberFilter = string.Empty;
        private string _emailFilter = string.Empty;
        //private string _petsAmountFilter = string.Empty;
        #endregion
        #region Properties
        /*  Just a Property to get all of the owners, in this case notification is not required, 
         *  because it will be mostly used to iterate through and check smth. Plus it provides interface.
         */
        public IEnumerable<OwnerViewModel> Owners {  get { return _owners; } set { OnPropertyChanged(nameof(Owners)); } }
        public OwnerViewModel SelectedOwner { get { return _selectedOwnerViewModel; } set { _selectedOwnerViewModel = value; OnPropertyChanged(nameof(SelectedOwner)); } }
        public ICollectionView OwnersView;
        // -- Filters --
        public string NameFilter
        {
            get { return _nameFilter; }
            set
            {
                _nameFilter = value;
                OnPropertyChanged(nameof(NameFilter));
                OwnersView.Refresh();
            }
        }
        public string SurnameFilter
        {
            get { return _surnameFilter; }
            set
            {
                _surnameFilter = value;
                OnPropertyChanged(nameof(SurnameFilter));
                OwnersView.Refresh();
            }
        }
        public string PhoneNumberFilter
        {
            get { return _phoneNumberFilter; }
            set
            {
                _phoneNumberFilter = value;
                OnPropertyChanged(nameof(PhoneNumberFilter));
                OwnersView.Refresh();
            }
        }
        public string EmailFilter
        {
            get { return _emailFilter; }
            set
            {
                _emailFilter = value;
                OnPropertyChanged(nameof(EmailFilter));
                OwnersView.Refresh();
            }
        }
        //public string PetsAmountFilter
        //{
        //    get { return _petsAmountFilter; }
        //    set
        //    {
        //        _petsAmountFilter = value;
        //        OnPropertyChanged(nameof(PetsAmountFilter));
        //        OwnersView.Refresh();
        //    }
        //}
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
            OwnersView = CollectionViewSource.GetDefaultView(_owners);

            OwnersView.Filter = FilterOwners;

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
        public bool FilterOwners(object obj)
        {
            if (obj is OwnerViewModel owner)
            {
                return owner.Name.Contains(NameFilter, StringComparison.InvariantCultureIgnoreCase) && 
                    owner.Surname.Contains(SurnameFilter, StringComparison.InvariantCultureIgnoreCase) &&
                    owner.Email.Contains(EmailFilter, StringComparison.InvariantCultureIgnoreCase) &&
                    owner.PhoneNumber.Contains(PhoneNumberFilter, StringComparison.InvariantCultureIgnoreCase);
            }
            return false;
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
