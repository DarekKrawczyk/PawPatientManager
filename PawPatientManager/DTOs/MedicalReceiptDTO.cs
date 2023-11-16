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
    public class MedicalReceiptDTO
    {
        [Key]
        public Guid ID { get; set; }
        public DateTime Signed { get; set; }
        public string Recommendation { get; set; }
        public Guid VisitID { get; set; }
        [ForeignKey("VisitID")]
        public VisitDTO Visit { get; set; }
    }
}
