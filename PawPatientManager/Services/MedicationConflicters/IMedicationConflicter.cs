using PawPatientManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawPatientManager.Services.MedicationConflicters
{
    public interface IMedicationConflicter
    {
        Task<Medication> GetConflictingMedication(Medication medication);
    }
}
