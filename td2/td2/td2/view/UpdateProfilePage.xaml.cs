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

            await profileViewModel.Update();
            await Navigation.PopAsync();
        }
    }
}