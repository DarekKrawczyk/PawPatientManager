using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawPatientManager.DbContexts
{
    public class MedicationDesignTimeDbContextFactory : IDesignTimeDbContextFactory<MedicationDbContext>
    {
        public MedicationDbContext CreateDbContext(string[] args)
        {
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlite("Data Source=medication.db").Options;
            return new MedicationDbContext(options);
        }
    }
}
