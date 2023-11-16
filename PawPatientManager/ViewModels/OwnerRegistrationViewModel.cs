using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Controls;
using PawPatientManager.Models;
using System.Security.Cryptography;
using System.Windows.Input;
using PawPatientManager.Stores;
using PawPatientManager.Commands;
using PawPatientManager.Services;

namespace PawPatientManager.ViewModels
{
    public class OwnerRegistrationViewModel : ViewModelBase
    {
        /* In this kind of classes we have to implements every *thing* that should
         * have binding between Model and View. For example, in this class "Owner"
         * object will be added. You have to create binding with "OwnerRegistrationView.xaml"
         * and create placeholders for values that you get from there. All of the fields specyfied in 
         * .xaml file have to have representation in this class.
         */
        #region Fields
        private VetSystem _vetSystem;
        //private NavigationBarViewModel _navigationBarVM;
        private NavigationStore _navigator;
        private INavigationService<ManageOwnersViewModel> _navManageOwnersService;
        //private NavigationStore _navigator;
        #endregion
        #region Properties
        //public NavigationBarViewModel NavigationBarVM {  get { return _navigationBarVM; } }
        #endregion
        #region Representation of "View" fields
        public Guid _id;
        private string _name;
        private string _surname;
        private bool _gender;
        private DateTime _birthDate;
        private string _adress;
        private string _phoneNumber;
        private string _email;
        private string _pesel;
        #endregion
        #region Properties of representations
        /*  Whenever value of the field if requested *get* nothing changes, but once it is changed
         *  *OnPropertyChanged()* method is run, under the hood it invokes *PropertyChangedEventHandler*
         *  handler which tells UI that data has been changed. Always remember to use *{Binding --name--}* 
         *  in fields of the .xaml file.
         */

        public Guid ID { get { return _id; } set { _id = value; OnPropertyChanged(nameof(ID)); } }
        public string Name { get { return _name; } set { _name = value; OnPropertyChanged(nameof(Name)); } }
        public string Surname { get { return _surname; } set { _surname = value; OnPropertyChanged(nameof(Surname)); } }
        public bool Gender { get { return _gender; } set { _gender = value; OnPropertyChanged(nameof(Gender)); } }
        public DateTime BirthDate { get { return _birthDate; } set { _birthDate = value; OnPropertyChanged(nameof(BirthDate)); } }
        public string Adress { get { return _adress; } set { _adress = value; OnPropertyChanged(nameof(Adress)); } }
        public string PhoneNumber { get { return _phoneNumber; } set { _phoneNumber = value; OnPropertyChanged(nameof(PhoneNumber)); } }
        public string Email { get { return _email; } set { _email = value; OnPropertyChanged(nameof(Email)); } }
        public string PESEL { get { return _pesel; } set { _pesel = value; OnPropertyChanged(nameof(PESEL)); } }
        #endregion
        #region Commands
        /* These are commands used in .xaml, for example *CommandRegsiterOwner* is a command for button click of 
         * *Register Owner* button. They have to have binding like properties!!!!
         */
        public ICommand CommandRegisterOwner{ get; }
        public ICommand CommandClearData { get; }
        public ICommand CommandReturn { get; }
        #endregion
        #region Constructor
        public OwnerRegistrationViewModel(VetSystem vetSystem, INavigationService<ManageOwnersViewModel> navManageOwnersService)
        {
            _vetSystem = vetSystem;
            _navManageOwnersService = navManageOwnersService;
            CommandRegisterOwner = new Commands.OwnerRegistratorViewModelCommands.RegisterOwner(_vetSystem, this);
            CommandClearData = new Commands.OwnerRegistratorViewModelCommands.ClearData(this);
            CommandReturn = new NavigateCommand<ManageOwnersViewModel>(_navManageOwnersService);
        }
        #endregion
    }
}
