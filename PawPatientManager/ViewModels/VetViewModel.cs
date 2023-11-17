using PawPatientManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PawPatientManager.ViewModels
{
    public class VetViewModel
    {
        #region Owner fields in ManageOwnerViewModel.cs
        private Vet _vet;
        #endregion
        #region Properties
        public Guid ID { get { return _vet.ID; } set { _vet.ID = value; } }
        public Vet Vet { get { return _vet; } set { _vet = value; } }
        public string Name { get { return _vet.Name; } set { _vet.Name = value; } }
        public string Surname { get { return _vet.Surname; } set { _vet.Surname = value; } }
        List<Visit> Visits { get { return _vet.Visits; } set { _vet.Visits = value; } }
        #endregion
        #region Constructor
        public VetViewModel(Vet vet)
        {
            _vet = vet;
        }
        #endregion
        #region Methods
        public bool IsNull()
        {
            if (_vet == null) return true;
            else return false;
        }
        #endregion
    }
}
