using Microsoft.Maui.Controls;

namespace OdevGorsel
{
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            string username = UsernameEntry?.Text?.Trim() ?? string.Empty;
            string email = EmailEntry?.Text?.Trim() ?? string.Empty;
            string password = PasswordEntry?.Text?.Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("Hata", "T�m alanlar doldurulmal�d�r!", "Tamam");
                return;
            }

            await DisplayAlert("Ba�ar�l�", "Kay�t ba�ar�l�!", "Tamam");
            await Navigation.PushAsync(new LoginPage());
        }

        private async void OnNavigateToLogin(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }
    }
}
