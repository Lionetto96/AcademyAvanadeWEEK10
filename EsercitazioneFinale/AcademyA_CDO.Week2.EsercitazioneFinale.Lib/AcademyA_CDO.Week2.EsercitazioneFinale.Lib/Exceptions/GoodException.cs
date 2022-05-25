using System;

namespace AcademyA_CDO.Week2.EsercitazioneFinale.Lib.Exceptions
{
    public class GoodException : Exception
    {
        public GoodException() : base()
        {
        }

        public GoodException(string message) : base(message)
        {
        }

        public GoodException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
