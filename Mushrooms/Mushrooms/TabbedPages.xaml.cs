using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mushrooms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedPages : TabbedPage
    {
        public TabbedPages(SQLiteHandler sq)
        {
            Page homePage = new HomePage(sq);
            homePage.Title = "Add a Mushroom";
            Page fieldGuide = new FieldGuidePage(sq);
            fieldGuide.Title = "Field Guide";
            Page myMushrooms = new SavedMushrooms(sq);
            myMushrooms.Title = "My Mushrooms";

            Children.Add(homePage);
            Children.Add(fieldGuide);
            Children.Add(myMushrooms);
        }

    }
}