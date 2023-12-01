using System.ComponentModel.DataAnnotations;

namespace PawPatientManagerWebAPI.DTOs
{
    public class MedicationDTO
    {
        [Key]
        public Guid ID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Amount { get; set; }
    }
}
