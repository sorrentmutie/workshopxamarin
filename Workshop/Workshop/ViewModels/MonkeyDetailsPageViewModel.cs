using System;
using System.Collections.Generic;
using System.Text;
using Workshop.Models;

namespace Workshop.ViewModels
{
    public class MonkeyDetailsPageViewModel: BaseViewModel
    {
        public MonkeyDetailsPageViewModel(Monkey monkey)
        {
            Title = "Details Page";
            SelectedMonkey = monkey;
        }

        Monkey monkey;
        public Monkey SelectedMonkey
        {
            get => monkey;
            set
            {
                if (monkey == value) return;
                monkey = value;
                OnPropertyChanged();
            }
        }
    }
}
