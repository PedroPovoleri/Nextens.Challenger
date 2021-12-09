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
            var lstRtrn = new List<Report>();
            var groupMessages = loadData.LoadDataset().GroupBy(x => x.ClientId);
            foreach (var client in groupMessages)
            {
                var report = GetReportPerClient(client.Key);
                lstRtrn.Add(report);
            }

            return lstRtrn;
        }

        public Report GetReportPerClient(Guid clientId)
        {
            var clientMessages = loadData.LoadDataset().Where(x => x.ClientId == clientId).ToList();

            return CreateReport(clientMessages);
        }


        private Report CreateReport(List<Client> clientMessages)
        {
            Client current = clientMessages.OrderByDescending(x => x.Year).FirstOrDefault();
            return new Report
            {
                ClientId = current.ClientId,
                Id = Guid.NewGuid(),
                UsedYear = current.Year,
                RealestateIndicator = RealEstateValue(clientMessages),
                WhealthTaxIndicator = CheckWealthTaxIndicator(current),
                IncomeVolatility = CheckIncomeVolatiliy(current, loadData.LoadDataset().FirstOrDefault(x => x.Year == current.Year -1 ))
            };
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
