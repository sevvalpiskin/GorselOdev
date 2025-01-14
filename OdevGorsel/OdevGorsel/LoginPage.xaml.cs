using Microsoft.Maui.Controls;
using OdevGorsel;

namespace OdevGorsel
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }


        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string email = EmailEntry?.Text?.Trim() ?? string.Empty;
            string password = PasswordEntry?.Text?.Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("Hata", "Email ve �ifre bo� b�rak�lamaz!", "Tamam");
                return;
            }

            
            bool loginSuccess = true; 

            if (loginSuccess)
            {
                await Navigation.PushAsync(new HomePage());
            }
            else
            {
                await DisplayAlert("Hata", "Giri� ba�ar�s�z. Bilgilerinizi kontrol edin.", "Tamam");
            }
        }

        private async void OnNavigateToRegister(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }
    }
}

