using System;

namespace AcademyA_CDO.Week2.EsercitazioneFinale.Lib.Helpers
{
    public class FileLoadingEventArgs : EventArgs
    {
        public int NumberOfRows { get; set; }

        public DateTime Timestamp = DateTime.Now;

        public string[] Data { get; set; }

        public FileLoadingEventArgs(int rows) : base()
        {
            NumberOfRows = rows;
        }
    }
}
