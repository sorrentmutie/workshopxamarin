using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workshop.Models;
using Workshop.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Workshop.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MonkeyDetailsPage : ContentPage
    {
        public MonkeyDetailsPage()
        {
            InitializeComponent();
        }

        public MonkeyDetailsPage(Monkey monkey)
        {
            InitializeComponent();
            BindingContext = new MonkeyDetailsPageViewModel(monkey);
        }
    }
}