using PawPatientManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace PawPatientManager.Stores
{
    public class NavigationStore
    {
        private ViewModelBase _viewModel;
        /*  This event was created to know when was the *CurrentViewModel* changed (property set).
         *  Whenever it is set, *OnCurrentViewModelChanged()* function is called and this function
         *  checks whether *CurrentViewModelChanged* is null (means that nobody subscribed to this event)
         *  and if it isn't it *Invoke()* this event, which means it will be fired.
         *  
         *  Whoever subscribes to this event will get notification! - check *MainViewModel.cs*
         */
        public event Action CurrentViewModelChanged;
        public ViewModelBase CurrentViewModel { get { return _viewModel; } set { _viewModel = value; OnCurrentViewModelChanged(); } }
        public NavigationStore(ViewModelBase viewModel)
        {
            _viewModel = viewModel;
        }
        public NavigationStore()
        {
        }
        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }
    }
}
