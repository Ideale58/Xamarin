﻿using Common.Api.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TD.Api.Dtos;

namespace td2.data
{
    public interface IRestService
    {
        Task<List<PlaceItemSummary>> RefreshDataAsync();
        Task<PlaceItem> FindPlaceItemById(int Id);
        Task<LoginResult> SaveUser(RegisterRequest user);
        Task<LoginResult> ConnexionUser(LoginRequest user);
        Task<Response> SaveComment(CreateCommentRequest com, int id);
        Task<UserItem> GetUser();
        Task<ImageItem> PostImage(byte[] imageData);
        Task<Response> SavePlaceItem(CreatePlaceRequest place);
        Task<Response> SaveMdp(UpdatePasswordRequest mdp);
        Task<UserItem> UpdateProfile(UpdateProfileRequest user);
        //Task SavePlaceItem(PlaceItem item, bool isNewItem);



        //Task DeletePlaceItem(string id);

    }
}
