using PawPatientManager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PawPatientManager.DTOs
{
    public class VetDTO
    {
        [Key]
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }    
        public ICollection<VisitDTO> Visits { get; set; } = new List<VisitDTO>();
    }
}
