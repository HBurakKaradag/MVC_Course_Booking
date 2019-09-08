namespace BookingSystem.Domain
{
    /// <summary>
    /// @Kodluyoruz-MVC-Bootcamp  13.07.2019 – 09.09.2019
    /// H.Burak Karadağ
    /// Oluşturulan ViewModellerin inteface sınıfı.
    /// </summary>
    public interface IModel : IDomain
    {
        int Id { get; set; }
    }
}