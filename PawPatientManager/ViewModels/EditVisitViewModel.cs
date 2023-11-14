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
        private INavigationService<VisitsViewModel> _navVisitsVMService;
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
        #endregion
        #region Properties for XAML
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
        public EditVisitViewModel(VetSystem vetSystem, VisitViewModel visitVM, INavigationService<VisitsViewModel> navVisitsVMService)
        {
            _vetSystem = vetSystem;
            _navVisitsVMService = navVisitsVMService;
            _selectedVisitVM = visitVM;

            _pets = new ObservableCollection<PetViewModel>();
            _vets = new ObservableCollection<VetViewModel>();

            ReloadPets();
            ReloadVets();

            SelectedDate = _selectedVisitVM.Date;
            SelectedVet = new VetViewModel(_selectedVisitVM.Vet);
            SelectedPet = new PetViewModel(_selectedVisitVM.Pet);
            SelectedHour = new HourViewModel() { Hour = _selectedVisitVM.VisitDateHour };


            //CommandEditPet = new Commands.EditPetCommand.EditPet(_vetSystem, this);
            //CommandReturn = new NavigateCommand<PetsViewModel>(_navPetsVMService);
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
