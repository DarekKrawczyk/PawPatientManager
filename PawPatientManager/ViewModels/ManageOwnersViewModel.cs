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
        //private NavigationBarViewModel _navigationBarVM;
        private NavigationStore _navigationStore;
        private VetSystem _vetSystem;
        #endregion
        #region Properties
        /*  Just a Property to get all of the owners, in this case notification is not required, 
         *  because it will be mostly used to iterate through and check smth. Plus it provides interface.
         */
        public IEnumerable<OwnerViewModel> Owners {  get { return _owners; } }
        //public NavigationBarViewModel NavigationBarVM {  get { return _navigationBarVM; } }
        #endregion
        #region Commands
        public ICommand CommandAddOwner { get; }
        public ICommand CommandDeleteOwner { get; }
        public ICommand CommandEditOwner { get; }
        #endregion
        public ManageOwnersViewModel(NavigationStore navigator, VetSystem vetSystem) 
        { 
            //_navigationBarVM = navigationBarVM;
            _vetSystem = vetSystem;
            _navigationStore = navigator;
            _owners = new ObservableCollection<OwnerViewModel>();
            foreach(Owner owner in vetSystem.Owners)
            {
                _owners.Add(new OwnerViewModel(owner));
            }
            // TODO: Commands!!
            CommandAddOwner = new NavigateCommand<OwnerRegistrationViewModel>(new NavigationService<OwnerRegistrationViewModel>(_navigationStore, ()=> new OwnerRegistrationViewModel(_vetSystem, _navigationStore)));
        }
    }
}
