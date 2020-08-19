using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Mushrooms
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class DisclaimerPage : ContentPage
    {
        public DisclaimerPage(SQLiteHandler sq)
        {

            Label welcomeLabel = new Label
            {
                Text = "Hello, and welcome to Mushroom Hunter, an app for identifying and saving the mushrooms you find!",
                FontSize = 20,
                HorizontalTextAlignment = TextAlignment.Center                
            };

            Button startButton = new Button
            {
                Text = "Start Hunting!",
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                TextColor = Color.Red,
                FontAttributes = FontAttributes.Bold
            };
            StackLayout sl = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                Padding = new Thickness(10, 5, 10, 5),
                HeightRequest = 300,
                Children =
                {
                    new Frame
                    {
                         BorderColor = Color.DarkRed,
                        CornerRadius = 5,
                        Padding = 8,
                        Content = new StackLayout
                        {
                            Children =
                            {
                                welcomeLabel,
                                startButton,
                                new Label
                                {
                                    Text = "*Please always exercise caution when digesting mushrooms. Do not ingest unless 100% positive what the mushroom is",
                                    FontAttributes = FontAttributes.Italic,
                                    FontSize = 10,
                                    HorizontalTextAlignment = TextAlignment.Center
                                }
                            }
                        }
                    }

                }
            };
 
            startButton.Clicked += async (sender, e) =>
            {
                //Navigation.InsertPageBefore(new TabbedPages(sq), this);
                //await Navigation.PopAsync();
                await Navigation.PushAsync(new TabbedPages(sq));
            };

            this.BackgroundColor = Color.Beige;
            Content = sl;



        }

        
    }
}