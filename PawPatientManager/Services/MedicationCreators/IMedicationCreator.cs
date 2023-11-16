using PawPatientManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawPatientManager.Services.MedicationCreators
{
    public interface IMedicationCreator
    {
        Task CreateMedication(Medication med);
        Task DeleteMedication(Medication med);
        Task EditMedication(Medication selectedMed, Medication editedMed);
    }
}
