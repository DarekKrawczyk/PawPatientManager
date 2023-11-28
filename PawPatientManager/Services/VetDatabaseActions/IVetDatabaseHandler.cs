using PawPatientManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawPatientManager.Services.VetDatabaseActions
{
    public interface IVetDatabaseHandler
    {
        Task CreateVet(Vet vet);
        Task DeleteVet(Vet vet);
        Task<Vet> GetConflictingVet(Vet vet);
        Task<Vet> LoginVet(string login, string password);
        Task<IEnumerable<Vet>> GetAllVets();
        Task DeleteAllVets();
    }
}
