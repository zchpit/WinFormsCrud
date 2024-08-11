using CommonLibrary.Dto;
using CommonLibrary.Strategy;
using WinFormsCrud.Helper;
using WinFormsCrud.Interface;


namespace WinFormsCrud.Services
{
    public class UserService : IUserService
    {
        //TODO: remove HttpClient as this object have prolem with socket release. Put IHttpClientFactory:  https://cezarywalenciuk.pl/blog/programing/ihttpclientfactory-na-problem-z-httpclient
        static HttpClient client = new HttpClient();
        ITransferStrategy _transferStrategy;

        public UserService(ITransferStrategy transferStrategy) 
        {
            _transferStrategy = transferStrategy;
        }

        public async ValueTask<SimpleUserDto> Login(string username, string password)
        {
            string encryptedUsername = _transferStrategy.Encrypt(username);
            string encryptedPassword = _transferStrategy.Encrypt(password);

            string path = string.Concat(ApiHelper.urlBase, ApiHelper.userControllerName, "/", encryptedUsername, "/", encryptedPassword);

            SimpleUserDto simpleUserDto = null;
            HttpResponseMessage response = await client.GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                simpleUserDto = await response.Content.ReadAsAsync<SimpleUserDto>();
            }

            return simpleUserDto;
        }

        public bool IsUserValid(string username)
        {
            if(string.IsNullOrEmpty(username)) 
                return false;

            return true;
        }
        public bool IsPasswordValid(string password)
        {
            if (string.IsNullOrEmpty(password))
                return false;

            if(password.Length < 5)
                return false;

            //some other validation stuff like Regexp etc.

            return true;
        }
    }
}
