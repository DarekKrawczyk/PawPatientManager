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
    public class PetDTO
    {
        [Key]
        public Guid ID { get; set; }
        public string Name { get; set; }
        public bool Gender { get; set; }
        public string Species { get; set; }
        public string Race { get; set; }
        public string MicrochipNumber { get; set; }
        public Guid? OwnerID { get; set; }
        [ForeignKey("OwnerID")]
        public OwnerDTO? Owner { get; set; }
        public DateTime BirthDate { get; set; }
        public ICollection<VisitDTO> Visits { get; set; } = new List<VisitDTO>();
    }
}
