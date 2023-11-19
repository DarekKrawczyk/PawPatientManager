using PawPatientManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawPatientManager.Services.VisitDatabaseActions
{
    public interface IVisitDatabaseHandler
    {
        Task CreateVisit(Vet vet, Pet pet, DateTime dateTime);
        Task DeleteVisit(Visit visit);
        Task EditVisit(Visit selectedVisit, Visit editedVisit, Guid newVetGUID, Guid newPetGUID);
        Task<Visit> GetConflictingVisit(Visit visit);
        Task<IEnumerable<Visit>> GetAllVisits();
    }
}
