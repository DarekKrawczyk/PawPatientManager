using PawPatientManager.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PawPatientManager.Models
{
    public class Vet
    {
        private Guid _id;
        private string _name;
        private string _surname;
        private List<Visit> _visits;
        public Guid ID { get { return _id; } set { _id = value; } }
        public string Name { get { return _name; } set { _name = value; } }
        public string Surname { get { return _surname; } set { _surname = value; } }
        public List<Visit> Visits { get { return _visits; } set { _visits = value; } }
        public Vet(Guid id, string name, string surname, List<Visit> visits)
        {
            _id = id;
            _name = name;
            _surname = surname;
            _visits = visits;
        }
        public Vet(VetDTO vetDTO)
        {
            _id = vetDTO.ID;
            _name = vetDTO.Name;
            _surname = vetDTO.Surname;
            _visits = null;
        }
        public Vet(Guid id, string name, string surname)
        {
            _id = id;
            _name = name;
            _surname = surname;
            _visits = new List<Visit>();
        }
    }
}
