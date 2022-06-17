using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Workshop.Models;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Linq;

namespace Workshop.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public ObservableCollection<Monkey> Monkeys { get; }
        public Command GetMonkeysCommand { get; }
        public Command GetClosestMonkeyCommand { get; set; }

        public MainPageViewModel()
        {
            Title = "Monkeys App";
            Monkeys = new ObservableCollection<Monkey>();
            GetMonkeysCommand = new Command(async () => await GetMonkeysAsync());
            GetClosestMonkeyCommand = new Command(async () => await GetClosestMonkeyAsync());
        }

        private async Task GetClosestMonkeyAsync()
        {
            var myPosition = await Geolocation.GetLastKnownLocationAsync();
            if (myPosition == null)
            {
                myPosition = await Geolocation.GetLocationAsync(
                    new GeolocationRequest
                    {
                        DesiredAccuracy = GeolocationAccuracy.Default,
                        Timeout = TimeSpan.FromSeconds(30)
                    });
            }
            var nearestMonkey = Monkeys.OrderBy(
                monkey => myPosition.CalculateDistance(
                    new Location(monkey.Latitude, monkey.Longitude), DistanceUnits.Kilometers))
                .FirstOrDefault();
            await Application.Current.MainPage.DisplayAlert("Nearest Monkey", nearestMonkey.Name + " " + nearestMonkey.Location, "OK");
            await Map.OpenAsync(nearestMonkey.Latitude, nearestMonkey.Longitude);
        }

        async Task GetMonkeysAsync()
        {
            if (IsLoading) return;

            try
            {
                IsLoading = true;

                var httpClient = new HttpClient();
                var json = await httpClient.GetStringAsync("https://montemagno.com/monkeys.json");
                var monkeys = JsonConvert.DeserializeObject<List<Monkey>>(json);
                Monkeys.Clear();
                foreach (var monkey in monkeys)
                {
                    Monkeys.Add(monkey);
                }

            }
            catch (Exception ex)
            {

                await Application.Current.MainPage.DisplayAlert("Errore!!!", ex.Message, "OK");
            }
            finally
            {
                IsLoading = false;
            }


        }

    }
}
