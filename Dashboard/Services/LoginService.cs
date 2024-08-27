﻿namespace Dashboard.Services
{
    // A service for saving users login details when they login
    public class LoginService
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public bool IsLoggedIn { get; set; }
        public bool IsAdmin { get; set; }
        public string ImgURL { get; set; }



    }


}