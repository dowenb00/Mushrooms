using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Mushrooms
{
    public class SearchDetailPage : ContentPage
    {
        public SearchDetailPage(SQLiteHandler sq, Mushroom m)
        {
            string capString = "";
            string undersideString = "";
            string[] str = m.CapColor.Split(',');
            foreach (string s in str)
            {
                capString += (s + ", ");
            }
            capString = capString.Substring(0, capString.Length - 2);
            str = m.UndersideColor.Split(',');
            foreach (string s in str)
            {
                undersideString += (s + ", ");
            }
            undersideString = undersideString.Substring(0, undersideString.Length - 2);
            Label Details = new Label
            {
                Text = "Here is more information listed for you...",
                FontAttributes = FontAttributes.Bold,
                FontSize = 30

            };

            //set to labels user input + image to showcase object

            Label capColor = new Label
            {
                Text = "Cap color: " + capString
            };

            Label undersideColor = new Label
            {
                Text = "Underside color: " + undersideString
            };

            Label stem = new Label
            {

            };
            if (m.HasStem)
            {
                stem.Text = "Stem: Present";
            }
            else
            {
                stem.Text = "Stem: Not present";
            }

            Label scales = new Label
            {
            };
            if (m.HasStem)
            {
                scales.Text = "Scales: Not present";
            }
            else
            {
                scales.Text = "Scales: present";
            }
            Label veil = new Label
            {
                Text = "Veil remnant type: " + m.Veil
            };

            Label CommonName = new Label
            {
                Text = "Common Name: " + m.CommonName
            };
            Label LatinName = new Label
            {
                Text = "Latin Name: " + m.LatinName
            };
            Label Edibility = new Label
            {
                Text = "Edibility: " + m.Edibility
            };
            Label Underside = new Label
            {
                Text = "Underside Type: " + m.Underside
            };
            Image picture = new Image
            {
                Source = m.ImageName,
                Aspect = Aspect.AspectFit,
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Fill
            };

            Frame Frame1 = new Frame
            {
                Content = CommonName,
                CornerRadius = 10,
                HasShadow = true,
                BorderColor = Color.Red,
            };

            Frame Frame2 = new Frame
            {
                Content = LatinName,
                CornerRadius = 10,
                HasShadow = true,
                BorderColor = Color.Red,
            };

            Frame Frame3 = new Frame
            {
                Content = capColor,
                CornerRadius = 10,
                HasShadow = true,
                BorderColor = Color.Red,
            };

            Frame Frame4 = new Frame
            {
                Content = Underside,
                CornerRadius = 10,
                HasShadow = true,
                BorderColor = Color.Red,
            };

            Frame Frame5 = new Frame
            {
                Content = undersideColor,
                CornerRadius = 10,
                HasShadow = true,
                BorderColor = Color.Red
            };

            Frame Frame6 = new Frame
            {
                Content = veil,
                CornerRadius = 10,
                HasShadow = true,
                BorderColor = Color.Red

            };
            Frame Frame7 = new Frame
            {
                Content = stem,
                CornerRadius = 10,
                HasShadow = true,
                BorderColor = Color.Red

            };
            Frame Frame8 = new Frame
            {
                Content = scales,
                CornerRadius = 10,
                HasShadow = true,
                BorderColor = Color.Red

            };
            Frame Frame9 = new Frame
            {
                Content = Edibility,
                CornerRadius = 10,
                HasShadow = true,
                BorderColor = Color.Red
            };
            Frame Frame10 = new Frame
            {
                Content = picture,
                CornerRadius = 10,
                HasShadow = true,
                BorderColor = Color.Red
            };

            Label confirmLabel = new Label
            {
                Text = "Is this your mushroom?",
                FontSize = 20
            };

            Button yesButton = new Button
            {
                Text = "Yes",
                
            };
            yesButton.Clicked += async (sender, args) =>
            {
                Location gps = await Geolocation.GetLocationAsync();
                string fmgps = "Lat " + gps.Latitude.ToString() + " Long " + gps.Longitude.ToString();

                FoundMushroom fm = new FoundMushroom(fmgps, DateTime.Now, m.ID, true);
                fm.mushroom = m;
                await Navigation.PushAsync(new PersonalizeMushroomPage(sq, fm));
            };
            Button noButton = new Button
            {
                Text = "No",

            };
            noButton.Clicked += async (sender, args) =>
            {
                await Navigation.PopAsync();
            };

            StackLayout s2 = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    yesButton,
                    noButton
                }
            };

            ScrollView sv = new ScrollView
            {
                Content = new StackLayout
                {
                    Padding = new Thickness(10, 5, 10, 5),
                    Children =
                    {
                        confirmLabel,
                        s2,
                        Frame10,
                        Frame1,
                        Frame2,
                        Frame3,
                        Frame4,
                        Frame5,
                        Frame6,
                        Frame7,
                        Frame8,
                        Frame9,
                    }
                }
            };

            this.Content = sv;
            this.BackgroundColor = Color.Beige;
        }
    }
}