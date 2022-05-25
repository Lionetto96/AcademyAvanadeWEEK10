using AcademyA_CDO.Week2.EsercitazioneFinale.Lib;
using AcademyA_CDO.Week2.EsercitazioneFinale.Lib.Goods;
using AcademyA_CDO.Week2.EsercitazioneFinale.Lib.Helpers;
using System;
using System.Collections.Generic;
using static AcademyA_CDO.Week2.EsercitazioneFinale.Lib.Helpers.GoodData;

namespace AcademyA_CDO.Week2.EsercitazioneFinale.Client
{
    public static class ConsoleHelpers
    {
        #region Build Lists

        public static string BuildMenu(string title, List<string> menuItems)
        {
            Console.Clear();
            Console.WriteLine($"============= {title} =============");
            Console.WriteLine();

            foreach (var item in menuItems)
            {
                Console.WriteLine(item);
                Console.WriteLine();
            }

            // get command
            Console.Write("> ");
            string command = Console.ReadLine();
            Console.WriteLine();

            return command;
        }

        #endregion

        #region GetData

        public static string GetData(string message)
        {
            Console.Write(message + ": ");
            var value = Console.ReadLine();
            return value;
        }

        public static string GetData(string message, string initialValue)
        {
            Console.Write(message + $" ({initialValue}): ");
            var value = Console.ReadLine();
            return string.IsNullOrEmpty(value) ? initialValue : value;
        }

        #endregion

        #region Add Good Form

        public static bool ReceiveNewGood(Warehouse wh)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("--- RECEIVE NEW GOOD ---");

                GoodData goodData = new GoodData();

                goodData.Type = GetEnum<GoodType>("Type");

                goodData.Code = GetData("Code");

                goodData.Description = GetData("Description");

                string price = GetData("Price");
                decimal.TryParse(price, out decimal priceVal);
                goodData.Price = priceVal;

                string receiving = GetData("Receiving Date");
                DateTime.TryParse(receiving, out DateTime receivingVal);
                goodData.ReceivingDate = receivingVal;

                string qty = GetData("Qty in Stock");
                int.TryParse(qty, out int qtyVal);
                goodData.QtyInStock = qtyVal;

                switch (goodData.Type)
                {
                    case GoodType.Electronic:
                        goodData.Manufacturer = GetData("Manufacturer");

                        break;
                    case GoodType.Perishable:
                        string expiry = GetData("Expiration Date");
                        DateTime.TryParse(expiry, out DateTime expiryVal);
                        goodData.ExpirationDate = expiryVal;

                        goodData.ConservationMode =
                            GetEnum<Conservation>("Conservation");

                        break;
                    case GoodType.SpiritDrink:
                        string alcool = GetData("Alcool %");
                        int.TryParse(alcool, out int alcoolVal);
                        goodData.AlcoolPercentage = alcoolVal;

                        goodData.SpiritType =
                            GetEnum<SpiritType>("Spirit Type");

                        break;
                    default:
                        throw new ArgumentException("Invalid Good type.");
                }
                // other data ...

                var good = GoodFactory.CreateGood(goodData);

                wh += good;

                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion

        #region Remove Good Form

        public static bool ShipGood(Warehouse wh)
        {
            try
            {
                // show list
                Console.Clear();
                Console.WriteLine(wh.StockList());

                // select good
                string goodCode = GetData("Good Code");
                Good good = wh.GetGoodByCode(goodCode);

                wh -= good;

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion

        #region Get goods from file

        public static bool ReadDataFromFile(Warehouse wh)
        {
            var path = GetData("Path");

            if (string.IsNullOrEmpty(path))
                return false;

            return wh.LoadGoodsFromFile(path);
        }

        #endregion

        #region GetEnum

        public static T GetEnum<T>(string fieldName, T state = default(T)) where T : struct
        {
            string enumLegenda = $"{fieldName} [";
            foreach (var suit in Enum.GetValues(typeof(T)))
            {
                enumLegenda += suit.ToString() + "/";
            }
            enumLegenda += "] > ";

            Console.Write(enumLegenda);
            var value = Console.ReadLine();

            Enum.TryParse<T>(value, true, out T result);

            return result;
        }

        #endregion
    }
}
