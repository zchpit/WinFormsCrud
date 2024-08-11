﻿using System.IO;
using System.Net.Http.Headers;
using WinFormsCrud.Dto;
using WinFormsCrud.Interface;

using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace WinFormsCrud.Services
{
    public class UserService : IUserService
    {
        static HttpClient client = new HttpClient();
        private const string urlBase = "https://localhost:7033/";
        private const string userService = "User";

        public UserService() 
        { 
        }

        public async ValueTask<SimpleUserDto> Login(string username, string password)
        {
            string path = string.Concat(urlBase, userService, "/", username,"/", password);

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
