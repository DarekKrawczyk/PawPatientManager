using Microsoft.EntityFrameworkCore;
using PawPatientManager.DbContextsFiles;
using PawPatientManager.DTOs;
using PawPatientManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace PawPatientManager.Services.VisitDatabaseActions
{
    public class VisitDatabaseHandler : IVisitDatabaseHandler
    {
        private DbContentFactory _dbContextFactory;
        public VisitDatabaseHandler(DbContentFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task AddMedicalReceipt(Visit visit, MedicalReceipt receipt)
        {
            using (MyDbContent dbContext = _dbContextFactory.CreateDbContext())
            {

                MedicalReceiptDTO medDT = new MedicalReceiptDTO()
                {
                    VisitID = visit.ID,
                    Signed = receipt.Signed,
                    Recommendation = receipt.Recommendation,
                };

                dbContext.MedicalReceipts.Add(medDT);
                await dbContext.SaveChangesAsync();
            }
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

        public async Task EditVisit(Visit selectedVisit, Visit editedVisit, Guid newVetGUID, Guid newPetGUID)
        {
            using (MyDbContent dbContext = _dbContextFactory.CreateDbContext())
            {
                VisitDTO visitToUpdate = await dbContext.Visits.FindAsync(selectedVisit.ID);

                if (visitToUpdate != null)
                {
                    visitToUpdate.PetID = (newPetGUID==visitToUpdate.PetID)?(visitToUpdate.PetID):(newPetGUID);
                    visitToUpdate.VetID = (newVetGUID== visitToUpdate.VetID) ? (visitToUpdate.VetID) : (newVetGUID);
                    visitToUpdate.Date = editedVisit.Date;
                    visitToUpdate.MedicalReceipts = visitToUpdate.MedicalReceipts;

                    await dbContext.SaveChangesAsync();
                }
            }
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
        public async Task<IEnumerable<Visit>> GetAllVisits(Vet vet, DateTime dateTime)
        {
            using (MyDbContent dbContext = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<VisitDTO> visitDTOs = await dbContext.Visits.Where(x=>x.VetID == vet.ID).Where(x=>x.Date>=dateTime).ToListAsync();

                List<Visit> visits = new List<Visit>();
                for (int i = 0; i < visitDTOs.Count(); i++)
                {
                    VetDTO vett = await dbContext.Vets.FindAsync(visitDTOs.ElementAt(i).VetID);
                    PetDTO pet = await dbContext.Pets.FindAsync(visitDTOs.ElementAt(i).PetID);
                    OwnerDTO owner = await dbContext.Owners.FindAsync(pet.OwnerID);
                    Visit visit = new Visit(visitDTOs.ElementAt(i), pet, vett, owner);

                    IEnumerable<MedicalReceiptDTO> meds = await dbContext.MedicalReceipts.ToListAsync();

                    List<MedicalReceipt> filtred = new List<MedicalReceipt>();
                    foreach(MedicalReceiptDTO med in meds)
                    {
                        if(med.VisitID == visit.ID)
                        {
                            filtred.Add(new MedicalReceipt(med));
                        }
                    }

                    visit.MedicalReceipts = filtred;

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
