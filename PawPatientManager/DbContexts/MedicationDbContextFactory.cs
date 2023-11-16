using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawPatientManager.DbContexts
{
    public class MedicationDbContextFactory
    {
        private string _connectionString;
        public MedicationDbContextFactory(string connectionString) 
        { 
            _connectionString = connectionString;
        }
        
        public MedicationDbContext CreateDbContext()
        {
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlite(_connectionString).Options;
            return new MedicationDbContext(options);
        }
    }
}
