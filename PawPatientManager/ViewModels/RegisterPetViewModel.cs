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
    public class RegisterPetViewModel : ViewModelBase
    {
        #region Fields
        private VetSystem _vetSystem;
        private INavigationService<PetsViewModel> _navReturnService;
        private ObservableCollection<OwnerViewModel> _owners;
        private OwnerViewModel _selectedOwnerVM;
        #endregion
        #region Properties
        #endregion
        #region Representation of "View" fields
        private uint _id;
        private string _name;
        private bool _gender;
        private string _species;
        private string _race;
        private string _microchipNumber;
        private DateTime _birthDate;
        #endregion
        #region Properties of representations
        public IEnumerable<OwnerViewModel> Owners { get { return _owners; } }
        public uint ID { get { return _id; } set { _id = value; OnPropertyChanged(nameof(ID)); } }
        public string Name { get { return _name; } set { _name = value; OnPropertyChanged(nameof(Name)); } }
        public OwnerViewModel SelectedOwner { get { return _selectedOwnerVM; } set { _selectedOwnerVM = value; OnPropertyChanged(nameof(SelectedOwner)); } }
        public bool Gender { get { return _gender; } set { _gender = value; OnPropertyChanged(nameof(Gender)); } }
        public string Spieces { get { return _species; } set { _species = value; OnPropertyChanged(nameof(Spieces)); } }
        public string Race { get { return _race; } set { _race = value; OnPropertyChanged(nameof(Race)); } }
        public string MicrochipNumber { get { return _microchipNumber; } set { _microchipNumber = value; OnPropertyChanged(nameof(MicrochipNumber)); } }
        public DateTime BirthDate { get { return _birthDate; } set { _birthDate = value; OnPropertyChanged(nameof(BirthDate)); } }
        #endregion
        #region Commands
        public ICommand CommandRegisterPet { get; }
        public ICommand CommandReturn { get; }
        public ICommand CommandHandleComboBoxSelectionChanged { get; }
        #endregion
        #region Constructor
        public RegisterPetViewModel(VetSystem vetSystem, INavigationService<PetsViewModel> navReturnService)
        {
            _vetSystem = vetSystem;
            _navReturnService = navReturnService;

            _owners = new ObservableCollection<OwnerViewModel>();
            ReloadOwners();

            CommandReturn = new NavigateCommand<PetsViewModel>(_navReturnService);
            CommandRegisterPet = new Commands.EditPetCommand.RegisterPet(_vetSystem, this);
            CommandHandleComboBoxSelectionChanged = new Commands.PetsCommands.UpdateSelectedOwner(this);
        }
        #endregion
        #region Methods
        private void ReloadOwners()
        {
            _owners.Clear();
            foreach(Owner owner in _vetSystem.Owners)
            {
                _owners.Add(new OwnerViewModel(owner));
            }
        }
        #endregion
    }
}
