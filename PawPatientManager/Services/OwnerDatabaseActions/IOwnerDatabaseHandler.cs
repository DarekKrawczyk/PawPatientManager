using PawPatientManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawPatientManager.Services.OwnerDatabaseActions
{
    public interface IOwnerDatabaseHandler
    {
        Task CreateOwner(Owner owner);
        Task DeleteOwner(Owner owner);
        Task EditOwner(Owner selectedowner, Owner editedowner);
        Task<Owner> GetConflictingOwner(Owner owner);
        Task<IEnumerable<Owner>> GetAllOwners();
    }
}
