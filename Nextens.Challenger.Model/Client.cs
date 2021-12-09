using Nextens.Challenger.Model.Interface;
using System;

namespace Nextens.Challenger.Model
{
    public class Client : IEntity
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public int Year { get; set; }
        public double? Income { get; set; }
        public double? RealEstatePropertyValue { get; set; }
        public double? BankBalanceNational { get; set; }
        public double? BankbalanceInternational { get; set; }
        public double? StockInvestments { get; set; }
    }
}
