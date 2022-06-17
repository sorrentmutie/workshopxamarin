using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workshop.Models;
using Workshop.Views;
using Xamarin.Forms;

namespace Workshop
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var monkey = e.CurrentSelection.FirstOrDefault() as Monkey;
            if (monkey == null) return;
            await Navigation.PushAsync(new MonkeyDetailsPage(monkey));
            ((CollectionView)sender).SelectedItem = null;
        }
    }
}
