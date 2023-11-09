using PawPatientManager.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawPatientManager.ViewModels
{
    public class LayotViewModel : ViewModelBase
    {
        #region
        private NavigationBarViewModel _navigationBarVM;
        private ViewModelBase _contentVM;
        #endregion
        #region
        public NavigationBarViewModel NavigationBarVM {  get { return _navigationBarVM; } }
        public ViewModelBase ContentVM { get { return _contentVM; } }
        #endregion
        #region
        public LayotViewModel(NavigationBarViewModel navigationBarVM, ViewModelBase contentVM)
        {
            _navigationBarVM = navigationBarVM;
            _contentVM = contentVM;
        }
        #endregion
    }
}
