using Microsoft.EntityFrameworkCore;
using PawPatientManager.DbContextsFiles;
using PawPatientManager.DTOs;
using PawPatientManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawPatientManager.Services.VisitDatabaseActions
{
    public class VisitDatabaseHandler : IVisitDatabaseHandler
    {
        private DbContentFactory _dbContextFactory;
        public VisitDatabaseHandler(DbContentFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public async Task CreateVisit(Vet vet, Pet pet, DateTime dateTime)
        {
            using (MyDbContent dbContext = _dbContextFactory.CreateDbContext())
            {
                VetDTO vetDT = await dbContext.Vets.Where(x => x.ID == vet.ID).FirstOrDefaultAsync();

                if (vetDT == null)
                {
                    return;
                }

                PetDTO petDT = await dbContext.Pets.Where(x => x.ID == pet.ID).FirstOrDefaultAsync();

                if (petDT == null)
                {
                    return;
                }

                VisitDTO visitDTO = new VisitDTO()
                {
                    Pet = petDT,
                    PetID = petDT.ID,
                    Vet = vetDT,
                    VetID = vetDT.ID,
                    Date = dateTime,
                    MedicalReceipts = null,
                };

                dbContext.Visits.Add(visitDTO);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteVisit(Visit visit)
        {
            using (MyDbContent dbContext = _dbContextFactory.CreateDbContext())
            {
                VisitDTO visitDT = await dbContext.Visits.FindAsync(visit.ID);

                if (visitDT != null)
                {
                    dbContext.Visits.Remove(visitDT);

                    await dbContext.SaveChangesAsync();
                }
            }
        }

        public Task EditVisit(Owner selectedowner, Owner editedowner)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Visit>> GetAllVisits()
        {
            using (MyDbContent dbContext = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<VisitDTO> visitDTOs = await dbContext.Visits.ToListAsync();

                List<Visit> visits = new List<Visit>();
                for(int i = 0; i < visitDTOs.Count(); i++)
                {
                    VetDTO vet = await dbContext.Vets.FindAsync(visitDTOs.ElementAt(i).VetID);
                    PetDTO pet = await dbContext.Pets.FindAsync(visitDTOs.ElementAt(i).PetID);
                    OwnerDTO owner = await dbContext.Owners.FindAsync(pet.OwnerID);
                    Visit visit = new Visit(visitDTOs.ElementAt(i), pet, vet, owner);
                    visits.Add(visit);
                }

                return visits;

                //return await visitDTOs.Select(visit => new Visit(visit, await dbContext.Pets.FindAsync(visit.PetID), await dbContext.Vets.FindAsync(visit)));
            }
        }

        public Task<Visit> GetConflictingVisit(Visit visit)
        {
            throw new NotImplementedException();
        }
    }
}
