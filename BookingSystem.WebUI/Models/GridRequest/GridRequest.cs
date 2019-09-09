namespace BookingSystem.WebUI.Models.GridRequest
{
    public class GridRequest<T>
    {
        public int draw { get; set; }
        public int length { get; set; }
        public int start { get; set; }
        public Column_Request[] columns { get; set; }
        public Order[] order { get; set; }
        public Search_Request search { get; set; }
        public T FilterRequest { get; set; }
    }
}