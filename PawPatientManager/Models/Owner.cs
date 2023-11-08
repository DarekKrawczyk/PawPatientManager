using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawPatientManager.Models
{
    public class Owner
    {
        private uint _id;
        private string _name;
        private string _surname;
        private List<Pet> _pets;
        private bool _gender;
        private DateTime _birthDate;
        private string _adress;
        private string _phoneNumber;
        private string _email;
        private string _pesel;
        //private string _photo;
        //private string _notes;
        public uint Id { get { return _id; } set { _id = value; } }
        public string Name { get { return _name; } set { _name = value; } }
        public string Surname { get { return _surname; } set { _surname = value; } }
        public List<Pet> Pets { get { return _pets; } set { _pets = value; } }
        public bool Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string Adress { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string PESEL { get; set; }
        //public string Photo { get; set; }
        //public string Notes { get; set; }
        public Owner(uint id, string name, string surname, List<Pet> pets)
        {
            _id = id;
            _name = name;
            _surname = surname;
            _pets = pets;
        }
        public Owner(uint id, string name, string surname, bool gender, DateTime birthDate, string adress, string email, string pesel)
        {
            _id = id;
            _name = name;
            _surname = surname;
            _pets = new List<Pet>();
            _gender = gender;
            _birthDate = birthDate;
            _adress = adress;
            _email = email;
            _pesel = pesel;
        }

        public void AddPet(Pet pet)
        {
            _pets.Add(pet);
        }

        /*
        public bool Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string Adress {  get; set; }
        public string PhoneNumber {  get; set; }
        public string Email {  get; set; }
        public string PESEL { get; set; }
        public string Photo { get; set; }
        public string Notes { get; set; }
        */
    }
}
