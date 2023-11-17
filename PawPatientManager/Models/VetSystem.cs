using PawPatientManager.Services.MedicationCreators;
using PawPatientManager.Services.OwnerDatabaseActions;
using PawPatientManager.Services.PetDatabaseActions;
using PawPatientManager.Services.VetDatabaseActions;
using PawPatientManager.Services.VisitDatabaseActions;
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
        private IPetDatabaseHandler _petCreator;
        private IVetDatabaseHandler _vetCreator;
        private IVisitDatabaseHandler _visitCreator;
        #endregion
        #region Properties
        public List<Owner> Owners { get { return _owners; } set { _owners = value; } }
        public List<Pet> Pets { get { return _pets; } set { _pets = value; } }
        public List<Vet> Vets { get { return _vets; } set { _vets = value; } }
        public List<Visit> Visits { get { return _visits; } set { _visits = value; } }
        public List<Medication> Meds { get { return _meds; } set { _meds = value; } }
        #endregion
        #region Constructor
        public VetSystem(IMedicationDatabaseHandler medicationCreator, IOwnerDatabaseHandler ownerCreator, IPetDatabaseHandler petCreator,
            IVetDatabaseHandler vetCreator, IVisitDatabaseHandler visitCreator) 
        { 
            _medicationCreator = medicationCreator;
            _ownerCreator = ownerCreator;
            _petCreator = petCreator;
            _vetCreator = vetCreator;
            _visitCreator = visitCreator;

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
        #region Methods - Database - Pet
        public async Task AssignPetToOwner(Owner owner, Pet pet)
        {
            Owner conflictableOwner = await _ownerCreator.GetConflictingOwner(owner);

            if (conflictableOwner == null)
            {
                MessageBox.Show("AssignPetToOwner() - there is no such owner!");
                return;
            }
            //await _ownerCreator.DeleteOwner(conflictableOwner);

            Pet conflictablePet = await _petCreator.GetConflictingPet(owner, pet);
            if(conflictablePet != null)
            {
                MessageBox.Show("DeleteOwner() - there was conflict whil assinging pet to owner!");
            }
            else await _petCreator.AssignPetToOwner(owner, pet);
        }
        public async Task<IEnumerable<Pet>> GetAllPetsFromOwner(Owner owner)
        {
            Owner conflictableOwner = await _ownerCreator.GetConflictingOwner(owner);

            if (conflictableOwner == null)
            {
                MessageBox.Show("GetAllPetsFromOwner() - there is no such owner!");
                return null;
            }
            //await _ownerCreator.DeleteOwner(conflictableOwner);

            IEnumerable<Pet> pets = await _petCreator.GetAllPetsFromOwner(owner);
            if (pets == null)
            {
                MessageBox.Show("GetAllPetsFromOwner() - there was conflict whil geting all oets from this owner!");
                return null;
            }
            else return pets;
        }
        public async Task<IEnumerable<Pet>> GetAllPetsFromAllOwners()
        {
            IEnumerable<Owner> allOwners = await GetAllOwnersAsync();

            if (allOwners == null)
            {
                MessageBox.Show("GetAllPetsFromAllOwners() - there was error getting all owners");
                return null;
            }

            List<Pet> allPets = new List<Pet>();
            foreach(Owner owner in allOwners)
            {
                IEnumerable<Pet> ownerPets = await GetAllPetsFromOwner(owner);
                allPets.AddRange(ownerPets);
            }

            if (allPets == null)
            {
                MessageBox.Show("GetAllPetsFromOwner() - there was conflict whil geting all oets from all owners!");
                return null;
            }
            else return allPets;
        }
        public async Task DeletePet(Owner owner, Pet pet)
        {
            Pet conflictablePet = await _petCreator.GetConflictingPet(owner, pet);
            Owner conflictableOwner = await _ownerCreator.GetConflictingOwner(owner);
            
            if(conflictablePet == null && conflictableOwner == null)
            {
                MessageBox.Show("DeleteOwner() - there is no such owner and pet!");
            }
            else if (conflictableOwner == null)
            {
                MessageBox.Show("DeleteOwner() - there is no such owner!");
            } 
            else if(conflictablePet == null)
            {
                MessageBox.Show("DeleteOwner() - there is no such pet!");
            }
            else await _petCreator.DeletePet(owner, pet);
        }
        public async Task EditPet(Pet pet)
        {
            //Pet conflictablePet = await _petCreator.GetConflictingPet(pet);

            //if (conflictablePet == null)
            //{
            //    MessageBox.Show("EditPet() - pet wasnt found");
            //}
            await _petCreator.EditPet(pet);
        }
        #endregion
        #region Methods - Database - Vets
        public async Task<IEnumerable<Vet>> GetAllVetsAsync()
        {
            return await _vetCreator.GetAllVets();
        }
        public async Task AddVet(Vet vet)
        {
            Vet conflictableVet = await _vetCreator.GetConflictingVet(vet);

            if (conflictableVet != null)
            {
                MessageBox.Show("AddVet() - there was conflict whil adding vet");
            }

            else await _vetCreator.CreateVet(vet);
        }
        public async Task DeleteVet(Vet vet)
        {
            Vet conflictableVet = await _vetCreator.GetConflictingVet(vet);

            if (conflictableVet == null)
            {
                MessageBox.Show("DeleteVet() - there is no such Vet!");
            }
            else await _vetCreator.DeleteVet(conflictableVet);
        }
        #endregion
        #region Methods - Database - Visits
        public async Task<IEnumerable<Visit>> GetAllVisitsAsync()
        {
            return await _visitCreator.GetAllVisits();
        }
        public async Task AddVisit(Vet vet, Pet pet, DateTime date)
        {
            await _visitCreator.CreateVisit(vet, pet, date);
        }
        public async Task DeleteVisit(Visit visit)
        {
            await _visitCreator.DeleteVisit(visit);
        }
        #endregion

    }
}
