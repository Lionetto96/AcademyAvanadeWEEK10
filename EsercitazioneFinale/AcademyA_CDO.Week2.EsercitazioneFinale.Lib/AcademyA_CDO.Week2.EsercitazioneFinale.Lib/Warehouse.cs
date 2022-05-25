using AcademyA_CDO.Week2.EsercitazioneFinale.Lib.Exceptions;
using AcademyA_CDO.Week2.EsercitazioneFinale.Lib.Goods;
using AcademyA_CDO.Week2.EsercitazioneFinale.Lib.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AcademyA_CDO.Week2.EsercitazioneFinale.Lib
{
    public class Warehouse
    {
        #region events

        public event EventHandler FileReaderStarted;
        public event EventHandler<FileLoadingEventArgs> FileReaderProgress;
        public event EventHandler<FileLoadingEventArgs> FileReaderCompleted;

        #endregion

        #region ctor

        public Warehouse(string address, int loadingSteps = 10)
        {
            WarehouseId = Guid.NewGuid();

            if (string.IsNullOrEmpty(address))
                throw new WarehouseException("Invalid Address.");
            Address = address;

            if (loadingSteps <= 0)
                throw new WarehouseException("Invalid File Loading Step.");
            FileLoadingSteps = loadingSteps;

            TotalGoodsCost = 0;
            LastOperation = DateTime.Now;
        }

        #endregion

        #region properties

        public Guid WarehouseId { get; set; }
        public string Address { get; set; }
        public decimal TotalGoodsCost { get; set; }
        public DateTime LastOperation { get; set; }
        private List<Good> _goods = new List<Good>();


        public int FileLoadingSteps { get; set; }

        #endregion


        #region Operator Overloading

        public static Warehouse operator +(Warehouse warehouse, Good good)
        {
            if (good.Price <= 0 || good.QtyInStock <= 0)
                throw new WarehouseException("Good price and/or Qty must be greater than 0.");

            // add good
            warehouse._goods.Add(good);

            // adjust warehouse properties
            warehouse.TotalGoodsCost += (good.Price * good.QtyInStock);
            warehouse.LastOperation = good.ReceivingDate;

            return warehouse;
        }

        public static Warehouse operator -(Warehouse warehouse, Good good)
        {
            if (good.Price <= 0 || good.QtyInStock <= 0)
                throw new WarehouseException("Good price and/or Qty must be greater than 0.");

            // remove good
            var goodToBeRemoved = warehouse._goods.FirstOrDefault(g => g.Code == good.Code);

            if (goodToBeRemoved != null)
            {
                warehouse._goods.Remove(goodToBeRemoved);

                // adjust warehouse properties
                warehouse.TotalGoodsCost -= (goodToBeRemoved.Price * goodToBeRemoved.QtyInStock);
                warehouse.LastOperation = DateTime.Now;
            }

            return warehouse;
        }

        #endregion

        public string StockList()
        {
            // Represents a mutable string of characters.String as we know it, is not mutable.
            // So for my conveniance I choos e to use this object. It's no mandatory for the scope of the test
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"====== Warehouse {WarehouseId} ======");
            sb.AppendLine($"Total Goods Cost: {TotalGoodsCost}\t\tLast Operation: {LastOperation:yyyy-MM-dd HH:mm}");
            sb.AppendLine("=======================================================");
            sb.AppendLine("== Goods ==========================================");
            foreach (Good good in _goods.OrderBy(m => m.Code))
                sb.AppendLine(good.ToString());

            sb.AppendLine("=======================================================");
            return sb.ToString();
        }

        public Good GetGoodByCode(string goodCode)
        {
            if (goodCode != null)
                return this._goods.FirstOrDefault(g => g.Code == goodCode);

            return null;
        }

        public bool LoadGoodsFromFile(string path)
        {
            int nbrOfReadLines = 0;

            try
            {
                using (StreamReader reader = File.OpenText(path))
                {

                    string line;

                    // scarta header
                    line = reader.ReadLine();

                    if (FileReaderStarted != null)
                        FileReaderStarted(this, EventArgs.Empty);

                    // process linee

                    while ((line = reader.ReadLine()) != null)
                    {
                        nbrOfReadLines++;

                        // process row
                        var data = GoodData.Parse(line);
                        var good = GoodFactory.CreateGood(data);
                        this._goods.Add(good);
                        this.TotalGoodsCost += (good.Price * good.QtyInStock);

                        if (nbrOfReadLines % FileLoadingSteps == 0)
                            if (FileReaderProgress != null)
                                FileReaderProgress(this, new FileLoadingEventArgs(nbrOfReadLines));
                    }

                    if (FileReaderCompleted != null)
                        FileReaderCompleted(this, new FileLoadingEventArgs(nbrOfReadLines));
                }
                return true;

            }
            catch (IOException ex)
            {
                throw new WarehouseException(
                    $"Error Processing file {path}.",
                    ex);
            }
            catch (Exception ex)
            {
                throw new WarehouseException(
                    $"Generic Error Processing file {path}.",
                    ex);
            }
        }
    }
}
