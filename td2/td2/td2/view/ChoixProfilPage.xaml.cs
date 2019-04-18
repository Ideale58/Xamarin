using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Api.Dtos;
using td2.viewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace td2.view
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChoixProfilPage : ContentPage
    {
        ChoixViewModel choixViewModel;
        UserItem user;
        public ChoixProfilPage()
        {
            InitializeComponent();
            BindingContext = choixViewModel = new ChoixViewModel();
        }

        async void Afficher(object sender, EventArgs e)
        {
            this.user = await choixViewModel.restService.GetUser();
            await Navigation.PushAsync(new ProfilePage(user));
        }
        async void Modifier(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PasswordPage());
        }
    }
}