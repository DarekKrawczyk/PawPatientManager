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
    public class OwnerDTO
    {
        [Key]
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public ICollection<PetDTO> Pets { get; set; } = new List<PetDTO>();
        public bool Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string Adress { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string PESEL { get; set; }
    }
}
