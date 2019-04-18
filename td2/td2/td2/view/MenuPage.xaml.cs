using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MenuItem = td2.Model.MenuItem;
using Item = td2.Model;
using td2.data;

namespace td2.view
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        List<MenuItem> List;

        public MenuPage()
        {
            InitializeComponent();
            List = new List<MenuItem>
            {
                new MenuItem { Id = Item.Menu.PlaceItem, Title = "PlaceItem" },
                new MenuItem { Id = Item.Menu.Register, Title = "S'inscrire" },
                new MenuItem { Id = Item.Menu.ChoixProfil, Title = "Profil" },
                new MenuItem { Id = Item.Menu.Login, Title = "Connexion" }
        };
            /*if (RestService.TOKEN != null) {
                List.Remove(connexion);
            }
            else {
                List.Remove(profil);

            }*/



            listView.ItemsSource = List;
            listView.SelectedItem = List[0];
            listView.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                var id = (int)((MenuItem)e.SelectedItem).Id;
                await RootPage.NavigateMenu(id);
            };
        }
    }

}