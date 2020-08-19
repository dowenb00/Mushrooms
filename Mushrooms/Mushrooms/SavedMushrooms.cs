using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Mushrooms
{
    public class SavedMushrooms : ContentPage
    {
        public SavedMushrooms(SQLiteHandler sq)
        {

            this.Title = "My Mushrooms";
            List<FoundMushroom> fmList = sq.GetFoundMushroomTable();
            fmList.Sort((p, q) => p.TimeStamp.CompareTo(q.TimeStamp));
            fmList.Reverse();

            ListView mushroomList = new ListView();
            mushroomList.VerticalOptions = LayoutOptions.FillAndExpand;
            mushroomList.ItemsSource = fmList;
            mushroomList.VerticalOptions = LayoutOptions.Start;
            mushroomList.ItemTemplate = new DataTemplate(typeof(FMViewCell));
            mushroomList.RowHeight = 100;
            //mushroomList.IsGroupingEnabled = true;
            //mushroomList.GroupHeaderTemplate = new DataTemplate(typeof(GroupLabel));

            Label noMushrooms = new Label
            {
                Text = "You don't have any saved mushrooms."
            };
            if (fmList.Count == 0)
            {
                noMushrooms.IsVisible = true;
            }
            else
            {
                noMushrooms.IsVisible = false;
            }

            mushroomList.ItemTapped += async (sender, e) =>
            {
                int index = e.ItemIndex;
                await Navigation.PushAsync(new FMDetailPage(fmList[index]));
            };

            Frame frame = new Frame
            {
                BorderColor = Color.DarkRed,
                CornerRadius = 5,
                Padding = 8,
                Content = new StackLayout
                {
                    Children = {
                        noMushrooms,
                        mushroomList
                    }
                }
            };

            Padding = new Thickness(10, 10, 10, 10);
            this.BackgroundColor = Color.BurlyWood;
            Content = frame;
        }
        
    }
}