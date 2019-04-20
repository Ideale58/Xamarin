using Common.Api.Dtos;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using TD.Api.Dtos;

namespace td2.viewModel
{
    class PasswordViewModel:BaseViewModel
    {
        public UpdatePasswordRequest Mdp { get; set; }
        public PasswordViewModel()
        {
            Mdp = new UpdatePasswordRequest();
        }

        public async Task<Response> SaveMdp()
        {
            Response res = null;
            if (!Refresh)
            {

                Refresh = true;

            try
            {
                res=await restService.SaveMdp(Mdp);

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
