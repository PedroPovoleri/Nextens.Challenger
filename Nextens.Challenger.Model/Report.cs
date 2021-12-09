using Nextens.Challenger.Model.Interface;
using System;
using System.Collections.Generic;

namespace Nextens.Challenger.Model
{
    public class Report : IEntity
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public int UsedYear { get; set; }
        public bool WhealthTaxIndicator { get; set; }
        public decimal WealthGrowth { get; set; }
        public bool RealestateIndicator { get; set; }
        public decimal RealestateGroth { get; set; }
        public bool IncomeVolatility { get; set; }
        public decimal IncomeVariation { get; set; }
        public List<string> MessagesUsed { get; set; }
    }
}
