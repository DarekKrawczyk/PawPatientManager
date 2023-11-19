using Microsoft.EntityFrameworkCore;
using PawPatientManager.DbContextsFiles;
using PawPatientManager.DTOs;
using PawPatientManager.Models;
using PawPatientManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawPatientManager.Services.VetDatabaseActions
{
    public class VetDatabaseHandler : IVetDatabaseHandler
    {
        private DbContentFactory _dbContextFactory;
        public VetDatabaseHandler(DbContentFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task CreateVet(Vet vet)
        {
            using (MyDbContent dbContext = _dbContextFactory.CreateDbContext())
            {
                VetDTO vetDT = new VetDTO()
                {
                    Name = vet.Name,
                    Surname = vet.Surname,
                    Login = vet.Login,
                    Password = vet.Password,
                    Visits = null
                };

                dbContext.Vets.Add(vetDT);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteVet(Vet vet)
        {
            using (MyDbContent dbContext = _dbContextFactory.CreateDbContext())
            {
                VetDTO vetDT = await dbContext.Vets.FindAsync(vet.ID);

                if (vetDT != null)
                {
                    dbContext.Vets.Remove(vetDT);

                    await dbContext.SaveChangesAsync();
                }
            }
        }

        public async Task<IEnumerable<Vet>> GetAllVets()
        {
            using (MyDbContent dbContext = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<VetDTO> vetsDTOs = await dbContext.Vets.ToListAsync();

                return vetsDTOs.Select(vet => new Vet(vet));
            }
        }

        public async Task<Vet> GetConflictingVet(Vet vet)
        {
            using (MyDbContent dbContext = _dbContextFactory.CreateDbContext())
            {
                VetDTO vetDT = await dbContext.Vets.Where(x => x.Name == vet.Name).
                    Where(x => x.Surname == vet.Surname).FirstOrDefaultAsync();

                if (vetDT == null)
                {
                    return null;
                }
                else
                {
                    return new Vet(vetDT);
                }

            }
        }

        public async Task<Vet> LoginVet(string login, string password)
        {
            using (MyDbContent dbContext = _dbContextFactory.CreateDbContext())
            {
                VetDTO vetDT = await dbContext.Vets.Where(x => x.Login == login).
                    Where(x => x.Password == password).FirstOrDefaultAsync();

                if (vetDT == null)
                {
                    return null;
                }
                else
                {
                    return new Vet(vetDT);
                }

            }
        }
    }
}

/*
 * 
 * 
 *             using (MyDbContent dbContext = _dbContextFactory.CreateDbContext())
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
 * 
 * 
 */