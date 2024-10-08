﻿using PawPatientManager.Commands;
using PawPatientManager.Models;
using PawPatientManager.Services;
using PawPatientManager.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PawPatientManager.ViewModels
{
    public class EditOwnerViewModel : ViewModelBase
    {
        #region Fields
        private VetSystem _vetSystem;
        private INavigationService<ManageOwnersViewModel> _navManageOwnersService;
        #endregion
        #region Properties
        #endregion
        #region Representation of "View" fields
        private Guid _id;
        private string _name;
        private string _surname;
        private bool _gender;
        private bool _genderX;
        private DateTime _birthDate;
        private string _adress;
        private string _phoneNumber;
        private string _email;
        private string _pesel;
        private OwnerViewModel _originalOwner;
        #endregion
        #region Properties of representations
        public Guid ID { get { return _id; } set { _id = value; OnPropertyChanged(nameof(ID)); } }
        public string Name { get { return _name; } set { _name = value; OnPropertyChanged(nameof(Name)); } }
        public string Surname { get { return _surname; } set { _surname = value; OnPropertyChanged(nameof(Surname)); } }
        public bool Gender { get { return _gender; } set { _gender = value; _genderX = !value; OnPropertyChanged(nameof(Gender)); OnPropertyChanged(nameof(GenderX)); } }
        public bool GenderX { get { return _genderX; } set { _genderX = value; _gender = !value; OnPropertyChanged(nameof(Gender)); OnPropertyChanged(nameof(GenderX)); } }
        public DateTime BirthDate { get { return _birthDate; } set { _birthDate = value; OnPropertyChanged(nameof(BirthDate)); } }
        public string Adress { get { return _adress; } set { _adress = value; OnPropertyChanged(nameof(Adress)); } }
        public string PhoneNumber { get { return _phoneNumber; } set { _phoneNumber = value; OnPropertyChanged(nameof(PhoneNumber)); } }
        public string Email { get { return _email; } set { _email = value; OnPropertyChanged(nameof(Email)); } }
        public string PESEL { get { return _pesel; } set { _pesel = value; OnPropertyChanged(nameof(PESEL)); } }
        public OwnerViewModel OriginalOwner { get { return _originalOwner; } set { _originalOwner = value; } }
        #endregion
        #region Commands
        public ICommand CommandEditOwner { get; }
        public ICommand CommandReturn { get; }
        #endregion
        #region Constructor
        public EditOwnerViewModel(VetSystem vetSystem, OwnerViewModel ownerVM, INavigationService<ManageOwnersViewModel> navManageOwnersService)
        {
            _vetSystem = vetSystem;
            _navManageOwnersService = navManageOwnersService;
            _originalOwner = ownerVM;

            _name = ownerVM.Name;
            _surname = ownerVM.Surname;
            Gender = ownerVM.Gender;
            _birthDate = ownerVM.BirthDate;
            _adress = ownerVM.Adress;
            _phoneNumber = ownerVM.PhoneNumber;
            _email = ownerVM.Email;
            _pesel = ownerVM.PESEL;

            CommandEditOwner = new Commands.OwnerRegistratorViewModelCommands.EditOwner(_vetSystem, this);
            CommandReturn = new NavigateCommand<ManageOwnersViewModel>(_navManageOwnersService);
    }
        #endregion
    }
}
