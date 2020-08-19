using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Mushrooms
{
    public class FMViewCell : ViewCell
    {
        public FMViewCell()
        {
            Image mushroomPic = new Image
            {
                Aspect = Aspect.AspectFit,
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Fill
            };
            mushroomPic.SetBinding(Image.SourceProperty, "Photo");

            Label mushroomName = new Label
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                FontSize = 21,
            };
            mushroomName.SetBinding(Label.TextProperty, "CommonName");

            Label dateLabel = new Label
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                FontSize = 14
            };
            dateLabel.SetBinding(Label.TextProperty, "DateString");


            StackLayout namesStack = new StackLayout
            {
                Margin = 10,
                VerticalOptions = LayoutOptions.Center,
                Orientation = StackOrientation.Vertical,
                Children =
                    {
                        mushroomName,
                        dateLabel
                    }
            };

            Grid grid = new Grid
            {
                Margin = new Thickness(0, 2),
                BackgroundColor = Color.Beige,
                HeightRequest = 200,
                RowDefinitions =
                    {
                        new RowDefinition { Height = GridLength.Auto }
                    },
                ColumnDefinitions =
                    {
                        new ColumnDefinition{Width = new GridLength(1, GridUnitType.Star)},
                        new ColumnDefinition{Width = new GridLength(3, GridUnitType.Star)}
                    }
            };

            grid.Children.Add(namesStack, 1, 0);
            grid.Children.Add(mushroomPic, 0, 0);
            View = grid;
        }

    }
}

