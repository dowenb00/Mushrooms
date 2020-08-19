using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Mushrooms
{
    public class PersonalizeMushroomPage : ContentPage
    {
        public PersonalizeMushroomPage(SQLiteHandler sq, FoundMushroom fm)
        {
            Label label = new Label
            {
                Text = "Additional Details",
                FontSize = 30,
                HorizontalOptions = LayoutOptions.Center
            };
            Button takePhoto = new Button
            {
                Text = "Add Photo"
            };

            Entry notesEntry = new Entry
            {
                Placeholder = "Additional Notes"
            };

            takePhoto.Clicked += async (sender, args) =>
            {
                await CrossMedia.Current.Initialize();
                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await DisplayAlert("No Camera", ":( No camera available.", "OK");
                    return;
                }

                Plugin.Media.Abstractions.StoreCameraMediaOptions Options = new Plugin.Media.Abstractions.StoreCameraMediaOptions()
                {
                    CompressionQuality = 50
                };
                var photo = await CrossMedia.Current.TakePhotoAsync(Options);
                string name = fm.CommonName + fm.TimeStamp.Ticks;
                fm.Photo = DependencyService.Get<IFileService>().SavePicture(name, photo.GetStream(), "imagesFolder");
            };

            Button saveButton = new Button
            {
                Text = "Save"
            };
            saveButton.Clicked += async (sender, args) =>
            {
                if (fm.Photo == null)
                {
                    fm.Photo = "empty";
                }
                fm.Notes = notesEntry.Text;
                if (fm.Identified)
                {
                    sq.AddFoundMushroom(fm);
                    await DisplayAlert("", "Mushroom saved as \"" + fm.mushroom.CommonName + "\" (" + fm.mushroom.LatinName + ")\n\n" + "Date: " + fm.TimeStamp.ToShortDateString() + "\n\n" + "Location: " + fm.GPS, "OK");
                }
                else
                {
                    sq.AddNewMushroom(fm);
                    await DisplayAlert("", "Mushroom saved as \"Unknown\" \n\n" + "Date: " + fm.TimeStamp.ToShortDateString() + "\n\n" + "Location: " + fm.GPS, "OK");
                }
                await Navigation.PushAsync(new TabbedPages(sq));

            };

            Frame Frame1 = new Frame
            {
                Content = new StackLayout
                {
                    Children =
                    {
                        notesEntry,
                        takePhoto,
                    }
                },
                CornerRadius = 10,
                HasShadow = true,
                BorderColor = Color.Red,
            };
            StackLayout sl = new StackLayout
            {
                Children =
                {
                    label,
                    Frame1,
                    saveButton
                }
            };

            BackgroundColor = Color.Beige;
            Padding = new Thickness(20);
            Content = sl;
            
        }
    }
}