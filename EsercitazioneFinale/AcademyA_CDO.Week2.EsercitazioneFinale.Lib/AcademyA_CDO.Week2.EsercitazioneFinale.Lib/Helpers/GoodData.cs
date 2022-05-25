using AcademyA_CDO.Week2.EsercitazioneFinale.Lib.Exceptions;
using AcademyA_CDO.Week2.EsercitazioneFinale.Lib.Goods;
using System;

namespace AcademyA_CDO.Week2.EsercitazioneFinale.Lib.Helpers
{
    public class GoodData
    {
        public enum GoodType { Electronic, Perishable, SpiritDrink }

        public GoodType Type { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime ReceivingDate { get; set; }
        public int QtyInStock { get; set; }
        public string Manufacturer { get; set; }
        public DateTime ExpirationDate { get; set; }
        public Conservation ConservationMode { get; set; }
        public int AlcoolPercentage { get; set; }
        public SpiritType SpiritType { get; set; }

        public static GoodData Parse(string rawData)
        {
            var data = rawData.Split(',');

            if (data.Length != 11)
                throw new WarehouseException("Cannot generate a proper Good object.");


            var result = Enum.TryParse<GoodType>(data[0], out GoodType type);
            result = decimal.TryParse(data[3], out decimal price);
            result = DateTime.TryParse(data[4], out DateTime receiving);
            result = int.TryParse(data[5], out int qtyInStock);
            result = DateTime.TryParse(data[7], out DateTime expiration);
            result = Enum.TryParse<Conservation>(data[8], out Conservation conservationMode);
            result = int.TryParse(data[9], out int alcoolPerc);
            result = Enum.TryParse<SpiritType>(data[8], out SpiritType spiritType);

            //if (!result)
            //    throw new WarehouseException("Cannot generate a proper GoodData object.");

            return new GoodData()
            {
                Type = type,
                Code = data[1],
                Description = data[2],
                Price = price,
                ReceivingDate = receiving,
                QtyInStock = qtyInStock,
                Manufacturer = data[6],
                ExpirationDate = expiration,
                ConservationMode = conservationMode,
                AlcoolPercentage = alcoolPerc,
                SpiritType = spiritType
            };
        }
    }
}
