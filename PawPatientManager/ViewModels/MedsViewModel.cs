using PawPatientManager.Commands;
using PawPatientManager.Models;
using PawPatientManager.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
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
        private bool _isLoading;
        // -- Filters --
        private string _nameFilter = string.Empty;
        private string _descriptionFilter = string.Empty;
        private string _amountFilter = string.Empty;
        #endregion
        #region Properties for XAML
        public ICollectionView MedsView;
        public IEnumerable<MedViewModel> Meds { get { return _meds; } set { OnPropertyChanged(nameof(Meds)); } }
        public MedViewModel SelectedMed { get { return _selectedMedVM; } 
            set { 
                _selectedMedVM = value; 
                EditName = _selectedMedVM?.Name;
                EditDescription = _selectedMedVM?.Description;
                EditAmount = (_selectedMedVM != null)? _selectedMedVM.Amount:0;
                OnPropertyChanged(nameof(SelectedMed));
            } 
        }
        public string AddName { get { return _addName; } set { _addName = value; OnPropertyChanged(nameof(AddName)); } }
        public string AddDescription { get { return _addDescription; } set { _addDescription = value; OnPropertyChanged(nameof(AddDescription)); } }
        public int AddAmount { get { return _addAmount; } set { _addAmount = value; OnPropertyChanged(nameof(AddAmount)); } }
        public string EditName { get { return _editName; } set { _editName = value; OnPropertyChanged(nameof(EditName)); } }
        public string EditDescription { get { return _editDescription; } set { _editDescription = value; OnPropertyChanged(nameof(EditDescription)); } }
        public int EditAmount { get { return _editAmount; } set { _editAmount = value; OnPropertyChanged(nameof(EditAmount)); } }
        public bool IsLoading { get { return _isLoading; } 
            set 
            { 
                _isLoading = value; 
                OnPropertyChanged(nameof(IsLoading)); 
            } 
        }
        // -- Filters --
        public string NameFilter { get { return _nameFilter; } 
            set 
            { 
                _nameFilter = value; 
                OnPropertyChanged(nameof(NameFilter));
                MedsView.Refresh();
            } 
        }
        public string DescriptionFilter
        {
            get { return _descriptionFilter; }
            set
            {
                _descriptionFilter = value;
                OnPropertyChanged(nameof(DescriptionFilter));
                MedsView.Refresh();
            }
        }
        public string AmountFilter
        {
            get { return _amountFilter; }
            set
            {
                _amountFilter = value;
                OnPropertyChanged(nameof(AmountFilter));
                MedsView.Refresh();
            }
        }
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
            MedsView = CollectionViewSource.GetDefaultView(_meds);

            MedsView.Filter = FilterMeds;
            //MedsView.GroupDescriptions.Add(new PropertyGroupDescription(nameof(MedViewModel.Name)));
            //MedsView.SortDescriptions.Add(new SortDescription(nameof(MedViewModel.Name), ListSortDirection.Ascending));

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
        public bool FilterMeds(object obj)
        {
            if(obj is MedViewModel med)
            {
                return med.Name.Contains(NameFilter, StringComparison.InvariantCultureIgnoreCase) && med.Description.Contains(DescriptionFilter, StringComparison.InvariantCultureIgnoreCase) &&
                    med.Amount == ((AmountFilter == string.Empty)?(med.Amount):int.Parse(AmountFilter));
            }
            return false;
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
