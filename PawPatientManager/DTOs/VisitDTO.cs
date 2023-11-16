using PawPatientManager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PawPatientManager.DTOs
{
    public class VisitDTO
    {
        [Key]
        public Guid ID { get; set; }
        public Guid PetID { get; set; }
        [ForeignKey("PetID")]
        public PetDTO Pet { get; set; }
        public Guid VetID { get; set; }
        [ForeignKey("VetID")]
        public VetDTO Vet { get; set; }
        public DateTime Date { get; set; }
        public ICollection<MedicalReceiptDTO> MedicalReceipts { get; set; } = new List<MedicalReceiptDTO>();
    }
}
