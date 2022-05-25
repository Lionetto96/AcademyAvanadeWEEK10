using AcademyA_CDO.Week2.EsercitazioneFinale.Lib.Exceptions;
using System;

namespace AcademyA_CDO.Week2.EsercitazioneFinale.Lib.Goods
{
    public enum SpiritType { WHISKY, WODKA, GRAPPA, GIN, OTHER }

    public class SpiritDrinkGood : Good
    {
        public int AlcoolPercentage { get; set; }
        public SpiritType Type { get; set; }

        public SpiritDrinkGood(
            string code,
            string desc,
            decimal price,
            DateTime receiving,
            int qty,
            int alcoolPerc,
            SpiritType type) : base(code, desc, price, receiving, qty)
        {
            if (alcoolPerc < 0)
                throw new GoodException("Invalid Alcool Percentage.");
            AlcoolPercentage = alcoolPerc;

            Type = type;
        }

        public override string ToString()
        {
            return $"[S - {Code}] {Type} - {Description} " +
                $"({QtyInStock} x {Price} EUR), {AlcoolPercentage}% alcool";
        }
    }
}
