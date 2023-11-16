using Microsoft.EntityFrameworkCore;
using PawPatientManager.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawPatientManager.DbContexts
{
    public class MedicationDbContext : DbContext
    {
        public MedicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<MedicationDTO> Medications { get; set; }
    }
}
