using Microsoft.EntityFrameworkCore;
using PawPatientManagerWebAPI.DTOs;

namespace PawPatientManagerWebAPI.DBContextFiles
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        public DbSet<MedicationDTO> Medications { get; set; }
    }
}
