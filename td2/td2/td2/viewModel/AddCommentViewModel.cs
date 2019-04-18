using System;
using System.Diagnostics;
using System.Threading.Tasks;
using TD.Api.Dtos;

namespace td2.viewModel
{
    public class AddCommentViewModel : BaseViewModel
    {
        public CreateCommentRequest Comment { get; set; }
        public int idplace;

        public AddCommentViewModel(int id)
        {
            idplace = id;
            Comment = new CreateCommentRequest();
        }

        public async Task SaveComment()
        {
            if (Refresh)
                return;

            Refresh = true;

            try
            {
                await restService.SaveComment(Comment, idplace);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                Refresh = false;
            }
        }

    }
}
