using System;

namespace BookingSystem.Core.CustomException
{
    /// <summary>
    /// @Kodluyoruz-MVC-Bootcamp  13.07.2019 – 09.09.2019
    /// H.Burak Karadağ
    /// Database katmanından gelen hataların handle edilebilmesi için oluşturulan Exception tipi
    /// </summary>
    public class DatabaseException : Exception
    {
        public DatabaseException()
        {
        }

        public DatabaseException(string message) : base(message)
        {
        }

        public DatabaseException(string message, Exception innerException)
                : base(message, innerException)
        {
        }
    }
}