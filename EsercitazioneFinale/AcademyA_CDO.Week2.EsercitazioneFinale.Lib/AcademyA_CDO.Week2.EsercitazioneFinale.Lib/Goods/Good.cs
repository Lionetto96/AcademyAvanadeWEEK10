using System;

namespace AcademyA_CDO.Week2.EsercitazioneFinale.Lib.Goods
{
    public abstract class Good
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime ReceivingDate { get; set; }
        public int QtyInStock { get; set; }

        public Good(string code, string desc, decimal price, DateTime receiving, int qty)
        {
            Code = code;
            Description = desc;
            Price = price;
            ReceivingDate = receiving;
            QtyInStock = qty;
        }

        public override string ToString()
        {
            return $"[{Code}] {Description} ({QtyInStock} x {Price})";
        }
    }
}
