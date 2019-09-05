using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookingSystem.Domain.WebUI
{
    public class CheckBoxListTemplate
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsSelected { get; set; }
    }
}