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
        MenuItem place;
        MenuItem inscription;
        MenuItem profil;
        MenuItem connexion;
        public MenuPage()
        {
            place = new MenuItem { Id = Item.Menu.PlaceItem, Title = "PlaceItem" };
            inscription = new MenuItem { Id = Item.Menu.Register, Title = "S'inscrire" };
            profil = new MenuItem { Id = Item.Menu.ChoixProfil, Title = "Profil" };
            connexion = new MenuItem { Id = Item.Menu.Login, Title = "Connexion" };
            InitializeComponent();
            List = new List<MenuItem> { place,profil,inscription,connexion};

            if (RestService.TOKEN == null)
            {
                List.Remove(profil);
            }
            else
            {
                List.Remove(connexion);
            }
           


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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
        }
    }

}