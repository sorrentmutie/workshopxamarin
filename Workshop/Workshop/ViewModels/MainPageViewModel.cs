using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Workshop.Models;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Workshop.ViewModels
{
    public class MainPageViewModel: BaseViewModel
    {
        public ObservableCollection<Monkey> Monkeys { get; }
        public Command GetMonkeysCommand { get; }

        public MainPageViewModel()
        {
            Title = "Monkeys App";
            Monkeys = new ObservableCollection<Monkey>();
            GetMonkeysCommand = new Command(async () => await GetMonkeysAsync());
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
