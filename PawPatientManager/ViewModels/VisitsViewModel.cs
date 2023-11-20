using PawPatientManager.Commands;
using PawPatientManager.Models;
using PawPatientManager.Services;
using PawPatientManager.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Data;
using System.Windows.Input;

namespace PawPatientManager.ViewModels
{
    public class VisitsViewModel : ViewModelBase
    {
        #region Fields
        private VetSystem _vetSystem;
        private INavigationService<RegisterVisitViewModel> _navRegisterVisitVMService;
        private LayoutNavigationServiceParam<VisitViewModel, EditVisitViewModel> _navEditVisitService;
        // -- Filters --
        private string _petFilter = string.Empty;
        private string _ownerFilter = string.Empty;
        private string _vetFilter = string.Empty;
        #endregion
        #region Fields for XAML
        private VisitViewModel _selectedVisitViewModel;
        private ObservableCollection<VisitViewModel> _visits;
        #endregion
        #region Properties for XAML
        public IEnumerable<VisitViewModel> Visits { get { return _visits; } set { OnPropertyChanged(nameof(Visits)); } }
        public VisitViewModel SelectedVisit { get { return _selectedVisitViewModel; } set { _selectedVisitViewModel = value; OnPropertyChanged(nameof(SelectedVisit)); } }
        // -- Filters --
        public ICollectionView VisitsView;
        public string PetFilter { get { return _petFilter; } set { _petFilter = value; OnPropertyChanged(nameof(PetFilter)); VisitsView.Refresh(); } }
        public string VetFilter { get { return _vetFilter ; } set { _vetFilter = value; OnPropertyChanged(nameof(VetFilter)); VisitsView.Refresh(); } }
        public string OwnerFilter { get { return _ownerFilter; } set { _ownerFilter = value; OnPropertyChanged(nameof(OwnerFilter)); VisitsView.Refresh(); } }
        #endregion
        #region Commands
        public ICommand CommandRegisterVisit { get; }
        public ICommand CommandDeleteVisit { get; }
        public ICommand CommandEditVisit { get; }
        public ICommand CommandLoadVisits { get; }
        public ICommand CommandHandleVisitSelectionChange { get; }
        #endregion
        public VisitsViewModel(VetSystem vetSystem, INavigationService<RegisterVisitViewModel> navRegisterVisitVMService,
             LayoutNavigationServiceParam<VisitViewModel, EditVisitViewModel> navEditVisitService)
        {
            _selectedVisitViewModel = new VisitViewModel(null);
            _vetSystem = vetSystem;
            _navRegisterVisitVMService = navRegisterVisitVMService;
            _navEditVisitService = navEditVisitService;

            _visits = new ObservableCollection<VisitViewModel>();
            VisitsView = CollectionViewSource.GetDefaultView(_visits);

            VisitsView.Filter = FilterVisits;

            CommandLoadVisits = new Commands.VisitsCommandsCombobox.LoadVisits(_vetSystem, this);
            CommandRegisterVisit = new NavigateCommand<RegisterVisitViewModel>(_navRegisterVisitVMService);
            CommandEditVisit = new Commands.VisitsCommandsCombobox.EditVisit(this, _navEditVisitService);
            CommandDeleteVisit = new Commands.VisitsCommandsCombobox.DeleteVisit(_vetSystem, this);
            CommandHandleVisitSelectionChange = new Commands.VisitsCommandsCombobox.UpdateSelected(this);
        }

        public static VisitsViewModel LoadViewModel (VetSystem vetSystem, INavigationService<RegisterVisitViewModel> navRegisterVisitVMService,
             LayoutNavigationServiceParam<VisitViewModel, EditVisitViewModel> navEditVisitService)
        {
            VisitsViewModel vm = new VisitsViewModel(vetSystem, navRegisterVisitVMService, navEditVisitService);

            vm.CommandLoadVisits.Execute(null);

            return vm;
        }

        public bool FilterVisits(object obj)
        {
            if (obj is VisitViewModel visit)
            {
                return visit.VetFullName.Contains(VetFilter, StringComparison.InvariantCultureIgnoreCase) &&
                    visit.PetFullName.Contains(PetFilter, StringComparison.InvariantCultureIgnoreCase) &&
                    visit.OwnerFullName.Contains(OwnerFilter, StringComparison.InvariantCultureIgnoreCase);
            }
            return false;
        }

        public void ReloadVisits(IEnumerable<Visit> visits)
        {
            _visits.Clear();
            foreach (Visit visit in visits)
            {
                _visits.Add(new VisitViewModel(visit));
            }
        }
    }
}
