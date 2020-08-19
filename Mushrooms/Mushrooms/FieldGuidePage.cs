using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Mushrooms
{
    public class FieldGuidePage : ContentPage
    {
        public FieldGuidePage(SQLiteHandler sq)
        {
            this.Title = "Field Guide";
            List<Mushroom> allMushrooms = sq.GetMushroomTable();
            List<Mushroom> mushrooms = new List<Mushroom>();
            foreach (Mushroom m in allMushrooms)
            {
                if (m.FieldGuide)
                {
                    mushrooms.Add(m);
                }
            }
            mushrooms.Sort((p, q) => p.LatinName.CompareTo(q.LatinName));

            ListView mushroomList = new ListView();

            mushroomList.ItemsSource = mushrooms;
            mushroomList.VerticalOptions = LayoutOptions.Start;
            mushroomList.ItemTemplate = new DataTemplate(typeof(MushroomViewCell));
            //mushroomList.BackgroundColor = Color.Azure;
            mushroomList.RowHeight = 100;
            //mushroomList.IsGroupingEnabled = true;
            //mushroomList.GroupHeaderTemplate = new DataTemplate(typeof(GroupLabel));

            mushroomList.ItemTapped += async (sender, e) =>
            {
                Mushroom m = mushrooms[e.ItemIndex];
                await Navigation.PushAsync(new NavigationPage(new DetailPage(m)));
            };

            StackLayout sl = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(10, 10, 10, 10),
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
                                mushroomList
                            }
                        }
                    }

                }
            };

            this.BackgroundColor = Color.Beige;
            Content = sl;
        }
    }
}