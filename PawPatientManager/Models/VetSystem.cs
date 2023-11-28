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

            // Create default account
            //_vetCreator.CreateVet(new Vet(new Guid(), "Dr. Dariusz", "Krawczyk", "darikra473", "darikra473", null));
            
            //_vetCreator.DeleteAllVets();

            //PopulateMeds();
            //PopulateOwners();
            //PopulatePets(100);
            //PopulateVets();

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
        public async Task DeleteAllVets()
        {
            await _vetCreator.DeleteAllVets();
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
        public async Task<Vet> LoginVet(string login, string password)
        {
            return await _vetCreator.LoginVet(login, password);
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
        public async Task<IEnumerable<Visit>> GetAllVisitsFromVetAsync(Vet vet)
        {
            return await _visitCreator.GetAllVisits(vet, DateTime.Now);
        }
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
        public async Task EditVisit(Visit selectedVisit, Visit editedVisit, Guid newVetID, Guid newPetID)
        {
            await _visitCreator.EditVisit(selectedVisit, editedVisit, newVetID, newPetID);
        }
        public async Task AddReceiptToVisit(Visit visit, MedicalReceipt receipt)
        {
            await _visitCreator.AddMedicalReceipt(visit, receipt);
        }
        #endregion
        #region Methods - Database
        private async void PopulateMeds()
        {
            string[] names = { "PawRelief", "FurGuard", "Meowlixir", "TailEase", "HoofHeal", "FeatherFix", "ScaleSoothe" };
            string[] descriptions = { "Relieves joint pain", "Promotes shiny coat", "Soothes digestive issues", "Boosts immune system", "Heals minor wounds" };
           
            List<Medication> medsToPopulate = new List<Medication>();

            for (int i = 0; i < 100; i++)
            {
                Guid id = Guid.NewGuid();
                string name = names[new Random().Next(names.Length)];
                string description = descriptions[new Random().Next(descriptions.Length)];
                int amount = new Random().Next(1, 101);

                medsToPopulate.Add(new Medication(id, name, description, amount));
            }

            foreach(Medication med in medsToPopulate)
            {
                await _medicationCreator.CreateMedication(med);
            }
        }
        private async void PopulateOwners()
        {
            string[] names = { "John", "Jane", "Alice", "Bob", "Eva", "Michael", "Sophia" };
            string[] surnames = { "Smith", "Johnson", "Williams", "Jones", "Brown", "Davis", "Miller" };
            string[] addresses = { "123 Main St", "456 Oak Ave", "789 Elm Blvd", "101 Pine Ln", "202 Cedar Rd" };

            List<Owner> ownersToPopulate = new List<Owner>();
            Random random = new Random();

            for (int i = 0; i < 20; i++)
            {
                Guid id = Guid.NewGuid();
                string name = names[random.Next(names.Length)];
                string surname = surnames[random.Next(surnames.Length)];
                bool gender = random.Next(2) == 0; // Randomly assign gender
                DateTime birthDate = DateTime.Now.AddYears(-random.Next(30, 60));
                string address = addresses[random.Next(addresses.Length)];
                string phoneNumber = $"(+{random.Next(1, 100)}) {random.Next(100000000, 999999999)}";
                string email = $"{name.ToLower()}.{surname.ToLower()}@example.com";
                string pesel = GenerateRandomPesel();

                ownersToPopulate.Add(new Owner(id, name, surname, gender, birthDate, null, address, phoneNumber, email, pesel));
            }

            foreach (Owner owner in ownersToPopulate)
            {
                await _ownerCreator.CreateOwner(owner);
                //foreach(Pet pet in owner.Pets)
                //{
                //    await _petCreator.AssignPetToOwner(owner, pet);
                //}
            }

        }
        private async void PopulateVets()
        {
            string[] vetNames = { "Dr. Adam", "Dr. Pope", "Dr. Max", "Dr. Bently", "Dr. Chris", "Dr. Trevor", "Dr. Amanda" };
            List<string> vetSurnames = new List<string>
                {
                    "Smith",
                    "Johnson",
                    "Williams",
                    "Jones",
                    "Brown",
                    "Davis",
                    "Miller",
                    "Wilson",
                    "Moore",
                    "Taylor",
                    "Anderson",
                    "Thomas",
                    "Jackson",
                    "White",
                    "Harris",
                    "Martin",
                    "Thompson",
                    "Garcia",
                    "Martinez",
                    "Davis"
                };
            List<Vet> vetsToPopulate = new List<Vet>();
            Random random = new Random();

            for (int i = 0; i < 10; i++)
            {
                Guid id = Guid.NewGuid();
                string name = vetNames[random.Next(vetNames.Length)];
                string surname = vetSurnames[random.Next(vetSurnames.Count)];
                string login = GenerateNickname(name);
                string password = surname;

                vetsToPopulate.Add(new Vet(id, name, surname, login, password));

                MessageBox.Show($"Login: {login}\nPassword: {password}", "PawPatientManager", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            foreach (Vet vet in vetsToPopulate)
            {
                await _vetCreator.CreateVet(vet);
            }
        }
        public string GenerateNickname(string inputString)
        {
            // Split the input string into words
            string[] words = inputString.Split(' ');

            // Use the first word as the basis for the nickname
            string nickname = words.Length > 0 ? words[0] : "";

            // Add the first letter of each subsequent word to the nickname
            for (int i = 1; i < words.Length; i++)
            {
                if (!string.IsNullOrWhiteSpace(words[i]))
                {
                    nickname += char.ToUpper(words[i][0]);
                }
            }

            return nickname;
        }
        public async void PopulatePets(int numberOfPets)
        {
            string[] names = { "Bella", "Max", "Charlie", "Lucy", "Cooper", "Luna", "Rocky" };
            string[] speciesList = { "Dog", "Cat", "Bird", "Rabbit", "Fish" };
            string[] races = { "Labrador", "Siamese", "Parrot", "Dwarf Rabbit", "Goldfish" };

            List<Pet> petsToPopulate = new List<Pet>();
            Random random = new Random();

            IEnumerable<Owner> owners = await GetAllOwnersAsync();

            for (int i = 0; i < numberOfPets; i++)
            {
                Guid id = Guid.NewGuid();
                string name = names[random.Next(names.Length)];
                bool gender = random.Next(2) == 0; // Randomly assign gender
                Owner owner = owners.ElementAt(random.Next(owners.Count()));
                DateTime birthDate = DateTime.Now.AddMonths(-random.Next(1, 120));
                List<Visit> visits = null; // You can modify this if you want to populate visits later
                List<MedicalReceipt> medicals = null; // You can modify this if you want to populate medicals later
                string species = speciesList[random.Next(speciesList.Length)];
                string race = races[random.Next(races.Length)];
                string microchipNumber = GenerateRandomMicrochipNumber();

                Pet pet = new Pet(id, name, gender, owner, birthDate, visits, medicals, species, race, microchipNumber);

                AssignPetToOwner(owner, pet);
            }

            //return petsToPopulate;
        }

        public static string GenerateRandomMicrochipNumber()
        {
            Random random = new Random();
            StringBuilder microchipNumberBuilder = new StringBuilder();

            // Add random digits for microchip number
            for (int i = 0; i < 15; i++)
            {
                microchipNumberBuilder.Append(random.Next(10));
            }

            return microchipNumberBuilder.ToString();
        }
        public static string GenerateRandomPesel()
        {
            Random random = new Random();
            StringBuilder peselBuilder = new StringBuilder();

            for (int i = 0; i < 10; i++)
            {
                peselBuilder.Append(random.Next(10));
            }

            int[] weights = { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3, 1 };
            int controlSum = 0;

            for (int i = 0; i < 10; i++)
            {
                controlSum += weights[i] * (peselBuilder[i] - '0');
            }

            int controlDigit = (10 - (controlSum % 10)) % 10;
            peselBuilder.Append(controlDigit);

            return peselBuilder.ToString();
        }
        #endregion
    }
}
