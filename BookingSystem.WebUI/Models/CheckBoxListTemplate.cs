using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookingSystem.WebUI.Models
{
    public class CheckBoxListTemplate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }
    }
}