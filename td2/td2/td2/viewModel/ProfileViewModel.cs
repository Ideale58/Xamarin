using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using TD.Api.Dtos;
using Xamarin.Forms;

namespace td2.viewModel
{
    public class ProfileViewModel:BaseViewModel
    {
        public UserItem User { get; set; }
        public Command LoadCommand { get; set; }

        public ProfileViewModel(UserItem user)
        {
            
            User = user;
            
        }

       
    }
}
