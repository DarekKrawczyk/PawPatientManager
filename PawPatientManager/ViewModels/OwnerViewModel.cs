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
        public uint ID { get { return _ownerModel.ID; } }
        public string Name { get { return _ownerModel.Name; } }
        public string Surname { get { return _ownerModel.Surname; } }
        public DateTime BirthDate { get { return _ownerModel.BirthDate; } }
        public string PhoneNumber { get { return _ownerModel.PhoneNumber; } }
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
    }
}
