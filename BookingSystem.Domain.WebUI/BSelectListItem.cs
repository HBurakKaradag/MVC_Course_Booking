using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookingSystem.Domain.WebUI
{
    public class BSelectListItem
    {
        public BSelectListItem()
        {
            this.Selected = false;
            this.Disabled = false;
            this.ParentValue = "0";
        }

        public string Value { get; set; }
        public string ParentValue { get; set; }
        public string Text { get; set; }
        public bool Selected { get; set; }
        public bool Disabled { get; set; }
    }
}