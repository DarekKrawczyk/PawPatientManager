using PawPatientManager.Commands;
using PawPatientManager.Models;
using PawPatientManager.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PawPatientManager.ViewModels
{
    public class EditVisitViewModel : ViewModelBase
    {
        #region Fields
        private VetSystem _vetSystem;
        private VisitViewModel _selectedVisitVM;
        private INavigationService<VisitsViewModel> _nevReturnVM;
        private LayoutNavigationServiceParam<VisitViewModel, EditVisitViewModel> _navEditVisitService;
        #endregion
        #region Properties
        #endregion
        #region Fields for XAML
        private DateTime _selectedDate;
        private PetViewModel _selectedPetVM;
        private VetViewModel _selectedVetVM;
        private HourViewModel _selectedHourVM;
        private ObservableCollection<PetViewModel> _pets;
        private ObservableCollection<VetViewModel> _vets;
        public Guid _id;
        public string _petName;
        public string _ownerFullName;
        public string _species;
        public string _race;
        public bool _gender;
        public string _microchipNumber;
        public string _vetName;
        public string _vetSurname;
        #endregion
        #region Properties for XAML
        public Guid ID { get { return _id; } set { _id = value; } }
        public string PetName { get { return _petName; } set { _petName = value; OnPropertyChanged(nameof(PetName)); } }
        public string OwnerFullName { get { return _ownerFullName; } set { _ownerFullName = value; OnPropertyChanged(nameof(OwnerFullName)); } }
        public string Species { get { return _species; } set { _species = value; OnPropertyChanged(nameof(Species)); } }
        public string Race { get { return _race; } set { _race = value; OnPropertyChanged(nameof(Race)); } }
        public bool Gender { get { return _gender; } set { _gender = value; OnPropertyChanged(nameof(Gender)); } }
        public string MicrochipNumber { get { return _microchipNumber; } set { _microchipNumber = value; OnPropertyChanged(nameof(MicrochipNumber)); } }
        public string VetName { get { return _vetName; } set { _vetName = value; OnPropertyChanged(nameof(VetName)); } }
        public string VetSurname { get { return _vetSurname; } set { _vetSurname = value; OnPropertyChanged(nameof(VetSurname)); } }
        public IEnumerable<VetViewModel> Vets { get { return _vets; } set { OnPropertyChanged(nameof(Vets)); } }
        public IEnumerable<PetViewModel> Pets { get { return _pets; } set { OnPropertyChanged(nameof(Pets)); } }
        public IEnumerable<HourViewModel> Hours { get { return HourViewModel.GenerateHours(); } set { OnPropertyChanged(nameof(Hours)); } }
        public PetViewModel SelectedPet { get { return _selectedPetVM; } set { _selectedPetVM = value; OnPropertyChanged(nameof(SelectedPet)); } }
        public HourViewModel SelectedHour { get { return _selectedHourVM; } set { _selectedHourVM = value; OnPropertyChanged(nameof(SelectedHour)); } }
        public VetViewModel SelectedVet { get { return _selectedVetVM; } set { _selectedVetVM = value; OnPropertyChanged(nameof(SelectedVet)); } }
        public DateTime SelectedDate { get { return _selectedDate; } set { _selectedDate = value; OnPropertyChanged(nameof(SelectedDate)); } }
        #endregion
        #region Commands
        public ICommand CommandUpdateVisit { get; }
        public ICommand CommandReturn { get; }
        #endregion
        #region Constructor
        public EditVisitViewModel(VetSystem vetSystem, VisitViewModel visitVM, INavigationService<VisitsViewModel> nevReturnVM)
        {
            _vetSystem = vetSystem;
            _nevReturnVM = nevReturnVM;
            _selectedVisitVM = visitVM;

            _pets = new ObservableCollection<PetViewModel>();
            _vets = new ObservableCollection<VetViewModel>();

            ReloadPets();
            ReloadVets();

            ID = _selectedVisitVM.ID;
            PetName = _selectedVisitVM.Pet.Name;
            OwnerFullName = _selectedVisitVM.OwnerFullName;
            Species = _selectedVisitVM.Pet.Species;
            Race = _selectedVisitVM.Pet.Race;
            Gender = _selectedVisitVM.Pet.Gender;
            MicrochipNumber = _selectedVisitVM.Pet.MicrochipNumber;
            VetName = _selectedVisitVM.Vet.Name;
            VetSurname = _selectedVisitVM.Vet.Surname;

            SelectedDate = _selectedVisitVM.Date;
            SelectedVet = new VetViewModel(_selectedVisitVM.Vet);
            SelectedPet = new PetViewModel(_selectedVisitVM.Pet);
            SelectedHour = new HourViewModel() { Hour = _selectedVisitVM.VisitDateHour };

            CommandUpdateVisit = new Commands.EditVisitCommands.EditVisit(_vetSystem, this);
            CommandReturn = new NavigateCommand<VisitsViewModel>(_nevReturnVM);
        }
        #endregion
        private void ReloadPets()
        {
            _pets.Clear();
            foreach (Pet pet in _vetSystem.Pets)
            {
                _pets.Add(new PetViewModel(pet));
            }
        }
        private void ReloadVets()
        {
            _vets.Clear();
            foreach (Vet vet in _vetSystem.Vets)
            {
                _vets.Add(new VetViewModel(vet));
            }
        }
    }
}
