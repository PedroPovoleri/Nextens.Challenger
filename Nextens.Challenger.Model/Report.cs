using Nextens.Challenger.Model.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nextens.Challenger.Model
{
    public class Report : IEntity
    {
        public Guid Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Guid ClientId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool WhealthTaxIndicator { get; set; }
        public decimal WealthGrowth { get; set; }
        public bool RealestateIndicator { get; set; }
        public decimal RealestateGroth { get; set; }
        public bool IncomeVolatility { get; set; }
        public decimal IncomeVariation { get; set; }
        public List<string> MessagesUsed { get; set; }
    }
}
