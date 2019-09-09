namespace BookingSystem.WebUI.Models.DataTableRequest
{
    public class Column_Request
    {
        public string data { get; set; }
        public string name { get; set; }
        public bool orderable { get; set; }
        public bool searchable { get; set; }
        public Search_Request search { get; set; }
    }
}