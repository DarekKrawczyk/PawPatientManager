using PawPatientManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PawPatientManager.Models
{
    public class VetSystem
    {
        #region Fields
        private List<Owner> _owners;
        private List<Pet> _pets;
        private List<Vet> _vets;
        private List<Visit> _visits;
        private List<Medication> _meds;
        #endregion
        #region Properties
        public List<Owner> Owners { get { return _owners; } set { _owners = value; } }
        public List<Pet> Pets { get { return _pets; } set { _pets = value; } }
        public List<Vet> Vets { get { return _vets; } set { _vets = value; } }
        public List<Visit> Visits { get { return _visits; } set { _visits = value; } }
        public List<Medication> Meds { get { return _meds; } set { _meds = value; } }
        #endregion
        #region Constructor
        public VetSystem() 
        { 
            _owners = new List<Owner>();
            _pets = new List<Pet>();
            _vets = new List<Vet>();
            _visits = new List<Visit>();
            _meds = new List<Medication>();
        }
        #endregion
        #region Methods
        public void AddOwner(Owner owner)
        {
            _owners.Add(owner);
        }

        public void AddMed(Medication med)
        {
            _meds.Add(med);
        }

        public void AddVisit(Visit visit)
        {
            _visits.Add(visit);
        }

        public void AddPetToOwner(Pet pet)
        {
            _pets.Add(pet);
            for(int i = 0; i<_owners.Count; i++)
            {
                if (_owners[i] == pet.Owner)
                {
                    _owners[i].AddPet(pet);
                }
            }
        }

        public void EditOwner(Owner owner)
        {
            for(int i =0; i < _owners.Count; i++)
            {
                if (_owners[i].ID == owner.ID)
                {
                    _owners[i] = owner;
                }
            }
        }

        public void DeleteMed(Medication med)
        {
            for(int i = _meds.Count - 1; i >= 0 ; i--)
            {
                if (_meds[i].ID == med.ID)
                {
                    _meds.RemoveAt(i);
                    break;
                }
            }
        }

        public void EditMed(Medication med)
        {
            for (int i = 0; i < _meds.Count; i++)
            {
                if (_meds[i].ID == med.ID)
                {
                    _meds[i] = med;
                }
            }
        }

        public void EditVisit(Visit visit)
        {
            for (int i = 0; i < _visits.Count; i++)
            {
                if (_visits[i].ID == visit.ID)
                {
                    _visits[i] = visit;
                }
            }
        }

        public void EditPet(Pet pet)
        {
            for(int i = 0; i < _pets.Count ; i++)
            {
                if (_pets[i].ID == pet.ID)
                {
                    _pets[i] = pet;
                }
            }
        }
        #endregion
    }
}
