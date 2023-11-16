using PawPatientManager.Commands;
using PawPatientManager.Models;
using PawPatientManager.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace PawPatientManager.ViewModels
{
    public class MedsViewModel : ViewModelBase
    {
        #region Fields
        private VetSystem _vetSystem;
        #endregion
        #region Fields for XAML
        private MedViewModel _selectedMedVM;
        private ObservableCollection<MedViewModel> _meds;
        private string _addName;
        private string _addDescription;
        private int _addAmount;
        private string _editName;
        private string _editDescription;
        private int _editAmount;
        #endregion
        #region Properties for XAML
        public IEnumerable<MedViewModel> Meds { get { return _meds; } set { OnPropertyChanged(nameof(Meds)); } }
        public MedViewModel SelectedMed { get { return _selectedMedVM; } 
            set { 
                _selectedMedVM = value; 
                EditName = _selectedMedVM?.Name;
                EditDescription = _selectedMedVM?.Description;
                EditAmount = (_selectedMedVM != null)? _selectedMedVM.Amount:0;
            } 
        }
        public string AddName { get { return _addName; } set { _addName = value; OnPropertyChanged(nameof(AddName)); } }
        public string AddDescription { get { return _addDescription; } set { _addDescription = value; OnPropertyChanged(nameof(AddDescription)); } }
        public int AddAmount { get { return _addAmount; } set { _addAmount = value; OnPropertyChanged(nameof(AddAmount)); } }
        public string EditName { get { return _editName; } set { _editName = value; OnPropertyChanged(nameof(EditName)); } }
        public string EditDescription { get { return _editDescription; } set { _editDescription = value; OnPropertyChanged(nameof(EditDescription)); } }
        public int EditAmount { get { return _editAmount; } set { _editAmount = value; OnPropertyChanged(nameof(EditAmount)); } }
        #endregion
        #region Commands
        public ICommand CommandAddMed { get; }
        public ICommand CommandEditMed { get; }
        public ICommand CommandDeleteMed { get; }
        public ICommand CommandHandleMedSelectionChange { get; }
        public ICommand CommandLoadMeds { get; }
        #endregion
        public MedsViewModel(VetSystem vetSystem)
        {
            _selectedMedVM = new MedViewModel(null);
            _vetSystem = vetSystem;

            _meds = new ObservableCollection<MedViewModel>();

            CommandAddMed = new Commands.MedsCommands.AddMed(_vetSystem, this);
            CommandEditMed = new Commands.MedsCommands.EditMed(_vetSystem, this);
            CommandDeleteMed = new Commands.MedsCommands.DeleteMed(_vetSystem, this);
            CommandLoadMeds = new Commands.MedsCommands.LoadMeds(_vetSystem, this);
            CommandHandleMedSelectionChange = new Commands.MedsCommands.SelectedMedChanged(this);
        }

        public static MedsViewModel LoadMedsViewModel(VetSystem vetSystem)
        {
            MedsViewModel _vm = new MedsViewModel(vetSystem);

            _vm.CommandLoadMeds.Execute(null);

            return _vm;
        }

        public void ReloadMeds(IEnumerable<Medication> meds)
        {
            _meds.Clear();
            foreach (Medication med in meds)
            {
                _meds.Add(new MedViewModel(med));
            }

            // Notify UI
            OnPropertyChanged(nameof(Meds));
        }

        //public bool DeleteVisit(VisitViewModel visitVM)
        //{
        //    bool result = false;
        //    if (visitVM == null)
        //    {
        //        result = _vetSystem.Visits.Remove(_selectedVisitViewModel.Visit);
        //    }
        //    else result = _vetSystem.Visits.Remove(visitVM.Visit);
        //    ReloadVisits();
        //    OnPropertyChanged(nameof(Visits));
        //    return result;

        //}
    }
}
