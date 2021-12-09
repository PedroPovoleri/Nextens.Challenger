using Nextens.Challenger.Business.Interface;
using Nextens.Challenger.Context.Interface;
using Nextens.Challenger.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nextens.Challenger.Business
{
    public class ReportBusiness : IReportBusiness
    {
        private readonly ILoadData loadData;
        public ReportBusiness(ILoadData loadData)
        {
            this.loadData = loadData;
        }

        public List<Report> GetReports()
        {
            throw new NotImplementedException();
        }



        private bool CheckIncomeVolatiliy(Client year1, Client year2)
        {
            return (GetPorcentage(year1.Income.GetValueOrDefault(), year2.Income.GetValueOrDefault()) > 50.00);

        }

        private double GetPorcentage(double vaule1, double value2)
        {
            return ((vaule1 / value2) * 100);
        }

        private bool RealEstateValue(List<Client> historicalClient)
        {

            if (historicalClient.Count == 1)
                return false;

            var FitInTheRole = false;
            double valueBigestYear = historicalClient.OrderByDescending(x => x.Year).First().RealEstatePropertyValue.GetValueOrDefault();
            int bigYear = historicalClient.OrderByDescending(x => x.Year).First().Year;

            foreach (Client client in historicalClient.OrderByDescending(x => x.Year).Take(3))
            {
                FitInTheRole = GetPorcentage(valueBigestYear, client.RealEstatePropertyValue.GetValueOrDefault()) > 15.00;
            }


            return FitInTheRole;
        }

        private bool CheckWealthTaxIndicator(Client client)
        {
            return (client.BankBalanceNational + client.BankbalanceInternational + client.StockInvestments > 200000.00);
        }

    }
}
