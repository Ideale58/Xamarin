using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using td2.viewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;

namespace td2.view
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddCommentPage : ContentPage
    {

        public AddCommentViewModel AddCommentViewModel { get; set; }
        public AddCommentPage(AddCommentViewModel addCommentViewModel)
		{
			InitializeComponent ();
            BindingContext = AddCommentViewModel = addCommentViewModel;
        }

        async void Save(object sender, EventArgs e)
        {
            await AddCommentViewModel.SaveComment();
            await Navigation.PopAsync();
        }
    }
}