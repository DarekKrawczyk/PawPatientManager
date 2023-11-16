using PawPatientManager.Services.MedicationCreators;
using PawPatientManager.Services.OwnerDatabaseActions;
using PawPatientManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
        #region Fields - Database
        private IMedicationDatabaseHandler _medicationCreator;
        private IOwnerDatabaseHandler _ownerCreator;
        #endregion
        #region Properties
        public List<Owner> Owners { get { return _owners; } set { _owners = value; } }
        public List<Pet> Pets { get { return _pets; } set { _pets = value; } }
        public List<Vet> Vets { get { return _vets; } set { _vets = value; } }
        public List<Visit> Visits { get { return _visits; } set { _visits = value; } }
        public List<Medication> Meds { get { return _meds; } set { _meds = value; } }
        #endregion
        #region Constructor
        public VetSystem(IMedicationDatabaseHandler medicationCreator, IOwnerDatabaseHandler ownerCreator) 
        { 
            _medicationCreator = medicationCreator;
            _ownerCreator = ownerCreator;

            _owners = new List<Owner>();
            _pets = new List<Pet>();
            _vets = new List<Vet>();
            _visits = new List<Visit>();
            _meds = new List<Medication>();
        }
        #endregion
        #region Methods
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
        #region Methods - Database - Meds
        public async Task<IEnumerable<Medication>> GetAllMedicationsAsync()
        {
            return await _medicationCreator.GetAllMedications();
        }
        public async Task AddMedication(Medication medication)
        { 
            Medication conflictableMedication = await _medicationCreator.GetConflictingMedication(medication);

            if(conflictableMedication != null)
            {
                MessageBox.Show("AddMedication() - there was conflict whil adding medication");
            }

            else await _medicationCreator.CreateMedication(medication);
        }
        public async Task DeleteMedication(Medication medication)
        {
            Medication conflictableMedication = await _medicationCreator.GetConflictingMedication(medication);

            if (conflictableMedication == null)
            {
                MessageBox.Show("DeleteMedication() - there is no such medication!");
            }
            else await _medicationCreator.DeleteMedication(medication);
        }
        public async Task EditMedication(Medication selected, Medication edited)
        {
            Medication conflictableMedication = await _medicationCreator.GetConflictingMedication(selected);

            if (conflictableMedication == null)
            {
                MessageBox.Show("EditMedication() - medication wasnt found");
            }
            else await _medicationCreator.EditMedication(selected, edited);
        }
        #endregion
        #region Methods - Database - Owner
        public async Task<IEnumerable<Owner>> GetAllOwnersAsync()
        {
            return await _ownerCreator.GetAllOwners();
        }
        public async Task AddOwner(Owner owner)
        {
            Owner conflictableOwner = await _ownerCreator.GetConflictingOwner(owner);

            if (conflictableOwner != null)
            {
                MessageBox.Show("AddOwner() - there was conflict whil adding owner");
            }

            else await _ownerCreator.CreateOwner(owner);
        }
        public async Task EditOwner(Owner selected, Owner edited)
        {
            Owner conflictableOwner = await _ownerCreator.GetConflictingOwner(selected);

            if (conflictableOwner == null)
            {
                MessageBox.Show("EditOwner() - medication wasnt found");
            }
            else await _ownerCreator.EditOwner(selected, edited);
        }
        public async Task DeleteOwner(Owner owner)
        {
            Owner conflictableOwner = await _ownerCreator.GetConflictingOwner(owner);

            if (conflictableOwner == null)
            {
                MessageBox.Show("DeleteOwner() - there is no such owner!");
            }
            else await _ownerCreator.DeleteOwner(conflictableOwner);
        }
        #endregion
    }
}
