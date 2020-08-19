using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Mushrooms
{
    public class FMDetailPage : ContentPage
    {
        public FMDetailPage(FoundMushroom fm)
        {
            string capString = "";
            string undersideString = "";
            if(fm.mushroom.CapColor != null)
            {
                string[] str = fm.mushroom.CapColor.Split(',');
                foreach (string s in str)
                {
                    capString += (s + ", ");
                }
                capString = capString.Substring(0, capString.Length - 2);
            }
            if (fm.mushroom.UndersideColor != null)
            {
                string[] str = fm.mushroom.UndersideColor.Split(',');
                foreach (string s in str)
                {
                    undersideString += (s + ", ");
                }
                undersideString = undersideString.Substring(0, undersideString.Length - 2);
            }

            Label Details = new Label
            {
                Text = "Here is more information listed for you...",
                FontAttributes = FontAttributes.Bold,
                FontSize = 30

            };

            //set to labels user input + image to showcase object
            Label foundDate = new Label
            {
                Text = "Found on: " + fm.TimeStamp.ToLongDateString()
            };
            Label location = new Label
            {
                Text = "Location: " + fm.GPS
            };

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
            if (fm.mushroom.HasStem)
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
            if (fm.mushroom.HasStem)
            {
                scales.Text = "Scales: Not present";
            }
            else
            {
                scales.Text = "Scales: present";
            }
            Label veil = new Label
            {
                Text = "Veil remnant type: " + fm.mushroom.Veil
            };

            Label CommonName = new Label
            {
                Text = "Common Name: " + fm.mushroom.CommonName
            };
            Label LatinName = new Label
            {
                Text = "Latin Name: " + fm.mushroom.LatinName
            };
            Label Edibility = new Label
            {
                Text = "Edibility: " + fm.mushroom.Edibility
            };
            Label Underside = new Label
            {
                Text = "Underside Type: " + fm.mushroom.Underside
            };
            Image picture = new Image
            {
                Source = fm.Photo,
                Aspect = Aspect.AspectFit,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };
            Label notes = new Label
            {
                Text = "Additional notes: " + fm.Notes
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

            Frame Frame11 = new Frame
            {
                Content = new StackLayout
                {
                    Children = {
                        foundDate,
                        location,
                        notes
                    }
                },
                CornerRadius = 10,
                HasShadow = true,
                BorderColor = Color.Green
            };

            ScrollView sv = new ScrollView
            {
                Content = new StackLayout
                {
                    Padding = new Thickness(10, 5, 10, 5),
                    Children =
                    {
                        Frame10,
                        Frame11,
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