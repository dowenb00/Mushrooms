using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Mushrooms
{
    public class IdentifyResultsPage : ContentPage
    {
        public IdentifyResultsPage(Mushroom m, SQLiteHandler sq)
        {
            int selected = -1;
            Mushroom selectedMushroom = new Mushroom();
            List<Mushroom> resultsList = sq.Identify(m);

            
            ListView mushroomList = new ListView();

            mushroomList.VerticalOptions = LayoutOptions.FillAndExpand;
            mushroomList.ItemsSource = resultsList;
            mushroomList.VerticalOptions = LayoutOptions.Start;
            mushroomList.ItemTemplate = new DataTemplate(typeof(MushroomViewCell));
            mushroomList.RowHeight = 130;
            //mushroomList.IsGroupingEnabled = true;
            //mushroomList.GroupHeaderTemplate = new DataTemplate(typeof(GroupLabel));

            mushroomList.ItemTapped += (sender, e) =>
            {
                selected = 0;
                selectedMushroom = resultsList[e.ItemIndex];
            };

            Button viewDetails = new Button
            {
                Text = "View Details",
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            viewDetails.Clicked += async (sender, args) =>
            {
                if (selected == -1)
                {
                    await DisplayAlert("", "Please select a mushroom to view.", "OK");
                }
                else
                {
                    await Navigation.PushAsync(new DetailPage(selectedMushroom));
                }
            };

            Button setIDButton = new Button
            {
                Text = "Save as Selected",
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            setIDButton.Clicked += async (sender, args) =>
            {
                if (selected == -1)
                {
                    await DisplayAlert("", "Please choose a mushroom from the list that matches your specimen.", "OK");
                }
                else
                {
                    var confirmed = await DisplayAlert("Confirm Identity", "Please confirm that you want to set the identity of your mushroom to \"" + selectedMushroom.CommonName + "\" (" + selectedMushroom.LatinName + ")", "Confirm", "Cancel");

                    if (confirmed)
                    {
                        Location gps = await Geolocation.GetLocationAsync();
                        string fmgps = "Lat " + gps.Latitude.ToString() + " Long " + gps.Longitude.ToString();

                        FoundMushroom fm = new FoundMushroom(fmgps, DateTime.Now, selectedMushroom.ID, true);
                        fm.mushroom = selectedMushroom;

                        await Navigation.PushAsync(new PersonalizeMushroomPage(sq, fm));
                    }
                }

            };

            if (resultsList.Count == 0)
            {
                setIDButton.IsEnabled = false;
                viewDetails.IsEnabled = false;
            }

            Label instructions = new Label
            {
                Text = "No matching mushroom? Click here to save as unidentified (You can edit it later).",
                FontSize = 20,
                VerticalOptions = LayoutOptions.EndAndExpand
            };
            Button saveUnIDButton = new Button
            {
                Text = "Save Unidentified Mushroom",
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            saveUnIDButton.Clicked += async (sender, args) =>
            {
                Location gps = await Geolocation.GetLocationAsync();
                string fmgps = "Lat " + gps.Latitude.ToString() + " Long " + gps.Longitude.ToString();

                FoundMushroom fm = new FoundMushroom(fmgps, DateTime.Now, m.ID, false);
                fm.mushroom = m;

                await Navigation.PushAsync(new PersonalizeMushroomPage(sq, fm));
            };

            StackLayout buttons = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    viewDetails,
                    setIDButton,
                }
            };
            StackLayout saveUNID = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical,
                BackgroundColor = Color.AliceBlue,
                Children =
                {
                    instructions,
                    saveUnIDButton
                }
            };

            StackLayout sl1 = new StackLayout
            {
                BackgroundColor = Color.AliceBlue,
                Children =
                {
                    mushroomList,
                    buttons,
                }
            };

            StackLayout sl2 = new StackLayout
            {
                Padding = new Thickness(10, 5, 10, 5),
                Orientation = StackOrientation.Vertical,
                Children =
                {
                    sl1,
                    saveUNID
                }

            };

            Frame frame = new Frame
            {
                BorderColor = Color.DarkRed,
                CornerRadius = 5,
                Padding = 8,
                Content = sl2
            };

            Padding = new Thickness(10, 5, 10, 5);
            this.BackgroundColor = Color.Beige;
            Content = frame;
        }
    }
}