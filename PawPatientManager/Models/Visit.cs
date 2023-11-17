using PawPatientManager.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Windows.Media;
using PawPatientManager.ViewModels;

namespace PawPatientManager.Models
{
    public class Visit
    {
        private Guid _id;
        private Pet _pet;
        private Vet _vet;
        private DateTime _date;
        private List<MedicalReceipt> _medicalReceipts;

        public Guid ID { get { return _id; } set { _id = value; } }
        public Pet Pet { get { return _pet; } set { _pet = value; } }
        public Vet Vet { get { return _vet; } set { _vet = value; } }
        public Owner Owner { get { return _pet.Owner; } set { _pet.Owner = value; } }
        public DateTime Date { get { return _date;} set { _date = value; } }
        public List<MedicalReceipt> MedicalReceipts { get { return _medicalReceipts; } set { _medicalReceipts = value; } }
        public Visit(Guid id, Pet pet, Vet vet, DateTime date, List<MedicalReceipt> medicalReceipts)
        {
            _id = id;
            _pet = pet;
            _vet = vet;
            _date = date;
            _medicalReceipts = medicalReceipts;
        }   
        public Visit(VisitDTO visit, PetDTO pet, VetDTO vet, OwnerDTO petOwner)
        {
            _id = visit.ID;
            _pet = new Pet(pet, petOwner);
            _vet = new Vet(vet);
            _date = visit.Date;
            _medicalReceipts = null;
        }
        public Visit(VisitViewModel visitVM)
        {
            _id = visitVM.ID;
            _pet = visitVM.Pet;
            _vet = visitVM.Vet;
            _date = visitVM.Date;
            _medicalReceipts = visitVM.MedicalReceipts;
        }
    }
}
