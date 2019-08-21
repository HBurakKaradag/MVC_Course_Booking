using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookingSystem.WebUI.Models.DataTableRequest
{
    public class Search_Request
    {
        public bool regex { get; set; }
        public string value { get; set; }
    }
}