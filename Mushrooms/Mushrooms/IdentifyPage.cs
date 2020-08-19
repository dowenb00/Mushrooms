using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Mushrooms
{
    public partial class IdentifyPage : ContentPage
    {
        public IdentifyPage(SQLiteHandler sq)
        {
            //Get list of colors from database
            List<Mushroom> fg = sq.GetMushroomTable();

            List<string> capColors = new List<string>();
            List<string> undersideColors = new List<string>();

            foreach (Mushroom m in fg)
            {
                if (m.FieldGuide)
                {
                    string[] ccs = m.CapColor.Split(',');
                    string[] ucs = m.UndersideColor.Split(',');
                    foreach (string s in ccs)
                    {
                        capColors.Add(s);
                    }
                    foreach (string s in ucs)
                    {
                        undersideColors.Add(s);
                    }
                }
            }
            capColors = capColors.Distinct().ToList();
            capColors.Sort();
            undersideColors = undersideColors.Distinct().ToList();
            undersideColors.Sort();


            //directions label placed inside a frame 

            Frame welcomeInfo = new Frame
            {
                BorderColor = Color.DarkRed,
                CornerRadius = 5,
                Padding = 8,
                Content = new StackLayout
                {
                    Children =
                    {
                        new Label
                         {
                            Text = "Welcome!",
                            FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                            FontAttributes = FontAttributes.Bold,
                            FontFamily = "Courier",

                        },
                        new BoxView
                        {
                            Color = Color.Black,
                            HeightRequest = 2,
                            HorizontalOptions = LayoutOptions.Fill
                        },
                        new Label
                        {
                            Text = "Please enter the following information below to help classify your mushroom. Click save when finished to view results."
                        }
                    }
                }
            };

            //user controls


            //switch for yes or no for stem

            Label switchLabel1 = new Label
            {
                Text = "Does the mushroom have a stem?"
            };

            Switch stemSwitch = new Switch
            {
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                OnColor = Color.Green,
            };

            stemSwitch.Toggled += (sender, e) =>
            {
                //  eventValue.Text = String.Format("Switch is now {0}", e.Value);
                //  pageValue.Text = gills.IsToggled.ToString();
            };

            // picker for cap color

            Picker capColor = new Picker
            {
                Title = "Pick a cap color",
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            
            foreach (string c in capColors)
            {
                capColor.Items.Add(c);
            }
            Label label3 = new Label
            {
                Text = " "
            };
            capColor.SelectedIndexChanged += (sender, args) =>
            {
                label3.Text = capColor.Items[capColor.SelectedIndex];
            };

            //Picker for underside type
            Picker undersideType = new Picker
            {
                Title = "What type of underside does the mushroom have?",
                VerticalOptions = LayoutOptions.CenterAndExpand,
            };

            var undersideOptions = new List<string>
            {
                "Gills",
                "Pores",
                "Teeth",
                "(No underside)"
            };

            foreach (string c in undersideOptions)
            {
                undersideType.Items.Add(c);
            }

            undersideType.SelectedIndexChanged += (sender, args) =>
            {
                label3.Text = undersideType.Items[undersideType.SelectedIndex];
            };



            // picker for underside color
            Picker undersideColor = new Picker
            {
                Title = "What color is the underside?",
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            foreach (string c in undersideColors)
            {
                undersideColor.Items.Add(c);
            }

            undersideColor.SelectedIndexChanged += (sender, args) =>
            {
                label3.Text = undersideColor.Items[undersideColor.SelectedIndex];
            };


            //picker for veil 
            Picker veilRemnant = new Picker
            {
                Title = "What type of veil remnant does the mushroom have?",
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            var options2 = new List<string>
            {
                "Universal",
                "Ring",
                "None"
            };

            foreach (string optionName in options2)
            {
                veilRemnant.Items.Add(optionName);
            }

            veilRemnant.SelectedIndexChanged += (sender, args) =>
            {
                label3.Text = veilRemnant.Items[veilRemnant.SelectedIndex];
            };


            //switch for yes or no for scales

            Label switchLabel2 = new Label
            {
                Text = "Does it have scales?"
            };

            Switch scalesSwitch = new Switch
            {
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                OnColor = Color.Green,
            };

            scalesSwitch.Toggled += (sender, e) =>
            {
                // eventValue.Text = String.Format("Switch is now {0}", e.Value);
                // pageValue.Text = scales.IsToggled.ToString();
            };

            //save button
            Button saveButton = new Button
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Text = "Submit",
                TextColor = Color.Red,
                FontAttributes = FontAttributes.Bold
            };

            saveButton.Clicked += async (sender, e) =>
            {
                //await DisplayAlert("Submit?", "Press OK to submit", "OK");
                Mushroom m = new Mushroom();
                if (capColor.SelectedItem != null)
                {
                    m.CapColor = capColor.SelectedItem.ToString();
                }
                if (undersideType.SelectedItem != null)
                {
                    m.Underside = undersideType.SelectedItem.ToString();
                }
                if (veilRemnant.SelectedItem != null)
                {
                    m.Veil = veilRemnant.SelectedItem.ToString();
                }
                if (undersideColor.SelectedItem != null)
                {
                    m.UndersideColor = undersideColor.SelectedItem.ToString();
                }
                m.HasStem = stemSwitch.IsToggled;
                m.HasScales = scalesSwitch.IsToggled;
                
                await Navigation.PushAsync(new IdentifyResultsPage(m, sq));
                //Get all user inputs and save them in a new Mushroom object.
                //Pass object to new IdentifyResultsPage()
            };

            StackLayout stackLayout1 = new StackLayout
            {
                Padding = new Thickness(10, 5, 10, 5),
                Children =
                {
                    welcomeInfo,
                  //  eventValue,
                  //  pageValue,
                    switchLabel1,
                    stemSwitch,
                    capColor,
                    undersideType,
                    undersideColor,
                    veilRemnant,
                    //label3,
                    switchLabel2,
                    scalesSwitch,
                    saveButton,
                }
            };

            //background color
            this.BackgroundColor = Color.Beige;
            this.Content = stackLayout1;
        }
    }
}