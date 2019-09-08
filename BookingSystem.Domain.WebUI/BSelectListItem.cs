namespace BookingSystem.Domain.WebUI
{
    /// <summary>
    /// @Kodluyoruz-MVC-Bootcamp  13.07.2019 – 09.09.2019
    /// H.Burak Karadağ
    /// UI tarafına döndüğümüz datanın, Extension olarak yazdığımız BDropdownListFor üzerinden ekrana basılabilmesi
    /// için eklenmiştir. SelectListItem yerine custom ettiğimiz  sınıfı kullanıyoruz.
    /// SelectListItem ParentValue içermediği için BSelectListItem üzerinden ihtiyacımızı giderdik.
    /// /// </summary>
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