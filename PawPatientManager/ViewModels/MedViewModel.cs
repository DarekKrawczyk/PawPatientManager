using PawPatientManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawPatientManager.ViewModels
{
    public class MedViewModel
    {
        #region Fields - Med field for *VisitsViewModel.cs*
        private Medication _med;
        #endregion
        #region Properties for XAML
        public uint ID { get { return _med.ID; } set { _med.ID = value; } }
        public string Name { get { return _med.Name; } set { _med.Name = value; } }
        public string Description { get { return _med.Description; } set { _med.Description = value; } }
        public int Amount { get { return _med.Amount; } set { _med.Amount = value; } }
        #endregion
        #region Constructor
        public MedViewModel(Medication med)
        {
            _med = med;
        }
        #endregion
        #region Methods
        public bool IsNull()
        {
            if (_med == null) return true;
            else return false;
        }
        #endregion
    }
}
