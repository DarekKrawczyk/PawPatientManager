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
        #endregion
        #region Properties
        public List<Owner> Owners { get { return _owners; } set { _owners = value; } }
        public List<Pet> Pets { get { return _pets; } set { _pets = value; } }
        public List<Vet> Vets { get { return _vets; } set { _vets = value; } }
        public List<Visit> Visits { get { return _visits; } set { _visits = value; } }
        #endregion
        #region Constructor
        public VetSystem() 
        { 
            _owners = new List<Owner>();
            _pets = new List<Pet>();
            _vets = new List<Vet>();
            _visits = new List<Visit>();
        }
        #endregion
        #region Methods
        public void AddOwner(Owner owner)
        {
            _owners.Add(owner);
        }
        public void AssignPetToOwner(Owner owner, Pet pet)
        {
            throw new NotImplementedException();
            //_pets.Add(pet);
        }
        // TODO:
        //Pet pet = new Pet(0, "Szarik", true, pioter, DateTime.Now, "Pies", "Owczarek Niemiecki", "997");
        //pioter.AddPet(pet);

        //Vet lekarz = new Vet(0, "Jarek", "Lekarek");
        //Medication xanax = new Medication(0, "Xanax", "Healing strong!");
        //Visit wizyta = new Visit(0, pet, lekarz, DateTime.Now, new List<Medication>() { xanax });
        #endregion
    }
}
