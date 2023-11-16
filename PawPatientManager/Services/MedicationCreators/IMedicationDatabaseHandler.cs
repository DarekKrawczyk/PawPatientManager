using PawPatientManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawPatientManager.Services.MedicationCreators
{
    public interface IMedicationDatabaseHandler
    {
        Task CreateMedication(Medication med);
        Task DeleteMedication(Medication med);
        Task EditMedication(Medication selectedMed, Medication editedMed);
        Task<Medication> GetConflictingMedication(Medication medication);
        Task<IEnumerable<Medication>> GetAllMedications();
    }
}
