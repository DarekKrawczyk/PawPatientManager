using PawPatientManager.DTOs;
using PawPatientManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Linq;

namespace PawPatientManager.Models
{
    public class Owner
    {
        private Guid _id;
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
        public Guid ID { get { return _id; } set { _id = value; } }
        public string Name { get { return _name; } set { _name = value; } }
        public string Surname { get { return _surname; } set { _surname = value; } }
        public List<Pet> Pets { get { return _pets; } set { _pets = value; } }
        public bool Gender { get { return _gender; } set { _gender = value; } }
        public DateTime BirthDate { get { return _birthDate; } set { _birthDate = value; } }
        public string Adress { get { return _adress; } set { _adress = value; } }
        public string PhoneNumber { get { return _phoneNumber; } set { _phoneNumber = value; } }
        public string Email { get { return _email; } set { _email = value; } }
        public string PESEL { get { return _pesel; } set { _pesel = value; } }
        //public string Photo { get; set; }
        //public string Notes { get; set; }
        public Owner()
        {
            _pets = new List<Pet>();
        }
        public Owner(OwnerDTO ownerDTO)
        {
            _id = ownerDTO.ID;
            _name = ownerDTO.Name;
            _surname = ownerDTO.Surname;
            _pets = null;
            _gender = ownerDTO.Gender;
            _birthDate = ownerDTO.BirthDate;
            _adress = ownerDTO.Adress;
            _phoneNumber = ownerDTO.PhoneNumber;
            _email = ownerDTO.Email;
            _pesel = ownerDTO.PESEL;
        }
        public Owner(OwnerViewModel ownerVM)
        {
            _id = ownerVM.ID;
            _name = ownerVM.Name;
            _surname = ownerVM.Surname;
            _pets = ownerVM.Pets;
            _gender = ownerVM.Gender;
            _birthDate = ownerVM.BirthDate;
            _adress = ownerVM.Adress;
            _phoneNumber = ownerVM.PhoneNumber;
            _email = ownerVM.Email;
            _pesel = ownerVM.PESEL;
        }
        public Owner(Guid id, string name, string surname, List<Pet> pets)
        {
            _id = id;
            _name = name;
            _surname = surname;
            _pets = pets;
        }
        public Owner(Guid id, string name, string surname, bool gender, DateTime birthDate, List<Pet> pets, string adress, string phoneNumber, string email, string pesel)
        {
            _id = id;
            _name = name;
            _surname = surname;
            _pets = pets;
            _gender = gender;
            _birthDate = birthDate;
            _adress = adress;
            _phoneNumber = phoneNumber;
            _email = email;
            _pesel = pesel;
        }

        // Override == operator
        public static bool operator ==(Owner owner1, Owner owner2)
        {
            // Check for null on both sides.
            if (ReferenceEquals(owner1, null) && ReferenceEquals(owner2, null))
                return true;

            // Check for null on one side only.
            if (ReferenceEquals(owner1, null) || ReferenceEquals(owner2, null))
                return false;

            // Compare individual fields for equality.
            return owner1.ID == owner2.ID &&
                   owner1.Name == owner2.Name &&
                   owner1.Surname == owner2.Surname &&
                   owner1.Gender == owner2.Gender &&
                   owner1.BirthDate == owner2.BirthDate &&
                   owner1.Adress == owner2.Adress &&
                   owner1.PhoneNumber == owner2.PhoneNumber &&
                   owner1.Email == owner2.Email &&
                   owner1.PESEL == owner2.PESEL &&
                   ArePetsEqual(owner1.Pets, owner2.Pets);
        }

        public static bool operator !=(Owner owner1, Owner owner2)
        {
            return !(owner1 == owner2);
        }

        public override bool Equals(object obj)
        {
            if (obj is Owner otherOwner)
            {
                return this == otherOwner;
            }

            return false;
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();
            hash.Add(ID);
            hash.Add(Name);
            hash.Add(Surname);
            hash.Add(Pets);
            hash.Add(Gender);
            hash.Add(BirthDate);
            hash.Add(Adress);
            hash.Add(PhoneNumber);
            hash.Add(Email);
            hash.Add(PESEL);
            return hash.ToHashCode();
        }

        private static bool ArePetsEqual(List<Pet> pets1, List<Pet> pets2)
        {
            return ReferenceEquals(pets1, pets2);
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
