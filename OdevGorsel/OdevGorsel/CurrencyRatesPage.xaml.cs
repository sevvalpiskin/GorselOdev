using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text.Json;
using Microsoft.Maui.Controls;

namespace OdevGorsel
{
    public partial class CurrencyRatesPage : ContentPage
    {
        // D�viz kurlar� koleksiyonu
        public ObservableCollection<CurrencyRate> Rates { get; set; } = new ObservableCollection<CurrencyRate>();

        private readonly string apiUrl = "https://finans.truncgil.com/today.json";

        public CurrencyRatesPage()
        {
            InitializeComponent();

            // CollectionView veri kayna��
            RatesCollectionView.ItemsSource = Rates;

            // D�viz kurlar�n� y�kle
            LoadRates();
        }

        // D�viz kurlar�n� API'den y�kler
        private async void LoadRates()
        {
            try
            {
                using var client = new HttpClient();
                var response = await client.GetStringAsync(apiUrl);

                // API'den gelen JSON'u deserialize et
                var jsonDocument = JsonDocument.Parse(response);
                var root = jsonDocument.RootElement;

                // Rates listesini temizle ve yeni verileri ekle
                Rates.Clear();

                foreach (var currency in root.EnumerateObject())
                {
                    // Sadece d�viz bilgilerini i�ler (�rnek olarak "USD", "EUR" gibi)
                    if (currency.Name == "Update_Date" || !currency.Value.TryGetProperty("Buying", out _))
                        continue;

                    Rates.Add(new CurrencyRate
                    {
                        Type = currency.Name,
                        Buy = currency.Value.GetProperty("Buying").GetString(),
                        Sell = currency.Value.GetProperty("Selling").GetString(),
                        Change = currency.Value.GetProperty("Change").GetString(),
                        ChangeColor = currency.Value.GetProperty("Change").GetString().Contains("-") ? Colors.Red : Colors.Green
                    });
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Hata", $"D�viz bilgileri al�namad�: {ex.Message}", "Tamam");
            }
        }

        // Yenileme butonuna t�kland���nda �a�r�l�r
        private void OnRefreshClicked(object sender, EventArgs e)
        {
            LoadRates();
        }
    }

    
    public class CurrencyRate
    {
        public string Type { get; set; } 
        public string Buy { get; set; } 
        public string Sell { get; set; } 
        public string Change { get; set; } 
        public Color ChangeColor { get; set; } 
    }
}
