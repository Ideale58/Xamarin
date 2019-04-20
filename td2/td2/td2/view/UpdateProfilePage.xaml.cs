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
	public partial class UpdateProfilePage : ContentPage
    {
        ProfileViewModel profileViewModel;
        UserItem user;
        Image nwImage;
        public UpdateProfilePage(UserItem user,Image image)
		{
            this.user = user;
            nwImage = image;
			InitializeComponent ();
            BindingContext = profileViewModel = new ProfileViewModel(user);
        }

        
        async void Upload(object sender, EventArgs e)
        {

            await profileViewModel.GetImageStreamAsync(imageView);
            nwImage.Source = imageView.Source;
        }
        async void Save(object sender, EventArgs e)
        {
            UserItem res=await profileViewModel.Update();
            if (res != null)
            {
                await Navigation.PopAsync();
                await DisplayAlert("Profil", "Profil mis à jour", "OK");
            }
            else
            {
                await DisplayAlert("Profil", "Profil non mis à jour, réessayer", "OK");
            }
        }
    }
}