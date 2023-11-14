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
    public class PetsViewModel : ViewModelBase
    {
        #region 
        private VetSystem _vetSystem;
        private PetViewModel _selectedPetViewModel;
        private ObservableCollection<PetViewModel> _pets;
        private INavigationService<RegisterPetViewModel> _navRegisterPetVMService;
        private LayoutNavigationServiceParam<PetViewModel, EditPetViewModel> _navEditPetService;
        #endregion
        #region Properties
        public IEnumerable<PetViewModel> Pets { get { return _pets; } set { OnPropertyChanged(nameof(Pets)); } }
        public PetViewModel SelectedPet { get { return _selectedPetViewModel; } set { _selectedPetViewModel = value; } }
        #endregion
        #region Commands
        public ICommand CommandRegisterPet { get; }
        public ICommand CommandDeletePet { get; }
        public ICommand CommandEditPet { get; }
        public ICommand CommandHandlePetSelectionChange { get; }
        #endregion
        public PetsViewModel(VetSystem vetSystem, INavigationService<RegisterPetViewModel> navRegisterPetVMService,
             LayoutNavigationServiceParam<PetViewModel, EditPetViewModel> navPetOwnerService)
        {
            _selectedPetViewModel = new PetViewModel(null);
            _vetSystem = vetSystem;
            _navRegisterPetVMService = navRegisterPetVMService;
            _navEditPetService = navPetOwnerService;

            _pets = new ObservableCollection<PetViewModel>();
            ReloadOwners();

            // TODO: Commands!!
            CommandRegisterPet = new NavigateCommand<RegisterPetViewModel>(_navRegisterPetVMService);
            CommandEditPet = new Commands.PetsCommands.EditPet(this, _navEditPetService);
            CommandDeletePet = new Commands.PetsCommands.DeletePet(this);
            CommandHandlePetSelectionChange = new Commands.PetsCommands.UpdateSelected(this);
            
            //OwnerViewModel os = OwnersList
            //AddSelectLocomotifCommand = new Commands.ManageOwnersViewModelCommands.UpdateSelected(this);
            //AddSelectLocomotifCommand = new RelayCommand(AddSelectLocomotif, CanAddSelectLocomotif);
            //CommandEditOwner = new NavigateCommandParam<OwnerViewModel, EditOwnerViewModel>(_navEditOwnerService, SelectedOwnerViewModel);
            //CommandAddOwner = new NavigateCommand<OwnerRegistrationViewModel>(new NavigationService<OwnerRegistrationViewModel>(_navigationStore, ()=> new OwnerRegistrationViewModel(_vetSystem, _navigationStore)));
        }

        private void ReloadOwners()
        {
            _pets.Clear();
            foreach (Pet pet in _vetSystem.Pets)
            {
                _pets.Add(new PetViewModel(pet));
            }
        }

        public bool DeletePet(PetViewModel petVM)
        {
            bool result = false;
            if (petVM == null)
            {
                result = _vetSystem.Pets.Remove(_selectedPetViewModel.Pet);
            }
            else result = _vetSystem.Pets.Remove(petVM.Pet);
            ReloadOwners();
            OnPropertyChanged(nameof(Pets));
            return result;
        }
    }
}
