using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace projektgiro
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DayMoods : ContentPage
    {
        public DayMoods()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            dayMoodsLists.ItemsSource = await App.Database.GetDayMoods();
        }
    }
}