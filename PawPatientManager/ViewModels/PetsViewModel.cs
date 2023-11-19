using PawPatientManager.Commands;
using PawPatientManager.Models;
using PawPatientManager.Services;
using PawPatientManager.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
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
        // -- Filters --
        private string _nameFilter = string.Empty;
        private string _ownerFilter = string.Empty;
        private string _spiecesFilter = string.Empty;
        private string _raceFilter = string.Empty;
        private string _ageFilter = string.Empty;
        private string _microchipNumberFilter = string.Empty;
        #endregion
        #region Properties
        public IEnumerable<PetViewModel> Pets { get { return _pets; } set { OnPropertyChanged(nameof(Pets)); } }
        public PetViewModel SelectedPet { get { return _selectedPetViewModel; } set { _selectedPetViewModel = value; OnPropertyChanged(nameof(SelectedPet)); } }
        // -- Filters --
        public ICollectionView PetsView;
        public string NameFilter { get { return _nameFilter; } set { _nameFilter = value; OnPropertyChanged(nameof(NameFilter)); PetsView.Refresh(); } }
        public string OwnerFilter { get { return _ownerFilter; } set { _ownerFilter = value; OnPropertyChanged(nameof(OwnerFilter)); PetsView.Refresh(); } }
        public string SpiecesFilter { get { return _spiecesFilter; } set { _spiecesFilter = value; OnPropertyChanged(nameof(SpiecesFilter)); PetsView.Refresh(); } }
        public string RaceFilter { get { return _raceFilter; } set { _raceFilter = value; OnPropertyChanged(nameof(RaceFilter)); PetsView.Refresh(); } }
        public string AgeFilter { get { return _ageFilter; } set { _ageFilter= value; OnPropertyChanged(nameof(AgeFilter)); PetsView.Refresh(); } }
        public string MicrochipNumberFilter { get { return _microchipNumberFilter; } set { _microchipNumberFilter = value; OnPropertyChanged(nameof(MicrochipNumberFilter)); PetsView.Refresh(); } }
        #endregion
        #region Commands
        public ICommand CommandRegisterPet { get; }
        public ICommand CommandDeletePet { get; }
        public ICommand CommandEditPet { get; }
        public ICommand CommandLoadPets { get; }
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

            PetsView = CollectionViewSource.GetDefaultView(_pets);
            PetsView.Filter = FilterPets;

            CommandLoadPets = new Commands.EditPetCommand.LoadPets(_vetSystem, this);
            CommandRegisterPet = new NavigateCommand<RegisterPetViewModel>(_navRegisterPetVMService);
            CommandEditPet = new Commands.PetsCommands.EditPet(this, _navEditPetService);
            CommandDeletePet = new Commands.PetsCommands.DeletePet(_vetSystem, this);
            CommandHandlePetSelectionChange = new Commands.PetsCommands.UpdateSelected(this);
        }
        public static PetsViewModel LoadViewModel(VetSystem vetSystem, INavigationService<RegisterPetViewModel> navRegisterPetVMService,
             LayoutNavigationServiceParam<PetViewModel, EditPetViewModel> navPetOwnerService)
        {
            PetsViewModel petViewModel = new PetsViewModel(vetSystem, navRegisterPetVMService, navPetOwnerService);

            petViewModel.CommandLoadPets.Execute(null);

            return petViewModel;
        }

        public bool FilterPets(object obj)
        {
            if (obj is PetViewModel pet)
            {
                return pet.Name.Contains(NameFilter, StringComparison.InvariantCultureIgnoreCase) &&
                    pet.OwnerNameAndSurname.Contains(OwnerFilter, StringComparison.InvariantCultureIgnoreCase) &&
                    pet.Species.Contains(SpiecesFilter, StringComparison.InvariantCultureIgnoreCase) &&
                    pet.Age.Contains(AgeFilter, StringComparison.InvariantCultureIgnoreCase) &&
                    pet.MicrochipNumber.Contains(MicrochipNumberFilter, StringComparison.InvariantCultureIgnoreCase) &&
                    pet.Race.Contains(RaceFilter, StringComparison.InvariantCultureIgnoreCase);
            }
            return false;
        }

        public void ReloadPets(IEnumerable<Pet> pets)
        {
            _pets.Clear();
            foreach (Pet pet in pets)
            {
                _pets.Add(new PetViewModel(pet));
            }
        }
    }
}
