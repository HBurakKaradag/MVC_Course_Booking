namespace BookingSystem.Domain.Entity
{
    public class HotelType
    {
        public HotelType()
        {
            this.IsActive = true;
            this.IsDeleted = false;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}