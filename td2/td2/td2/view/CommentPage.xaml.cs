using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using td2.data;
using td2.viewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace td2.view
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CommentPage : ContentPage
    {
        public CommentViewModel CommentsViewModel { get; set; }
        public CommentPage (CommentViewModel commentsViewModel)
		{
			InitializeComponent ();
            BindingContext = CommentsViewModel = commentsViewModel;
            /*if (RestService.TOKEN == null) {
                this.addcomment.IsEnabled=false;
            }*/
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            /*if (RestService.TOKEN == null)
            {
                this.addcomment.IsEnabled = false;
            }*/
            if (CommentsViewModel.Comments.Count == 0)
                CommentsViewModel.LoadCommentsCommand.Execute(null);
        }
        async void AjoutComment(object sender, EventArgs e)
        {
            int id=CommentsViewModel.PlaceItem.Id;

               await Navigation.PushAsync(new AddCommentPage(new AddCommentViewModel(id)));
        }
    }
}