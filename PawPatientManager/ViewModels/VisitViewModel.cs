using PawPatientManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PawPatientManager.ViewModels
{
    public class VisitViewModel
    {
        #region Owner fields in ManageOwnerViewModel.cs
        private Visit _visit;
        #endregion
        #region Properties
        public uint ID { get { return _visit.ID; } set { _visit.ID = value; } }
        public Visit Visit { get { return _visit; } set { _visit = value; } }
        public Pet Pet { get { return _visit.Pet; } set { _visit.Pet = value; } }
        public Vet Vet { get { return _visit.Vet; } set { _visit.Vet = value; } }
        public Owner Owner { get { return _visit.Pet.Owner; } }
        public DateTime Date { get { return _visit.Date; } set { _visit.Date = value; } }
        public List<MedicalReceipt> MedicalReceipts { get { return _visit.MedicalReceipts; } set { _visit.MedicalReceipts = value; } }
        #endregion
        #region Properties for fields
        #endregion
        public string VetFullName { get { return $"{_visit.Vet.Name} {_visit.Vet.Surname}"; } }
        public string OwnerFullName { get { return $"{_visit.Owner?.Name} {_visit.Owner?.Surname}"; } }
        public string PetFullName { get { return $"{_visit.Pet.Name}"; } }
        public string VisitDate { get { return _visit.Date.ToString("dd-MM-yyyy"); } }
        public string VisitDateHour { get { return _visit.Date.ToString("HH:mm"); } }
        #region Constructor
        public VisitViewModel(Visit visit)
        {
            _visit = visit;
        }
        #endregion
        #region Methods
        public bool IsNull()
        {
            if (_visit == null) return true;
            else return false;
        }
        #endregion
    }
}
