using Common.Api.Dtos;
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

        public async Task<Response> SaveComment()
        {
            Response res = null;
            if (!Refresh)
            {


                Refresh = true;

            try
            {
                res= await restService.SaveComment(Comment, idplace);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                Refresh = false;
                }
                return res;
            }
            return null;
        }

    }
}
