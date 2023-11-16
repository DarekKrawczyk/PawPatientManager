using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using PawPatientManager.DbContexts;
using PawPatientManager.DbContextsFiles;
using PawPatientManager.DTOs;
using PawPatientManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PawPatientManager.Commands.MedsCommands;

namespace PawPatientManager.Services.OwnerDatabaseActions
{
    public class OwnerDatabaseHandler : IOwnerDatabaseHandler
    {
        private DbContentFactory _dbContextFactory;
        public OwnerDatabaseHandler(DbContentFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public async Task CreateOwner(Owner owner)
        {
            using (MyDbContent dbContext = _dbContextFactory.CreateDbContext())
            {
                OwnerDTO ownerDTO = new OwnerDTO()
                {
                    Name = owner.Name,
                    Surname = owner.Surname,
                    Gender =owner.Gender,
                    BirthDate =owner.BirthDate,
                    Adress =owner.Adress,
                    PhoneNumber = owner.PhoneNumber,
                    Email =owner.Email,
                    PESEL=owner.PESEL
                };

                dbContext.Owners.Add(ownerDTO);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteOwner(Owner owner)
        {
            using (MyDbContent dbContext = _dbContextFactory.CreateDbContext())
            {
                OwnerDTO ownerDTO = await dbContext.Owners.FindAsync(owner.ID);

                if (ownerDTO != null)
                {
                    dbContext.Owners.Remove(ownerDTO);

                    await dbContext.SaveChangesAsync();
                }
            }
        }

        public async Task EditOwner(Owner selectedowner, Owner editedowner)
        {
            using (MyDbContent dbContext = _dbContextFactory.CreateDbContext())
            {
                OwnerDTO medicationToUpdate = await dbContext.Owners.FindAsync(selectedowner.ID);

                if (medicationToUpdate != null)
                {
                    medicationToUpdate.Name = editedowner.Name;
                    medicationToUpdate.Surname = editedowner.Surname;
                    medicationToUpdate.Gender = editedowner.Gender;
                    medicationToUpdate.BirthDate = editedowner.BirthDate;
                    medicationToUpdate.Adress = editedowner.Adress;
                    medicationToUpdate.PhoneNumber = editedowner.PhoneNumber;
                    medicationToUpdate.Email = editedowner.Email;
                    medicationToUpdate.PESEL = editedowner.PESEL;

                    await dbContext.SaveChangesAsync();
                }
            }
        }

        //public async Task<IEnumerable<Owner>> GetAllOwners()
        //{
        //    using (MyDbContent dbContext = _dbContextFactory.CreateDbContext())
        //    {
        //        IEnumerable<OwnerDTO> ownerDTOs = await dbContext.Owners.ToListAsync();
        //        //DARIKRA -> Czy ten cast do List<Pet> jest ok?

        //        List<Owner> owners = new List<Owner>();
        //        for(int i = 0; i < ownerDTOs.Count(); i++)
        //        {

        //            List<Pet> pets = new List<Pet>();
        //            for(int j = 0; j < ownerDTOs.ElementAt(i).Pets.Count; j++)
        //            {
        //                pets.Add(new Pet())
        //            }
        //            Owner temp = new Owner(
        //            owners.Add()
        //        }

        //        return ownerDTOs.Select(x => new Owner(x.ID, x.Name, x.Surname, x.Gender, x.BirthDate, new List<Pet>() { x.Pets },
        //                x.Adress, x.PhoneNumber, x.Email, x.PESEL));
        //    }
        //}        
        
        public async Task<IEnumerable<Owner>> GetAllOwners()
        {
            using (MyDbContent dbContext = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<OwnerDTO> ownerDTOs = await dbContext.Owners
                    .Include(owner => owner.Pets) // Include the Pets navigation property
                    .ToListAsync();

                return ownerDTOs.Select(ownerDTO => new Owner
                {
                    ID = ownerDTO.ID,
                    Name = ownerDTO.Name,
                    Surname = ownerDTO.Surname,
                    Pets = null,
                    Gender = ownerDTO.Gender,
                    BirthDate = ownerDTO.BirthDate,
                    Adress = ownerDTO.Adress,
                    PhoneNumber = ownerDTO.PhoneNumber,
                    Email = ownerDTO.Email,
                    PESEL = ownerDTO.PESEL
                });
            }
        }

        public async Task<Owner> GetConflictingOwner(Owner owner)
        {
            using (MyDbContent dbContext = _dbContextFactory.CreateDbContext())
            {
                OwnerDTO ownerDTO = await dbContext.Owners.Where(x => x.Name == owner.Name).
                    Where(x => x.Surname == owner.Surname).
                    Where(x => x.Gender == owner.Gender).
                    Where(x => x.BirthDate == owner.BirthDate).
                    Where(x => x.Adress == owner.Adress).
                    Where(x => x.PhoneNumber == owner.PhoneNumber).
                    Where(x => x.Email == owner.Email).
                    Where(x => x.PESEL == owner.PESEL).FirstOrDefaultAsync();

                if (ownerDTO == null)
                {
                    return null;
                }
                else
                {
                    return new Owner(ownerDTO.ID, ownerDTO.Name, ownerDTO.Surname, ownerDTO.Gender, ownerDTO.BirthDate, null,
                        ownerDTO.Adress, ownerDTO.PhoneNumber, ownerDTO.Email, ownerDTO.PESEL);
                }

            }
        }
    }
}
