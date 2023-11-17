using Microsoft.EntityFrameworkCore;
using PawPatientManager.DbContextsFiles;
using PawPatientManager.DTOs;
using PawPatientManager.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PawPatientManager.Services.PetDatabaseActions
{
    public class PetDatabaseHandler : IPetDatabaseHandler
    {
        private DbContentFactory _dbContextFactory;
        public PetDatabaseHandler(DbContentFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public async Task AssignPetToOwner(Owner owner, Pet pet)
        {
            using (MyDbContent dbContext = _dbContextFactory.CreateDbContext())
            {
                Guid ownerID = owner.ID;

                PetDTO petDT = new PetDTO()
                {
                    Name = pet.Name,
                    Gender = pet.Gender,
                    Species = pet.Species,
                    Race = pet.Race,
                    BirthDate = pet.BirthDate,
                    MicrochipNumber = pet.MicrochipNumber,
                    OwnerID = ownerID,
                    Owner = null
                };

                dbContext.Pets.Add(petDT);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task DeletePet(Owner owner, Pet pet)
        {
            using (MyDbContent dbContext = _dbContextFactory.CreateDbContext())
            {
                PetDTO petToDelete = await dbContext.Pets.FindAsync(pet.ID);

                if (petToDelete != null)
                {
                    dbContext.Pets.Remove(petToDelete);

                    await dbContext.SaveChangesAsync();
                }
            }
        }

        public async Task EditPet(Pet pet)
        {
            using (MyDbContent dbContext = _dbContextFactory.CreateDbContext())
            {
                PetDTO petToUpdate = await dbContext.Pets.FindAsync(pet.ID);

                if (petToUpdate != null)
                {
                    petToUpdate.Name = pet.Name;
                    petToUpdate.Gender = pet.Gender;
                    petToUpdate.Species = pet.Species;
                    petToUpdate.Race = pet.Race;
                    petToUpdate.BirthDate = pet.BirthDate;
                    petToUpdate.MicrochipNumber = pet.MicrochipNumber;
                    petToUpdate.OwnerID = petToUpdate.OwnerID;
                    petToUpdate.Owner = petToUpdate.Owner;

                    await dbContext.SaveChangesAsync();
                }
            }
        }

        public async Task<IEnumerable<Pet>> GetAllPetsFromOwner(Owner owner)
        {
            using (MyDbContent dbContext = _dbContextFactory.CreateDbContext())
            {
                var ownerPets = dbContext.Pets
                    .Include(pet => pet.Owner) // Include the Owner navigation property
                    .Where(pet => pet.OwnerID == owner.ID)
                    .ToList();

                OwnerDTO ownerDTO = await dbContext.Owners
                    .Where(x => x.ID == owner.ID).FirstOrDefaultAsync();

                return ownerPets.Select(petDTO => new Pet(petDTO, ownerDTO));
            }
        }

        public async Task<Pet> GetConflictingPet(Owner owner, Pet pet)
        {
            using (MyDbContent dbContext = _dbContextFactory.CreateDbContext())
            {
                // Get owners ID
                Guid ownerID = owner.ID;

                // Check if pet exists
                PetDTO petDT = await dbContext.Pets.Where(x => x.Name == pet.Name).
                    Where(x => x.Gender == pet.Gender).
                    Where(x => x.Species == pet.Species).
                    Where(x => x.Race == pet.Race).
                    Where(x => x.MicrochipNumber == pet.MicrochipNumber).
                    Where(x => x.BirthDate == pet.BirthDate).
                    Where(x => x.OwnerID == ownerID).FirstOrDefaultAsync();

                OwnerDTO ownerDTO = await dbContext.Owners
                    .Where(x => x.ID == owner.ID).FirstOrDefaultAsync();

                if (petDT == null)
                {
                    return null;
                }
                else
                {
                    return new Pet(petDT, ownerDTO);
                }
            }
        }
    }
}
