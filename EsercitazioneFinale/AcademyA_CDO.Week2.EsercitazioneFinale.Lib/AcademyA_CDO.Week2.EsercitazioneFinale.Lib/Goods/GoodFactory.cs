using AcademyA_CDO.Week2.EsercitazioneFinale.Lib.Helpers;
using System;

namespace AcademyA_CDO.Week2.EsercitazioneFinale.Lib.Goods
{
    public static class GoodFactory
    {
        public static Good CreateGood(GoodData gd)
        {
            switch (gd.Type)
            {
                case GoodData.GoodType.Electronic:
                    return new ElectronicGood(
                        gd.Code, gd.Description, gd.Price, gd.ReceivingDate, gd.QtyInStock,
                        gd.Manufacturer
                    );

                case GoodData.GoodType.Perishable:
                    return new PerishableGood(
                        gd.Code, gd.Description, gd.Price, gd.ReceivingDate, gd.QtyInStock,
                        gd.ExpirationDate, gd.ConservationMode
                    );

                case GoodData.GoodType.SpiritDrink:
                    return new SpiritDrinkGood(
                        gd.Code, gd.Description, gd.Price, gd.ReceivingDate, gd.QtyInStock,
                        gd.AlcoolPercentage, gd.SpiritType
                    );

                default:
                    throw new ArgumentException("Invalid Good type.");
            }
        }
    }
}
