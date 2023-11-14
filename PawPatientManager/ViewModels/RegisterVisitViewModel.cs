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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PawPatientManager.ViewModels
{
    public class RegisterVisitViewModel : ViewModelBase
    {
        #region Fields
        private VetSystem _vetSystem;
        private INavigationService<VisitsViewModel> _navReturnService;
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
        public IEnumerable<HourViewModel> Hours { get { return HourViewModel.GenerateHours(); } }
        public PetViewModel SelectedPet { get { return _selectedPetVM; } set { _selectedPetVM = value; } }
        public HourViewModel SelectedHour { get { return _selectedHourVM; } set { _selectedHourVM = value; } }
        public VetViewModel SelectedVet { get { return _selectedVetVM; } set { _selectedVetVM = value; } }
        public DateTime SelectedDate { get { return _selectedDate; } set { _selectedDate = value; OnPropertyChanged(nameof(SelectedDate)); } }
        #endregion
        #region Commands
        public ICommand CommandRegisterVisit { get; }
        public ICommand CommandReturn { get; }
        public ICommand CommandHandleVetSelectionChanged { get; }
        public ICommand CommandHandlePetSelectionChanged { get; }
        public ICommand CommandHandleHourSelectionChanged { get; }
        public ICommand CommandHandleDateSelectionChanged { get; }
        #endregion
        #region Constructor
        public RegisterVisitViewModel(VetSystem vetSystem, INavigationService<VisitsViewModel> navReturnService)
        {
            _selectedDate = DateTime.Now;
            _vetSystem = vetSystem;
            _navReturnService = navReturnService;

            _pets = new ObservableCollection<PetViewModel>();
            _vets = new ObservableCollection<VetViewModel>();

            CommandHandleHourSelectionChanged = new Commands.RegisterVisitCommands.UpdateHour(this);
            CommandHandlePetSelectionChanged = new Commands.RegisterVisitCommands.UpdatePet(this);
            CommandHandleVetSelectionChanged = new Commands.RegisterVisitCommands.UpdateVet(this);
            CommandHandleVetSelectionChanged = new Commands.RegisterVisitCommands.UpdateDate(this);
            CommandReturn = new NavigateCommand<VisitsViewModel>(_navReturnService);
            CommandRegisterVisit = new Commands.RegisterVisitCommands.RegisterVisit(_vetSystem, this);

            ReloadPets();
            ReloadVets();

        }
        #endregion
        #region Methods
        public DateTime GetVisitDateTime()
        {
            DateTime timePart = DateTime.ParseExact(_selectedHourVM.Hour, "H:mm", null);
            DateTime dateTime = new DateTime(_selectedDate.Year, _selectedDate.Month, _selectedDate.Day,
                timePart.Hour, timePart.Minute, timePart.Second);
            return dateTime;
        }
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
            foreach(Vet vet in _vetSystem.Vets)
            {
                _vets.Add(new VetViewModel(vet));
            }
        }
        #endregion
    }
}