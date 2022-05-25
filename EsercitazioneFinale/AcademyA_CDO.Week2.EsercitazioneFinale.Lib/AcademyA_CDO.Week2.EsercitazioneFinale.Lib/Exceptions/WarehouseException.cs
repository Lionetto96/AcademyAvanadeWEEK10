using System;

namespace AcademyA_CDO.Week2.EsercitazioneFinale.Lib.Exceptions
{
    public class WarehouseException : Exception
    {
        public WarehouseException() : base()
        {
        }

        public WarehouseException(string message) : base(message)
        {
        }

        public WarehouseException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
