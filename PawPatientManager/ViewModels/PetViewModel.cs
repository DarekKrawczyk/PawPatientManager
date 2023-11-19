using PawPatientManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PawPatientManager.ViewModels
{
    public class PetViewModel
    {
        #region Owner fields in ManageOwnerViewModel.cs
        private Pet _pet;
        #endregion
        #region Properties for fields
        public Pet Pet { get { return _pet; } }
        public Guid ID { get { return _pet.ID; } set { _pet.ID = value; } }
        public string Name { get { return _pet.Name; } set { _pet.Name = value; } }
        public bool Gender { get { return _pet.Gender; } set { _pet.Gender = value; } }
        public string Race { get { return _pet.Race; } set { _pet.Race = value; } }
        public string MicrochipNumber { get { return _pet.MicrochipNumber; } set { _pet.MicrochipNumber = value; } }
        public string OwnerNameAndSurname { get { return $"{_pet.Owner?.Name} {_pet.Owner?.Surname}"; } }
        public Owner Owner { get { return _pet.Owner; } set { _pet.Owner = value; } }
        public DateTime BirthDate { get { return _pet.BirthDate; } set { _pet.BirthDate = value; } }
        public List<Visit> Visits { get { return _pet.Visits; } set { _pet.Visits = value; } }
        public List<MedicalReceipt> Medicals { get { return _pet.Medicals; } set { _pet.Medicals = value; } }
        #endregion
        #region Properties - *RegisterVisitViewModel.cs*
        public string OwnerFullName { get { return $"{_pet.Owner?.Name} { _pet.Owner?.Surname}"; } }
        public string Species { get { return _pet.Species; } set { _pet.Species = value; } }
        public string Age { get { return ((DateTime.Now - BirthDate).Days/365).ToString(); } }
        #endregion
        #region Constructor
        public PetViewModel(Pet pet)
        {
            _pet = pet;
        }
        #endregion
        #region Methods
        public bool IsNull()
        {
            if (_pet == null) return true;
            else return false;
        }
        #endregion
    }
}
