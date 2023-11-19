using PawPatientManager.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawPatientManager.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        //private NavigationBarViewModel _navigationBarVM;
        //public NavigationBarViewModel NavigationBarVM {  get { return _navigationBarVM; } }
        //public HomeViewModel(NavigationBarViewModel navigationBarVM)
        //{
        //    _navigationBarVM = navigationBarVM;
        //}
        private AccountStore _accountStore;
        public string Login { get { return _accountStore.CurrentAccount.Login; } }
        public HomeViewModel(AccountStore accountStore)
        {
            _accountStore = accountStore;
        }
    }
}
