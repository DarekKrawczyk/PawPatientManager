using Microsoft.EntityFrameworkCore;
using PawPatientManager.Components;
using PawPatientManager.DbContexts;
using PawPatientManager.DbContextsFiles;
using PawPatientManager.Models;
using PawPatientManager.Services;
using PawPatientManager.Services.MedicationCreators;
using PawPatientManager.Services.OwnerDatabaseActions;
using PawPatientManager.Services.PetDatabaseActions;
using PawPatientManager.Services.VetDatabaseActions;
using PawPatientManager.Services.VisitDatabaseActions;
using PawPatientManager.Stores;
using PawPatientManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace PawPatientManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static string ConnectionString = "Data Source=medication.db";
        private AccountStore _accountStore;
        private VetSystem _vetSystem;
        private NavigationStore _navigationStore;
        private INavigationService<LoginViewModel> _firstNavService;
        #region Fields - Database
        private IMedicationDatabaseHandler _medicationCreator;
        private IOwnerDatabaseHandler _ownerCreator;
        private IPetDatabaseHandler _petCreator;
        private IVetDatabaseHandler _vetCreator;
        private IVisitDatabaseHandler _visitCreator;
        private DbContentFactory _dbContextFactory;
        #endregion

        public App()
        {
            _dbContextFactory = new DbContentFactory(ConnectionString);

            _medicationCreator = new MedicationDatabaseHandler(_dbContextFactory);
            _ownerCreator = new OwnerDatabaseHandler(_dbContextFactory);
            _petCreator = new PetDatabaseHandler(_dbContextFactory);
            _vetCreator = new VetDatabaseHandler(_dbContextFactory);
            _visitCreator = new VisitDatabaseHandler(_dbContextFactory);

            _vetSystem = new VetSystem(_medicationCreator, _ownerCreator, _petCreator, _vetCreator, _visitCreator);

            _navigationStore = new NavigationStore();
            _accountStore = new AccountStore();

            _firstNavService = CreateLoginNavService();

        }

        protected override void OnStartup(StartupEventArgs e)
        {
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlite(ConnectionString).Options;
            using (MyDbContent medsDbContext = new MyDbContent(options))
            {
                RelationalDatabaseFacadeExtensions.Migrate(medsDbContext.Database);
            };


            _firstNavService.Navigate();
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_vetSystem, _navigationStore)
            };

            MainWindow.Show();
        }
        #region Factories - Owner
        private INavigationService<OwnerRegistrationViewModel> CreateOwnerRegisterService()
        {
            return new LayoutNavigationService<OwnerRegistrationViewModel>(
                _navigationStore,
                () => new OwnerRegistrationViewModel(
                    _vetSystem,
                    CreateManageOwnersNavService()),
                CreateNavBarVM);
        }
        private INavigationService<ManageOwnersViewModel> CreateManageOwnersNavService()
        {
            return new LayoutNavigationService<ManageOwnersViewModel>(
                _navigationStore,
                () => ManageOwnersViewModel.LoadMedsViewModel(
                    _vetSystem,
                    CreateOwnerRegisterService(),
                    CreateEditOwnerNavService()
                    ),
                CreateNavBarVM);
            //return new LayoutNavigationService<ManageOwnersViewModel>(
            //    _navigationStore,
            //    () => new ManageOwnersViewModel(
            //        _vetSystem,
            //        CreateOwnerRegisterService(),
            //        CreateEditOwnerNavService()
            //        ),
            //    CreateNavBarVM);
        }
        private LayoutNavigationServiceParam<OwnerViewModel, EditOwnerViewModel> CreateEditOwnerNavService()
        {
            return new LayoutNavigationServiceParam<OwnerViewModel, EditOwnerViewModel>(
                _navigationStore,
                (paramater) => new EditOwnerViewModel(
                    _vetSystem,
                    paramater,
                    CreateManageOwnersNavService()
                    ),
                CreateNavBarVM);
        }
        #endregion
        #region Factories - Pet
        private INavigationService<RegisterPetViewModel> CreateRegisterPetNavService()
        {
            return new LayoutNavigationService<RegisterPetViewModel>(
                _navigationStore,
                () => RegisterPetViewModel.LoadMedsViewModel(
                    _vetSystem,
                    CreatePetsNavService()
                    ),
                CreateNavBarVM);           
            //return new LayoutNavigationService<RegisterPetViewModel>(
            //    _navigationStore,
            //    () => new RegisterPetViewModel(
            //        _vetSystem,
            //        CreatePetsNavService()
            //        ),
            //    CreateNavBarVM);
        }
        private INavigationService<PetsViewModel> CreatePetsNavService()
        {
            return new LayoutNavigationService<PetsViewModel>(
                _navigationStore,
                () => PetsViewModel.LoadViewModel(
                    _vetSystem,
                    CreateRegisterPetNavService(),
                    CreateEditPetNavService()
                    ),
                CreateNavBarVM
                );            
            //return new LayoutNavigationService<PetsViewModel>(
            //    _navigationStore,
            //    () => new PetsViewModel(
            //        _vetSystem,
            //        CreateRegisterPetNavService(),
            //        CreateEditPetNavService()
            //        ),
            //    CreateNavBarVM
            //    );
        }

        private LayoutNavigationServiceParam<PetViewModel, EditPetViewModel> CreateEditPetNavService()
        {
            return new LayoutNavigationServiceParam<PetViewModel, EditPetViewModel>(
                _navigationStore,
                (paramater) => new EditPetViewModel(
                    _vetSystem,
                    paramater,
                    CreatePetsNavService()
                    ),
                CreateNavBarVM);
        }
        #endregion
        #region Factories - Visits
        private INavigationService<RegisterVisitViewModel> CreateRegisterVisitNavService()
        {
            return new LayoutNavigationService<RegisterVisitViewModel>(
                _navigationStore,
                () => RegisterVisitViewModel.LoadViewModel(
                    _vetSystem,
                    CreateVisitsNavService()
                    ),
                CreateNavBarVM);
        }
        private INavigationService<VisitsViewModel> CreateVisitsNavService()
        {
            return new LayoutNavigationService<VisitsViewModel>(
                _navigationStore,
                () => VisitsViewModel.LoadViewModel(
                    _vetSystem,
                    CreateRegisterVisitNavService(),
                    CreateEditVisitNavService()
                    ),
                CreateNavBarVM
                );
        }

        private LayoutNavigationServiceParam<VisitViewModel, EditVisitViewModel> CreateEditVisitNavService()
        {
            return new LayoutNavigationServiceParam<VisitViewModel, EditVisitViewModel>(
                _navigationStore,
                (paramater) => EditVisitViewModel.LoadViewModel(
                    _vetSystem,
                    paramater,
                    CreateVisitsNavService()
                    ),
                CreateNavBarVM);
        }
        #endregion
        #region Factories - Meds
        private INavigationService<MedsViewModel> CreateMedsNavService()
        {
            return new LayoutNavigationService<MedsViewModel>(
                _navigationStore,
                () => MedsViewModel.LoadMedsViewModel(_vetSystem),
                CreateNavBarVM);
            //return new LayoutNavigationService<MedsViewModel>(
            //    _navigationStore,
            //    () => new MedsViewModel(
            //        _vetSystem
            //        ),
            //    CreateNavBarVM);
        }

        #endregion
        #region Factories - Home
        private INavigationService<HomeViewModel> CreateHomeNavService()
        {
            return new LayoutNavigationService<HomeViewModel>(
                _navigationStore,
                () => HomeViewModel.LoadViewModel(_vetSystem,_accountStore),
                CreateNavBarVM);
        }
        #endregion
        #region Factories - Login
        private INavigationService<LoginViewModel> CreateLoginNavService()
        {
            return new NavigationService<LoginViewModel>(
                _navigationStore,
                () => new LoginViewModel(
                    _vetSystem,
                    _accountStore, 
                    CreateHomeNavService()
                    )
                );
        }
        #endregion
        #region Factories - Navigation bar
        private NavigationBarViewModel CreateNavBarVM()
        {
            return new NavigationBarViewModel(_vetSystem,
                CreateHomeNavService(),
                CreateManageOwnersNavService(),
                CreateLoginNavService(),
                CreatePetsNavService(),
                CreateVisitsNavService(),
                CreateMedsNavService()
                );
        }
        #endregion
    }
}
