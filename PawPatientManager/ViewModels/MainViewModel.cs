using PawPatientManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawPatientManager.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ViewModelBase _currentViewModel;
        private VetSystem _vetSystem;
        public ViewModelBase CurrentViewModel {  get { return _currentViewModel; } }
        public MainViewModel(VetSystem vetSystem) 
        {
            _vetSystem = vetSystem;
            _currentViewModel = new OwnerRegistrationViewModel(_vetSystem);
        }
    }
}
