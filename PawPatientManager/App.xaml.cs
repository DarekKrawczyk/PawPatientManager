using PawPatientManager.Models;
using PawPatientManager.Stores;
using PawPatientManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PawPatientManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            VetSystem vetSystem = new VetSystem();

            NavigationStore navigationStore = new NavigationStore();
            NavigationBarViewModel navigationBarVM = new NavigationBarViewModel(navigationStore, vetSystem);

            navigationStore.CurrentViewModel = new HomeViewModel();
            //navigationStore.CurrentViewModel = new HomeViewModel(navigationBarVM);
            //navigationStore.CurrentViewModel = new ManageOwnersViewModel(navigationStore, vetSystem, navigationBarVM);

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(vetSystem, navigationStore)
                //DataContext = new MainViewModel(vetSystem, navigationStore, navigationBarVM)
            };

            MainWindow.Show();
        }
    }
}
