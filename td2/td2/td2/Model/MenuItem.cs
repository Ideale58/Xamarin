using System;
using System.Collections.Generic;
using System.Text;

namespace td2.Model
{
        public enum Menu
        {
            PlaceItem,Register,Login,ChoixProfil
        }

        public class MenuItem
        {
            public Menu Id { get; set; }
            public string Title { get; set; }
        }


    }
