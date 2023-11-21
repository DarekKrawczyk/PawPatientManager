using PawPatientManager.Models;
using PawPatientManager.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace PawPatientManager.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private AccountStore _accountStore;
        private VetSystem _vetSystem;
        private string _name;
        private string _surname;
        private string _firstVisit;
        private int _visitsAmount;
        private string _medicalReceipt;
        private VisitViewModel _selectedVisitViewModel;
        private ObservableCollection<VisitViewModel> _visits;
        public string MedicalReceipt { get { return _medicalReceipt; } set { _medicalReceipt = value; 
                OnPropertyChanged(nameof(MedicalReceipt)); } }
        public string Name { get { return _accountStore.CurrentAccount.Name; } set { _name = value; OnPropertyChanged(nameof(Name)); } }
        public string Surname { get { return _accountStore.CurrentAccount.Surname; } set { _surname = value; OnPropertyChanged(nameof(Surname)); } }
        public int VisitsAmount { get { return _visitsAmount; } set { _visitsAmount = value; OnPropertyChanged(nameof(VisitsAmount)); } }
        public string VisitsText { get { return $"You have {_visitsAmount} visits"; } }
        public string FirstVisitTime { get { return _firstVisit; } set { _firstVisit = value; OnPropertyChanged(nameof(VisitsAmount)); } }
        public IEnumerable<VisitViewModel> Visits { get { return _visits; } set { OnPropertyChanged(nameof(Visits)); } }
        public VisitViewModel SelectedVisit { get { return _selectedVisitViewModel; } set { _selectedVisitViewModel = value; OnPropertyChanged(nameof(SelectedVisit)); } }
        public string Login { get { return _accountStore.CurrentAccount.Login; } }
        public AccountStore AccountStore { get { return _accountStore; } }
        public ICommand CommandPrescribe { get; }
        public ICommand CommandHandleVisitSelectionChange { get; }
        public ICommand CommandLoadVisits { get; }
        public HomeViewModel(VetSystem vetsystem, AccountStore accountStore)
        {
            _vetSystem = vetsystem;
            _accountStore = accountStore;
            _visits = new ObservableCollection<VisitViewModel>();

            CommandHandleVisitSelectionChange = new Commands.HomeCommands.UpdateSelected(this);
            CommandLoadVisits = new Commands.HomeCommands.LoadVisits(_vetSystem, this);
            CommandPrescribe = new Commands.HomeCommands.Prescribe(_vetSystem, this);
        }
        public static HomeViewModel LoadViewModel(VetSystem vetsystem, AccountStore accountStore)
        {
            HomeViewModel vm = new HomeViewModel(vetsystem, accountStore);

            vm.CommandLoadVisits.Execute(null);

            return vm;
        }
        public void ReloadVisits(IEnumerable<Visit> visits)
        {
            _visits.Clear();
            foreach(Visit vis in visits)
            {
                _visits.Add(new VisitViewModel(vis));
            }
            try
            {
                VisitsAmount = _visits.Count();
                FirstVisitTime = _visits.OrderBy(v => Math.Abs((v.Date - DateTime.Now).Ticks)).FirstOrDefault().Date.ToString();
            } catch(Exception ex) {
                FirstVisitTime = "0";
            }
        }
    }
}
