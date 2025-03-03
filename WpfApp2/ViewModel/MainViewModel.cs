using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using FontAwesome.Sharp;
using WpfApp2.Model;
using WpfApp2.Repositories;

namespace WpfApp2.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        //polja
        private UserAccountModel _currentUserAccount;
        private ViewModelBase _currentChildView;
        private string _caption;
        private IconChar _icon;
        private IUserRepository userRepository;

        public UserAccountModel CurrentUserAccount
        {
            get { return _currentUserAccount; }
            set
            {
                _currentUserAccount = value;
                OnPropertyChanged(nameof(CurrentUserAccount));
            }

        }

        public ViewModelBase CurrentChildView
        {
            get { return _currentChildView; }
            set
            {
                _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
            }
        }
        public string Caption
        {
            get { return _caption; }
            set
            {
                _caption = value;
                OnPropertyChanged(nameof(Caption));
            }
        }
        public IconChar Icon
        {
            get { return _icon; }
            set
            {
                _icon = value;
                OnPropertyChanged(nameof(Icon));
            }
        }

        public ICommand ShowHomeViewCommand { get; }
        public ICommand ShowCustomerViewCommand { get; }

        public ICommand ShowStatistikaViewCommand { get; }

        public ICommand ShowTablicaViewCommand { get; }

        public MainViewModel()
        {
            userRepository = new UserRepository();
            CurrentUserAccount = new UserAccountModel();

            ShowHomeViewCommand = new ViewModelCommand(ExecuteShowHomeViewCommand);
            ShowCustomerViewCommand = new ViewModelCommand(ExecuteShowCustomerViewCommand);
            ShowStatistikaViewCommand = new ViewModelCommand(ExecuteShowStatistikaViewCommand);
            ShowTablicaViewCommand = new ViewModelCommand(ExecuteShowTablicaViewCommand);

            ExecuteShowHomeViewCommand(null);

            LoadCurrentUserData();
        }

        private void ExecuteShowCustomerViewCommand(object obj)
        {
            CurrentChildView = new CustomerViewModel();
            Caption = "Polaznik";
            Icon = IconChar.UserGroup;
        }

        private void ExecuteShowHomeViewCommand(object obj)
        {
            CurrentChildView = new HomeViewModel();
            Caption = "Predavač";
            Icon = IconChar.GraduationCap;
        }

        private void ExecuteShowStatistikaViewCommand(object obj)
        {
            CurrentChildView = new StatistikaViewModel();
            Caption = "Statistika";
            Icon = IconChar.PieChart;
        }

        private void ExecuteShowTablicaViewCommand(object obj)
        {
            CurrentChildView = new TablicaViewModel();
            Caption = "Tablica";
            Icon = IconChar.Table;
        }

        private void LoadCurrentUserData()
        {
            if (Thread.CurrentPrincipal?.Identity == null || string.IsNullOrEmpty(Thread.CurrentPrincipal.Identity.Name))
            {
                CurrentUserAccount.DisplayName = "Invalid user, not logged in";
                return;
            }

            var user = userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);
            if (user != null)
            {
                CurrentUserAccount.Username = user.Username;
                CurrentUserAccount.DisplayName = $"{user.Name} {user.LastName} ;)";
                CurrentUserAccount.ProfilePicture = null;
            }
            else
            {
                CurrentUserAccount.DisplayName = "Invalid user, not logged in";

            }
        }
    }
}
