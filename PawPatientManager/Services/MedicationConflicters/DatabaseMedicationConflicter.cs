using Microsoft.EntityFrameworkCore;
using PawPatientManager.DbContexts;
using PawPatientManager.DTOs;
using PawPatientManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawPatientManager.Services.MedicationConflicters
{
    public class DatabaseMedicationConflicter : IMedicationConflicter
    {
        private MedicationDbContextFactory _dbContextFactory;
        public DatabaseMedicationConflicter(MedicationDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public async Task<Medication> GetConflictingMedication(Medication medication)
        {
            using (MedicationDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                MedicationDTO medDTO = await dbContext.Medications.Where(x=>x.Name == medication.Name).
                    Where(x=>x.Description==medication.Description).
                    Where(x=>x.Amount == medication.Amount).FirstOrDefaultAsync();
                //return await dbContext.Medications.Select(x => new Medication(0, x.Name, x.Description, x.Amount)).FirstOrDefaultAsync(x => x.Conflicts(medication));
                
                if(medDTO==null) 
                {
                    return null;
                }
                else
                {
                    //TODO ZROB COS Z TYM 0!!!!
                    return new Medication(medDTO.ID, medDTO.Name,medDTO.Description,medDTO.Amount);
                }

            }
        }
    }
}
