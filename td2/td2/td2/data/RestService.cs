using Common.Api.Dtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TD.Api.Dtos;
using Xamarin.Forms;

namespace td2.data
{
    class RestService : IRestService
    {
        public static String TOKEN=null;
        public static UserItem USER = null;
        private String url = "https://td-api.julienmialon.com/";
        HttpClient client;
        public List<PlaceItemSummary> Items { get; private set; }
        public RestService()
        {
            client = new HttpClient();
         }

        public async Task<List<PlaceItemSummary>> RefreshDataAsync()
        {
            List<PlaceItemSummary> list = new List<PlaceItemSummary>();
            var uri = new Uri(url+ "places");

            try
            {
                var response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    list = JsonConvert.DeserializeObject<Response<List<PlaceItemSummary>>>(content).Data;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return await Task.FromResult(list);
        }

        public async Task<PlaceItem> FindPlaceItemById(int Id)
        {
            PlaceItem item = new PlaceItem();
            var uri = new Uri(url + "places/" + Id);

            try
            {
                var response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    item = JsonConvert.DeserializeObject<Response<PlaceItem>>(content).Data;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return await Task.FromResult(item);
        }

        public async Task<LoginResult> SaveUser(RegisterRequest user)

        {
            LoginResult res= new LoginResult();

             var uri = new Uri(url + "auth/register");



            try
            {
                var json = JsonConvert.SerializeObject(user);

                var u = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(uri,u);
                var content = await response.Content.ReadAsStringAsync();
                res = JsonConvert.DeserializeObject<Response<LoginResult>>(content).Data;




                if (response.IsSuccessStatusCode)
                {

                    Debug.WriteLine(@"User enregistré");

                }



            }
            catch (Exception ex)
            {

                Debug.WriteLine(@"				ERROR {0}", ex.Message);

            }
            return await Task.FromResult(res);

        }


        public async Task<LoginResult> ConnexionUser(LoginRequest user) {

            LoginResult res = new LoginResult();

            var uri = new Uri(url + "auth/login");



            try
            {
                var json = JsonConvert.SerializeObject(user);

                var u = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(uri, u);
                var content = await response.Content.ReadAsStringAsync();
                res = JsonConvert.DeserializeObject<Response<LoginResult>>(content).Data;



                if (response.IsSuccessStatusCode)
                {
                    TOKEN = res.AccessToken;
                    //USER =await GetUser();

                    Debug.WriteLine(@"User connecté");

                }



            }
            catch (Exception ex)
            {

                Debug.WriteLine(@"				ERROR {0}", ex.Message);

            }
            return await Task.FromResult(res);
        }

        public async Task SaveComment(CreateCommentRequest com, int id)

        {
            CreateCommentRequest res = new CreateCommentRequest();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url + "places/" + id + "/comments");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);

            //var uri = new Uri(url + "places/"+id+"/comments");



            try
            {
                var json = JsonConvert.SerializeObject(com);

                var u = new StringContent(json, Encoding.UTF8, "application/json");
                request.Content = u;
                HttpResponseMessage response = await client.SendAsync(request);

                var result = await response.Content.ReadAsStringAsync();
                //var response = await client.PostAsync(uri, u);




                if (response.IsSuccessStatusCode)
                {

                    Debug.WriteLine(@"Commentaire enregistré");

                }



            }
            catch (Exception ex)
            {

                Debug.WriteLine(@"				ERROR {0}", ex.Message);

            }

        }

        public async Task<UserItem> GetUser()
        {
            CreateCommentRequest res = new CreateCommentRequest();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url + "me");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);

            UserItem user = new UserItem();
            var uri = new Uri(url + "me");

            try
            {
                HttpResponseMessage response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    user = JsonConvert.DeserializeObject<Response<UserItem>>(content).Data;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return await Task.FromResult(user);
        }



        public async Task<ImageItem> PostImage(byte[] imageData)
        {
           
               HttpClient client = new HttpClient();
            //byte[] imageData = await client.GetByteArrayAsync("https://bnetcmsus-a.akamaihd.net/cms/blog_header/x6/X6KQ96B3LHMY1551140875276.jpg");
            //byte[] imageData = await client.GetByteArrayAsync(url);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, this.url+"images");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer",TOKEN);

            MultipartFormDataContent requestContent = new MultipartFormDataContent();

            var imageContent = new ByteArrayContent(imageData);
            imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");

            // Le deuxième paramètre doit absolument être "file" ici sinon ça ne fonctionnera pas
            requestContent.Add(imageContent, "file", "file.jpg");

            request.Content = requestContent;

            HttpResponseMessage response = await client.SendAsync(request);

            var result = await response.Content.ReadAsStringAsync();

             ImageItem res = JsonConvert.DeserializeObject<Response<ImageItem>>(result).Data;

            Console.WriteLine(res.Id);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Image uploaded!");
            }
            else
            {
                Debugger.Break();
            }

            return await Task.FromResult(res);
        }
 

        public async Task SavePlaceItem(CreatePlaceRequest place)

        {

            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url + "places");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);

    

            try
            {
                var json = JsonConvert.SerializeObject(place);

                var u = new StringContent(json, Encoding.UTF8, "application/json");
                request.Content = u;
                HttpResponseMessage response = await client.SendAsync(request);

                var result = await response.Content.ReadAsStringAsync();
                //var response = await client.PostAsync(uri, u);


            var res = JsonConvert.DeserializeObject<Response>(result);


                if (response.IsSuccessStatusCode)
                {

                    Debug.WriteLine(@"Commentaire enregistré");

                }



            }
            catch (Exception ex)
            {

                Debug.WriteLine(@"				ERROR {0}", ex.Message);

            }

        }

        public async Task<Response> SaveMdp(UpdatePasswordRequest mdp)

        {

            /*CreateCommentRequest res = new CreateCommentRequest();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url + "places/" + id + "/comments");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);

            */

            Response res =new Response();
            HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("PATCH"), url + "me/password");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);
            

            try
            {
                var json = JsonConvert.SerializeObject(mdp);

                var u = new StringContent(json, Encoding.UTF8, "application/json");
                request.Content = u;
                HttpResponseMessage response = await client.SendAsync(request);
                var content = await response.Content.ReadAsStringAsync();
                res = JsonConvert.DeserializeObject<Response>(content);




                if (response.IsSuccessStatusCode)
                {

                    Debug.WriteLine(@"Mot de passe changé");

                }



            }
            catch (Exception ex)
            {

                Debug.WriteLine(@"				ERROR {0}", ex.Message);

            }
            return await Task.FromResult(res);

        }

        /*

                public async Task DeletePlaceItem(string id)

                {

                    // RestUrl = http://developer.xamarin.com:8081/api/todoitems/{0}

                    var uri = new Uri(string.Format(Constants.RestUrl, id));



                    try
                    {

                        var response = await client.DeleteAsync(uri);



                        if (response.IsSuccessStatusCode)
                        {

                            Debug.WriteLine(@"TodoItem successfully deleted.");

                        }



                    }
                    catch (Exception ex)
                    {

                        Debug.WriteLine(@"ERROR {0}", ex.Message);

                    }

                }

                public Task SavePlaceItem(PlaceItem item, bool isNewItem)
                {
                    throw new NotImplementedException();
                }*/
    }
}
