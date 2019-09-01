using System;

namespace BookingSystem.WebUI.Models.Response
{
    public class Errors
    {
        public string Key { get; set; }
        public string[] ErrorMessages { get; set; }
        public Exception[] Exceptions { get; set; }
    }
}