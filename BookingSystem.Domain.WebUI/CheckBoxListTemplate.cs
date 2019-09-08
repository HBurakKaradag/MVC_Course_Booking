namespace BookingSystem.Domain.WebUI
{
    /// <summary>
    /// @Kodluyoruz-MVC-Bootcamp  13.07.2019 – 09.09.2019
    /// H.Burak Karadağ
    /// EditorTemplate üzerinden ekrana CheckBox basmak için kullandığımız sınıf
    /// EditorTemplate adı ile class adınnı aynı olması gerekir.
    /// View üzerinde @Html.EditorFor ile çağrılmalıdır.
    /// </summary>
    public class CheckBoxListTemplate
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsSelected { get; set; }
    }
}