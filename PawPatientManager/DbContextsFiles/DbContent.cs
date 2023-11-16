using Microsoft.EntityFrameworkCore;
using PawPatientManager.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawPatientManager.DbContextsFiles
{
    public class MyDbContent : DbContext
    {
        public MyDbContent(DbContextOptions options) : base(options)
        {
        }

        public DbSet<MedicalReceiptDTO> MedicalReceipts { get; set; }
        public DbSet<MedicationDTO> Medications { get; set; }
        public DbSet<OwnerDTO> Owners { get; set; }
        public DbSet<PetDTO> Pets { get; set; }
        public DbSet<VetDTO> Vets { get; set; }
        public DbSet<VisitDTO> Visits { get; set; }
    }
    public class DbContentFactory
    {
        private string _connectionString;
        public DbContentFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public MyDbContent CreateDbContext()
        {
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlite(_connectionString).Options;
            return new MyDbContent(options);
        }
    }
}
