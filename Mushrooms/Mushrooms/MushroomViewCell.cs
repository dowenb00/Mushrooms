using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Mushrooms
{
    class MushroomViewCell : ViewCell
    {
        public MushroomViewCell()
        {
            Image mushroomPic = new Image
            {
                Aspect = Aspect.AspectFit,
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Fill
            };
            mushroomPic.SetBinding(Image.SourceProperty, "ImageName");

            Label mushroomName = new Label
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                FontSize = 21,
            };
            mushroomName.SetBinding(Label.TextProperty, "CommonName");

            Label latinName = new Label
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                FontSize = 14
            };
            latinName.SetBinding(Label.TextProperty, "LatinName");


            StackLayout namesStack = new StackLayout
            {
                Margin = 10,
                VerticalOptions = LayoutOptions.Center,
                Orientation = StackOrientation.Vertical,
                Children =
                    {
                        mushroomName,
                        latinName
                    }
            };

            Grid grid = new Grid
            {
                BackgroundColor = Color.Bisque,
                Margin = new Thickness(0, 2),
                HeightRequest = 200,
                RowDefinitions =
                    {
                        new RowDefinition { Height = GridLength.Auto }
                    },
                ColumnDefinitions =
                    {
                        new ColumnDefinition{Width = new GridLength(1, GridUnitType.Star)},
                        new ColumnDefinition{Width = new GridLength(2, GridUnitType.Star)}
                    }
            };

            grid.Children.Add(namesStack, 1, 0);
            grid.Children.Add(mushroomPic, 0, 0);
            View = grid;
        }

    }
}

