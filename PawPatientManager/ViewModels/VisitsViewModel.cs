using PawPatientManager.Commands;
using PawPatientManager.Models;
using PawPatientManager.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PawPatientManager.ViewModels
{
    public class VisitsViewModel : ViewModelBase
    {
        #region Fields
        private VetSystem _vetSystem;
        private INavigationService<RegisterVisitViewModel> _navRegisterVisitVMService;
        private LayoutNavigationServiceParam<VisitViewModel, EditVisitViewModel> _navEditVisitService;
        #endregion
        #region Fields for XAML
        private VisitViewModel _selectedVisitViewModel;
        private ObservableCollection<VisitViewModel> _visits;
        #endregion
        #region Properties for XAML
        public IEnumerable<VisitViewModel> Visits { get { return _visits; } set { OnPropertyChanged(nameof(Visits)); } }
        public VisitViewModel SelectedVisit { get { return _selectedVisitViewModel; } set { _selectedVisitViewModel = value; } }
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
