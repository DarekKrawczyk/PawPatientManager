using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawPatientManager.Models
{
    public class Visit
    {
        private uint _id;
        private Pet _pet;
        private Vet _vet;
        private DateTime _date;
        private List<Medication> _medications;

        public uint Id { get { return _id; } set { _id = value; } }
        public Pet Pet { get { return _pet; } set { _pet = value; } }
        public Vet Vet { get { return _vet; } set { _vet = value; } }
        public DateTime Date { get { return _date;} set { _date = value; } }
        public List<Medication> Medications { get { return _medications; } set { _medications = value; } }
        public Visit(uint id, Pet pet, Vet vet, DateTime date, List<Medication> medications)
        {
            _id = id;
            _pet = pet;
            _vet = vet;
            _date = date;
            _medications = medications;
        }   
    }
}
