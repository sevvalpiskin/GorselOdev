using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Xml.Linq;
using Microsoft.Maui.Controls;

namespace OdevGorsel
{
    public partial class HaberlerPage : ContentPage
    {
        public ObservableCollection<Kategori> Kategoriler { get; set; }
        public ObservableCollection<Haber> Haberler { get; set; }

        public HaberlerPage()
        {
            InitializeComponent();

            
            Kategoriler = new ObservableCollection<Kategori>
            {
                new Kategori { Baslik = "Gündem", Link = "https://www.trthaber.com/gundem_articles.rss" },
                new Kategori { Baslik = "Ekonomi", Link = "https://www.trthaber.com/ekonomi_articles.rss" },
                new Kategori { Baslik = "Spor", Link = "https://www.trthaber.com/spor_articles.rss" },
                new Kategori { Baslik = "Bilim Teknoloji", Link = "https://www.trthaber.com/bilim_teknoloji_articles.rss" },
                new Kategori { Baslik = "Güncel", Link = "https://www.trthaber.com/guncel_articles.rss" }
            };

            Haberler = new ObservableCollection<Haber>();

            KategoriCollectionView.ItemsSource = Kategoriler;
            HaberCollectionView.ItemsSource = Haberler;
        }

        private async void OnKategoriSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count == 0) return;

            var selectedKategori = (Kategori)e.CurrentSelection[0];
            await LoadHaberler(selectedKategori.Link);
        }

        private async Task LoadHaberler(string rssLink)
        {
            try
            {
                using var client = new HttpClient();
                var rss = await client.GetStringAsync(rssLink);
                var doc = XDocument.Parse(rss);

                Haberler.Clear();

                foreach (var item in doc.Descendants("item"))
                {
                    var haber = new Haber
                    {
                        Title = item.Element("title")?.Value,
                        Description = item.Element("description")?.Value,
                        Link = item.Element("link")?.Value,
                        PublishDate = item.Element("pubDate")?.Value,
                        Image = item.Descendants("image").FirstOrDefault()?.Value // Eðer resim varsa
                    };

                    Haberler.Add(haber);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Hata", $"Haberler yüklenirken bir hata oluþtu: {ex.Message}", "Tamam");
            }
        }

        private async void OnShareClicked(object sender, EventArgs e)
        {
            var link = (string)((Button)sender).CommandParameter;
            await Share.RequestAsync(new ShareTextRequest
            {
                Uri = link,
                Title = "Haber Paylaþýmý"
            });
        }

        private void OnRefreshClicked(object sender, EventArgs e)
        {
            
            if (KategoriCollectionView.SelectedItem is Kategori selectedKategori)
            {
                LoadHaberler(selectedKategori.Link);
            }
        }
    }

    public class Kategori
    {
        public string Baslik { get; set; }
        public string Link { get; set; }
        public Color BackgroundColor { get; set; } = Colors.LightGray;
    }

    public class Haber
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string PublishDate { get; set; }
        public string Image { get; set; }
    }
}
