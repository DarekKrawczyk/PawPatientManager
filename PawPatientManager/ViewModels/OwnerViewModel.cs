using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using PawPatientManager.Models;

namespace PawPatientManager.ViewModels
{
    public class OwnerViewModel : ViewModelBase
    {
        /*  This viewmodel is created to make binding between Owner and List of owners in *ManageOwnersViewModel.cs*.
         *  Created because viewmodel for each owner requires *OnPropertyChange()* method, without that, there could
         *  be memory leaks.
         */
        #region Owner fields in ManageOwnerViewModel.cs
        private Owner _ownerModel;
        #endregion
        #region Properties for fields
        public Owner Owner{ get { return _ownerModel; } }
        public uint ID { get { return _ownerModel.ID; } }
        public string Name { get { return _ownerModel.Name; } }
        public string NameAndSurname { get { return $"{_ownerModel.Name} {_ownerModel.Surname}"; } }
        public string Surname { get { return _ownerModel.Surname; } }
        public bool Gender { get { return _ownerModel.Gender; } }
        public List<Pet> Pets { get { return _ownerModel.Pets; } }
        public DateTime BirthDate { get { return _ownerModel.BirthDate; } }
        public string BirthDateAsString { get { return $"{_ownerModel.BirthDate}"; } }
        public string PhoneNumber { get { return _ownerModel.PhoneNumber; } }
        public string Adress { get { return _ownerModel.Adress; } }
        public string PESEL { get { return _ownerModel.PESEL; } }
        public string Email { get { return _ownerModel.Email; } }
        #endregion
        #region Commands
        #endregion
        #region Constructor
        public OwnerViewModel(Owner ownerModel)
        {
            _ownerModel = ownerModel;
        }
        #endregion
        #region Methods
        public bool IsNull()
        {
            if (_ownerModel == null) return true;
            else return false;
        }
        #endregion
    }
}
