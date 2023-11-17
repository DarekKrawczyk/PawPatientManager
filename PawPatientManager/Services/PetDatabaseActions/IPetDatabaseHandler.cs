using PawPatientManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawPatientManager.Services.PetDatabaseActions
{
    public interface IPetDatabaseHandler
    {
        Task AssignPetToOwner(Owner owner, Pet pet);
        Task DeletePet(Owner owner, Pet pet);
        Task EditPet(Pet pet);
        Task<Pet> GetConflictingPet(Owner owner, Pet pet);
        Task<IEnumerable<Pet>> GetAllPetsFromOwner(Owner owner);
    }
}
