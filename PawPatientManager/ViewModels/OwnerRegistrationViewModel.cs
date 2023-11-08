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

        #region Representation of "View" fields
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

        public string Name { get { return _name; } set { _name = value; OnPropertyChanged(nameof(_name)); } }
        public string Surname { get { return _surname; } set { _surname = value; OnPropertyChanged(nameof(_surname)); } }
        public bool Gender { get { return _gender; } set { _gender = value; OnPropertyChanged(nameof(_gender)); } }
        public DateTime BirthDate { get { return _birthDate; } set { _birthDate = value; OnPropertyChanged(nameof(_birthDate)); } }
        public string Adress { get { return _adress; } set { _adress = value; OnPropertyChanged(nameof(_adress)); } }
        public string PhoneNumber { get { return _phoneNumber; } set { _phoneNumber = value; OnPropertyChanged(nameof(_phoneNumber)); } }
        public string Email { get { return _email; } set { _email = value; OnPropertyChanged(nameof(_email)); } }
        public string PESEL { get { return _pesel; } set { _pesel = value; OnPropertyChanged(nameof(_pesel)); } }
        #endregion
        #region Commands
        /* These are commands used in .xaml, for example *CommandRegsiterOwner* is a command for button click of 
         * *Register Owner* button. They have to have binding like properties!!!!
         */
        public ICommand CommandRegisterOwner{ get; }
        #endregion
        #region Constructor
        public OwnerRegistrationViewModel()
        {

        }
        #endregion
    }
}
