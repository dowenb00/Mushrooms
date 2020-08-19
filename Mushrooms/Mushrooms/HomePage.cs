using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Syncfusion.SfAutoComplete.XForms;

namespace Mushrooms
{
    public class HomePage : ContentPage
    {

        public HomePage(SQLiteHandler sq)
        {
            this.Title = "Add a Mushroom";

            List<Mushroom> ms = sq.GetMushroomTable();
            List<string> names = new List<string>();
            foreach (Mushroom m in ms)
            {
                if (m.FieldGuide)
                {
                    names.Add(m.CommonName);
                    names.Add(m.LatinName);
                }

            }

            SfAutoComplete mushroomEntry = new SfAutoComplete();
            mushroomEntry.AutoCompleteSource = names;
            mushroomEntry.SuggestionMode = SuggestionMode.Contains;
            mushroomEntry.DropDownCornerRadius = 5;
            mushroomEntry.DropDownBackgroundColor = Color.AntiqueWhite;
            mushroomEntry.Watermark = "Search Here";

            Label questionLabel = new Label
            {
                Text = "Do you already know what your mushroom is? If so, type the name into the search bar below..",
                FontSize = 20
            };

            Button submitButton = new Button
            {
                Text = "Submit",
                TextColor = Color.Red,
            };

            submitButton.Clicked += async (sender, args) =>
            {
                string query = mushroomEntry.Text.ToLower();
                if (validateInput(query))
                {
                    List<Mushroom> match = sq.GetMushroom(query);
                    if(match.Count == 1)
                    {
                        mushroomEntry.Text = "";
                        await Navigation.PushAsync(new SearchDetailPage(sq, match[0]));
                    }
                    else
                    {
                        await DisplayAlert("", "Mushroom Hunter does not have a mushroom matching this name.", "OK");
                    }
                    
                }
            };
            Label instr2 = new Label
            {
                Text = "Not sure what kind of mushroom you have? Select \"Identify\" to find out. ",
                FontSize = 20
            };
            Button button1 = new Button
            {
                Text = "Identify",
                TextColor = Color.Red,
            };

            button1.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(new IdentifyPage(sq));
            };
            Button button2 = new Button
            {
                Text = "Go to Field Guide page"
            };

            button2.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(new FieldGuidePage(sq));
            };
            StackLayout sl1 = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                Padding = new Thickness(10, 5, 10, 5),
                Children =
                {
                    questionLabel,
                    mushroomEntry,
                    submitButton
                }
            };

            StackLayout sl2 = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                Padding = new Thickness(10, 5, 10, 5),
                Children = {

                    instr2,
                    button1
                }
            };
            Frame frame1 = new Frame
            {
                BorderColor = Color.DarkRed,
                CornerRadius = 10,
                Padding = 8,
                Content = sl1
            };
            Frame frame2 = new Frame
            {
                BorderColor = Color.DarkRed,
                CornerRadius = 10,
                Padding = 8,
                Content = sl2
            };

            Grid grid = new Grid
            {
                BackgroundColor = Color.Beige,
                RowDefinitions =
                {
                    new RowDefinition{ Height = new GridLength(8, GridUnitType.Star)},
                    new RowDefinition{ Height = new GridLength(1, GridUnitType.Star)},
                    new RowDefinition{ Height = new GridLength(8, GridUnitType.Star)}
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition{ Width = GridLength.Auto}
                }
            };
            grid.Children.Add(frame1, 0, 0);
            grid.Children.Add(frame2, 0, 2);

            StackLayout sl3 = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    grid
                }
            };

            Padding = new Thickness(30, 55);
            BackgroundColor = Color.Beige;
            Content = grid;
        }

        public bool validateInput(string input)
        {
            //Code for validation here
            return true;
        }
    }
}