using System;

namespace AcademyA_CDO.Week2.EsercitazioneFinale.Lib.Goods
{
    public enum Conservation { FREEZER, FRIDGE, SHELF }

    public class PerishableGood : Good
    {
        public DateTime ExpirationDate { get; set; }
        public Conservation ConservationMode { get; set; }

        public PerishableGood(
            string code,
            string desc,
            decimal price,
            DateTime receiving,
            int qty,
            DateTime expire,
            Conservation mode) : base(code, desc, price, receiving, qty)
        {
            ExpirationDate = expire;

            ConservationMode = mode;
        }

        public override string ToString()
        {
            return $"[P - {Code}] {Description} ({QtyInStock} x {Price} EUR), " +
                $"expires on {ExpirationDate.ToString("dd-MMM-yyyy")}, keep in/on {ConservationMode}";
        }
    }
}
