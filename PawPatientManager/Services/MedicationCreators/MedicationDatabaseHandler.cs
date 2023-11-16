using Microsoft.EntityFrameworkCore;
using PawPatientManager.DbContexts;
using PawPatientManager.DbContextsFiles;
using PawPatientManager.DTOs;
using PawPatientManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawPatientManager.Services.MedicationCreators
{
    public class MedicationDatabaseHandler : IMedicationDatabaseHandler
    {
        private DbContentFactory _dbContextFactory;
        public MedicationDatabaseHandler(DbContentFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public async Task CreateMedication(Medication med)
        {
            using (MyDbContent dbContext = _dbContextFactory.CreateDbContext())
            {
                MedicationDTO medicationDto = new MedicationDTO()
                {
                    Name = med.Name,
                    Description = med.Description,
                    Amount = med?.Amount ?? 1
                };

                dbContext.Medications.Add(medicationDto);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteMedication(Medication med)
        {
            using (MyDbContent dbContext = _dbContextFactory.CreateDbContext())
            {
                MedicationDTO medicationToDelete = await dbContext.Medications.FindAsync(med.ID);

                if (medicationToDelete != null)
                {
                    dbContext.Medications.Remove(medicationToDelete);

                    await dbContext.SaveChangesAsync();
                }
            }
        }

        public async Task EditMedication(Medication selectedMed, Medication editedMed)
        {
            using (MyDbContent dbContext = _dbContextFactory.CreateDbContext())
            {
                MedicationDTO medicationToUpdate = await dbContext.Medications.FindAsync(selectedMed.ID);

                if (medicationToUpdate != null)
                {
                    medicationToUpdate.Name = editedMed.Name;
                    medicationToUpdate.Description = editedMed.Description;
                    medicationToUpdate.Amount = editedMed?.Amount ?? 1;

                    await dbContext.SaveChangesAsync();
                }
            }
        }

        public async Task<IEnumerable<Medication>> GetAllMedications()
        {
            using (MyDbContent dbContext = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<MedicationDTO> medicationDTOs = await dbContext.Medications.ToListAsync();

                return medicationDTOs.Select(x => new Medication(x.ID, x.Name, x.Description, x.Amount));
            }
        }

        public async Task<Medication> GetConflictingMedication(Medication medication)
        {
            using (MyDbContent dbContext = _dbContextFactory.CreateDbContext())
            {
                MedicationDTO medDTO = await dbContext.Medications.Where(x => x.Name == medication.Name).
                    Where(x => x.Description == medication.Description).
                    Where(x => x.Amount == medication.Amount).FirstOrDefaultAsync();

                if (medDTO == null)
                {
                    return null;
                }
                else
                {
                    return new Medication(medDTO.ID, medDTO.Name, medDTO.Description, medDTO.Amount);
                }

            }
        }
    }
}
