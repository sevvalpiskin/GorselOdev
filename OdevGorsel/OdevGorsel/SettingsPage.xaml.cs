using Microsoft.Maui.Controls;

namespace OdevGorsel
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();

            
            if (Preferences.ContainsKey("DarkModeEnabled"))
            {
                DarkModeSwitch.IsToggled = Preferences.Get("DarkModeEnabled", false);
            }
        }

        private void OnDarkModeToggled(object sender, ToggledEventArgs e)
        {
            
            Preferences.Set("DarkModeEnabled", e.Value);

            
            Application.Current.UserAppTheme = e.Value ? AppTheme.Dark : AppTheme.Light;
        }
    }
}
