using Microsoft.Maui.Controls;

namespace OdevGorsel
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            
            if (Preferences.ContainsKey("DarkModeEnabled"))
            {
                bool isDarkMode = Preferences.Get("DarkModeEnabled", false);
                Current.UserAppTheme = isDarkMode ? AppTheme.Dark : AppTheme.Light;
            }

            MainPage = new AppShell();
        }
    }
}
