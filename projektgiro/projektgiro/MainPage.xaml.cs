using projektgiro.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace projektgiro
{
    public partial class MainPage : ContentPage
    {
        private static List<ImageButton> moodButtons = new List<ImageButton>()
        {
            new ImageButton
            {
                 Source = "good.png",
            },
            new ImageButton
            {
                Source = "mid.png"
            },
            new ImageButton
            {
                Source = "meh.png"
            },
            new ImageButton
            {
                Source = "sad.png"
            },
            new ImageButton
            {
                Source = "bad.png"
            }
        };

        public MainPage()
        {
            InitializeComponent();

            for (int i = 0; i < moodButtons.Count; i++)
            {
                //moodButtons[i].Text = Enum.GetName(typeof(MoodEnum), i);
                buttonsGrid.Children.Add(moodButtons[i]);
                Grid.SetColumn(moodButtons[i], i);
                moodButtons[i].Clicked += MoodButtonClicked;
            }

            var lastDayMoodTask = App.Database.GetLastDayMood();
            var lastDayMood = lastDayMoodTask.Result;

            if (lastDayMood != null)
            {
                lastDayDateLabel.Text = lastDayMood.Date.ToString("MM/dd/yyyy");
                lastDayMoodLabel.Text = lastDayMood.Mood.ToString();
            }
            else
                lastDayDateLabel.Text = "Brak danych.";
        }

        private async void MoodButtonClicked(object sender, EventArgs e)
        {
            if (datePicker.Date <= DateTime.Now)
            {
                var column = Grid.GetColumn((Button)sender);
                var dayMood = new DayMood();
                dayMood.Mood = (MoodEnum)Enum.Parse(typeof(MoodEnum), column.ToString());
                dayMood.Date = datePicker.Date;
                await App.Database.InsertDayMoodAsync(dayMood);
                DisableButtons(column);
            }
            else
                await DisplayAlert("Błąd", "Niepoprawana data", "OK");
        }

        private void DisableButtons(int buttonId)
        {
            EnableButtons();

            for (int i = 0; i < moodButtons.Count; i++)
            {
                if (i == buttonId)
                {
                    moodButtons[i].BackgroundColor = Color.Red;
                }

                moodButtons[i].IsEnabled = false;
            }
        }

        private void EnableButtons()
        {
            foreach (var button in moodButtons)
            {
                button.IsEnabled = true;
                button.BackgroundColor = Color.Transparent;
            }
        }

        private async void datePickerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var dayMood = await App.Database.GetDayMood(datePicker.Date);
            if (dayMood != null)
                DisableButtons((int)dayMood.Mood);
            else
                EnableButtons();
        }

        private void SeeMoreDataClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DayMoods());
        }
    }
}
