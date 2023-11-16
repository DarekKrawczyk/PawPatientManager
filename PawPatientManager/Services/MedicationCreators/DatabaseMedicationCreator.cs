using Microsoft.EntityFrameworkCore;
using PawPatientManager.DbContexts;
using PawPatientManager.DTOs;
using PawPatientManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawPatientManager.Services.MedicationCreators
{
    public class DatabaseMedicationCreator : IMedicationCreator
    {
        private MedicationDbContextFactory _dbContextFactory;
        public DatabaseMedicationCreator(MedicationDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public async Task CreateMedication(Medication med)
        {
            using (MedicationDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                MedicationDTO medicationDto = new MedicationDTO()
                {
                    Name = med.Name,
                    Description = med.Description,
                    Amount = med?.Amount ?? 1
                };

                dbContext.Medications.Add(medicationDto);
                await dbContext.SaveChangesAsync();

                Guid newMedicationId = medicationDto.ID;
            }
        }

        public async Task DeleteMedication(Medication med)
        {
            using (MedicationDbContext dbContext = _dbContextFactory.CreateDbContext())
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
            using (MedicationDbContext dbContext = _dbContextFactory.CreateDbContext())
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
    }
}
