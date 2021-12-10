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
        public double WealthGrowth { get; set; }
        public bool RealestateIndicator { get; set; }
        public double RealestateGrothPercent { get; set; }
        public double RealestateGrothValue { get; set; }
        public bool IncomeVolatility { get; set; }
        public double IncomeVariation { get; set; }
        public double IncomeVariationLastYear { get; set; }
        public double IncomeCurrentYear { get; set; }

        public double Wealth { get; set; }
        public List<string> MessagesUsed { get; set; } = new List<string>();
    }
}
