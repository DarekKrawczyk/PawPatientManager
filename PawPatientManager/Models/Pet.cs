﻿using PawPatientManager.DTOs;
using PawPatientManager.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PawPatientManager.Models
{
    public class Pet
    {
        private Guid _id;
        private string _name;
        private bool _gender;
        private string _species;
        private string _race;
        private string _microchipNumber;
        private Owner _owner;
        private DateTime _birthDate;
        private List<Visit> _visits;
        private List<MedicalReceipt> _medicals;
        public Guid ID { get { return _id; } set { _id = value; } }
        public string Name { get { return _name; } set { _name = value; } }
        public bool Gender { get { return _gender; } set { _gender = value; } }
        public string Species { get { return _species; } set { _species = value; } }
        public string Race { get { return _race; } set { _race = value; } }
        public string MicrochipNumber { get { return _microchipNumber; } set { _microchipNumber = value; } }
        public Owner Owner { get { return _owner; } set { _owner = value; } }
        public DateTime BirthDate { get { return _birthDate; } set { _birthDate = value; } }
        public List<Visit> Visits { get { return _visits; } set { _visits = value; } }
        public List<MedicalReceipt> Medicals { get { return _medicals; } set { _medicals = value; } }
        public Pet(PetViewModel petVM)
        {
            _id = petVM.ID;
            _name = petVM.Name;
            _gender = petVM.Gender;
            _owner = petVM.Owner;
            _birthDate = petVM.BirthDate;
            _visits = petVM.Visits;
            _medicals = petVM.Medicals;
            _species = petVM.Species;
            _race = petVM.Race;
            _microchipNumber = petVM.MicrochipNumber;
        }
        public Pet(Guid id, string name, bool gender, Owner owner, DateTime dateTime, List<Visit> visits, List<MedicalReceipt> medicals, string species, string race, string microchipnumber)
        {
            _id = id;
            _name = name;
            _gender = gender;
            _owner = owner;
            _birthDate = dateTime;
            _visits = visits;
            _medicals = medicals;
            _species = species;
            _race = race;
            _microchipNumber = microchipnumber;
        }
        public Pet(PetDTO petDTO, OwnerDTO ownerDTO) 
        {
            _id = petDTO.ID;
            _name = petDTO.Name;
            _gender = petDTO.Gender;
            _owner = (ownerDTO != null) ? new Owner(ownerDTO) : null;
            _birthDate = petDTO.BirthDate;
            _visits = null;
            _species = petDTO.Species;
            _race = petDTO.Race;
            _microchipNumber = petDTO.MicrochipNumber;
        }
        public Pet(Guid id, string name, bool gender, Owner owner, DateTime dateTime, string species, string race, string microchipnumber)
        {
            _id = id;
            _name = name;
            _gender = gender;
            _owner = owner;
            _birthDate = dateTime;
            _visits = new List<Visit>();
            _medicals = new List<MedicalReceipt>();
            _species = species;
            _race = race;
            _microchipNumber = microchipnumber;
    }
        /*
        private string _photo;
        private string _informacjeZdrowotne; //Pole na przechowywanie informacji o stanie zdrowia, szczepieniach, leczeniach i historii chorób zwierzęcia.
        */


    }
}
