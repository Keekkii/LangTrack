using System.Configuration;
using System.Data;
using System.Windows;
using WpfApp2.View;

namespace WpfApp2
{
    public partial class App : Application
    {
        protected void ApplicationStart(object sender, StartupEventArgs e)
        {
            var loginView = new LoginView();
            loginView.Show();

            loginView.Closed += (s, ev) => // Kada se LoginView zatvori, osiguraj da ne pokreće ništa drugo
            {
                loginView = null; // Sprječava korištenje zatvorenog objekta
            };

            loginView.IsVisibleChanged += (s, ev) =>
            {
                if (!loginView.IsVisible && loginView.IsLoaded)
                {
                    var mainView = new MainView();
                    mainView.Show();

                    // Provjeri da li prozor nije već zatvoren
                    if (loginView != null)
                    {
                        loginView.Close();
                    }
                }
            };
        }
    }
}
