﻿using PawPatientManager.Commands;
using PawPatientManager.Stores;
using PawPatientManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace PawPatientManager.Services
{
    public interface INavigationService<TViewModel> where TViewModel : ViewModelBase
    {
        void Navigate();
    }

    public class NavigationService<TViewModel> : INavigationService<TViewModel> where TViewModel : ViewModelBase
    {
        /*  This is like a small factory for navitagion commands. *_createVMCallbackFunc* is a callback,
         *  while instantiating this class object method needs to be provided, this method will be passed
         *  in here. This passed method should create a new ViewModel for given View.
         */
        private Func<TViewModel> _createVMCallbackFunc;
        private NavigationStore _navigationStore;
        public NavigationService(NavigationStore navigationStore, Func<TViewModel> createVMCallbackFunc)
        {
            _createVMCallbackFunc = createVMCallbackFunc;
            _navigationStore = navigationStore;
        }
        public void Navigate()
        {
            _navigationStore.CurrentViewModel = _createVMCallbackFunc();
        }
    }

    public class LayoutNavigationService<TViewModel> : INavigationService<TViewModel> where TViewModel : ViewModelBase
    {
        /*  This is like a small factory for navitagion commands. *_createVMCallbackFunc* is a callback,
         *  while instantiating this class object method needs to be provided, this method will be passed
         *  in here. This passed method should create a new ViewModel for given View.
         */
        private Func<TViewModel> _createVMCallbackFunc;
        private NavigationStore _navigationStore;
        private Func<NavigationBarViewModel> _navBarVMCallback;
        public LayoutNavigationService(NavigationStore navigationStore, Func<TViewModel> createVMCallbackFunc, Func<NavigationBarViewModel> navBarVMCallback)
        {
            _createVMCallbackFunc = createVMCallbackFunc;
            _navigationStore = navigationStore;
            _navBarVMCallback = navBarVMCallback;
        }
        public void Navigate()
        {
            _navigationStore.CurrentViewModel = new LayoutViewModel(_navBarVMCallback(), _createVMCallbackFunc());
        }
    }

    public class LayoutNavigationServiceParam<TParameter, TViewModel> where TViewModel : ViewModelBase
    {

        private Func<TParameter, TViewModel> _createVMCallbackFunc;
        private NavigationStore _navigationStore;
        private Func<NavigationBarViewModel> _navBarVMCallback;
        public LayoutNavigationServiceParam(NavigationStore navigationStore, Func<TParameter, TViewModel> createVMCallbackFunc,Func<NavigationBarViewModel> navBarVMCallback)
        {
            _createVMCallbackFunc = createVMCallbackFunc;
            _navigationStore = navigationStore;
            _navBarVMCallback = navBarVMCallback;
        }
        public void Navigate(TParameter paramater)
        {
            _navigationStore.CurrentViewModel = new LayoutViewModel(_navBarVMCallback(), _createVMCallbackFunc(paramater));
        }
    }


    public class NavigationServiceParam<TParameter, TViewModel> where TViewModel : ViewModelBase
    {
        private Func<TParameter, TViewModel> _createVMCallbackFunc;
        private NavigationStore _navigationStore;
        public NavigationServiceParam(NavigationStore navigationStore, Func<TParameter, TViewModel> createVMCallbackFunc)
        {
            _createVMCallbackFunc = createVMCallbackFunc;
            _navigationStore = navigationStore;
        }
        public void Navigate(TParameter paramter)
        {
            _navigationStore.CurrentViewModel = _createVMCallbackFunc(paramter);
        }
    }
}
