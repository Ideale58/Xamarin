﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using TD.Api.Dtos;
using Xamarin.Forms;

namespace td2.viewModel
{
    public class CommentViewModel : BaseViewModel
    {
        public ObservableCollection<CommentItem> Comments { get; set; }
        public Command LoadCommentsCommand { get; set; }
        public PlaceItem PlaceItem { get; set; }

        public CommentViewModel(PlaceItem placeItem = null)
        {
            Comments = new ObservableCollection<CommentItem>();
            PlaceItem = placeItem;

            LoadCommentsCommand = new Command(async () => await LoadComments());
        }

        async Task LoadComments()
        {
            if (Refresh)
                return;

            Refresh = true;

            try
            {
                var placeItem = await restService.FindPlaceItemById(PlaceItem.Id);
                PlaceItem = placeItem;
                Comments.Clear();

                foreach (var comment in placeItem.Comments)
                {
                    Comments.Add(comment);
                }
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
