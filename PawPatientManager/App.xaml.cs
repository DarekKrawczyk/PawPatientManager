using PawPatientManager.Models;
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
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(vetSystem)
            };

            MainWindow.Show();
        }
    }
}
