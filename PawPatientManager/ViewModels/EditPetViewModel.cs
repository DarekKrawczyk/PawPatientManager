using PawPatientManager.Commands;
using PawPatientManager.Models;
using PawPatientManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PawPatientManager.ViewModels
{
    public class EditPetViewModel : ViewModelBase
    {
        #region Fields
        private VetSystem _vetSystem;
        private INavigationService<PetsViewModel> _navPetsVMService;
        private LayoutNavigationServiceParam<PetViewModel, EditPetViewModel> _navEditPetService;
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
        private Owner _owner;
        private List<Visit> _visits;
        private List<MedicalReceipt> _medicals;
        #endregion
        #region Properties of representations
        public uint ID { get { return _id; } set { _id = value; OnPropertyChanged(nameof(ID)); } }
        public string Name { get { return _name; } set { _name = value; OnPropertyChanged(nameof(Name)); } }
        public string OwnerNameAndSurname { get { return $"{_owner?.Name} {_owner?.Surname}"; } }
        public bool Gender { get { return _gender; } set { _gender = value; OnPropertyChanged(nameof(Gender)); } }
        public string Spiecies { get { return _species; } set { _species = value; OnPropertyChanged(nameof(Spiecies)); } }
        public string Race { get { return _race; } set { _race = value; OnPropertyChanged(nameof(Race)); } }
        public string MicrochipNumber { get { return _microchipNumber; } set { _microchipNumber = value; OnPropertyChanged(nameof(MicrochipNumber)); } }
        public DateTime BirthDate { get { return _birthDate; } set { _birthDate = value; OnPropertyChanged(nameof(BirthDate)); } }
        public Owner Owner { get { return _owner; } }
        public List<Visit> Visits { get { return _visits; } }
        public List<MedicalReceipt> Medicals { get { return _medicals; } }
        #endregion
        #region Commands
        public ICommand CommandEditPet { get; }
        public ICommand CommandReturn { get; }
        #endregion
        #region Constructor
        public EditPetViewModel(VetSystem vetSystem, PetViewModel petVM, INavigationService<PetsViewModel> navPetsVMService)
        {
            _vetSystem = vetSystem;
            _navPetsVMService = navPetsVMService;
            //_navEditPetService = navEditPetService;

            _id = petVM.ID;
            _name = petVM.Name;
            _owner = petVM.Owner;
            _gender = petVM.Gender;
            _species = petVM.Spiecies;
            _race = petVM.Race;
            _microchipNumber = petVM.MicrochipNumber;
            _birthDate = petVM.BirthDate;
            _visits = petVM.Visits;
            _medicals = petVM.Medicals;

            CommandEditPet = new Commands.EditPetCommand.EditPet(_vetSystem, this);
            CommandReturn = new NavigateCommand<PetsViewModel>(_navPetsVMService);
        }
        #endregion
    }
}