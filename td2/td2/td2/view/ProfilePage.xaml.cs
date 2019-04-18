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
	public partial class ProfilePage : ContentPage
    {
        ProfileViewModel profileViewModel;
        UserItem user;
        public ProfilePage(UserItem user)
		{
            this.user = user;
			InitializeComponent ();
            BindingContext = profileViewModel = new ProfileViewModel(user);
        }

        
        async void Modifier(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new UpdateProfilePage(user,imageView1));
        }
    }
}