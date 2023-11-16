using Microsoft.EntityFrameworkCore;
using PawPatientManager.DbContexts;
using PawPatientManager.DTOs;
using PawPatientManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawPatientManager.Services.MedicationProviders
{
    public class DatabaseMedicationProvider : IMedicationProvider
    {
        private MedicationDbContextFactory _dbContextFactory;
        public DatabaseMedicationProvider(MedicationDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public async Task<IEnumerable<Medication>> GetAllMedications()
        {
            using(MedicationDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<MedicationDTO> medicationDTOs =  await dbContext.Medications.ToListAsync();

                //TODO - USUNĄC TE 0 <DARIKRA>
                return medicationDTOs.Select(x => new  Medication(x.ID, x.Name, x.Description, x.Amount));
            }
        }
    }
}
