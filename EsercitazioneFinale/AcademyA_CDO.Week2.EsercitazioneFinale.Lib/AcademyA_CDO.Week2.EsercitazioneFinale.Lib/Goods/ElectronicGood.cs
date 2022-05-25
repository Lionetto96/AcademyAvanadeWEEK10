using AcademyA_CDO.Week2.EsercitazioneFinale.Lib.Exceptions;
using System;

namespace AcademyA_CDO.Week2.EsercitazioneFinale.Lib.Goods

{
    public class ElectronicGood : Good
    {
        public string Manufacturer { get; set; }

        public ElectronicGood(
            string code,
            string desc,
            decimal price,
            DateTime receiving,
            int qty,
            string manufacturer) : base(code, desc, price, receiving, qty)
        {
            if (string.IsNullOrEmpty(manufacturer))
                throw new GoodException("Invalid Manufacturer.");

            Manufacturer = manufacturer;
        }

        public override string ToString()
        {
            return $"[E - {Code}] {Description} ({QtyInStock} x {Price} EUR), " +
                $"manufactured by {Manufacturer}";
        }
    }
}
