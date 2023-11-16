using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PawPatientManager.DTOs
{
    public class MedicationDTO
    {
        [Key]
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
    }
}
