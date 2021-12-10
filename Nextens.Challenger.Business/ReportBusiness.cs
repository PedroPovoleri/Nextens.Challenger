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

            return lstRtrn.Where(x => x.RealestateIndicator == true ||
                                      x.IncomeVolatility == true ||
                                      x.WhealthTaxIndicator == true)
                          .OrderByDescending(x => x.Wealth).ToList();
        }

        public Report GetReportPerClient(Guid clientId)
        {
            var clientMessages = loadData.LoadDataset().Where(x => x.ClientId == clientId).ToList();

            return CreateReport(clientMessages);
        }


        private Report CreateReport(List<Client> clientMessages)
        {
            Client current = clientMessages.OrderByDescending(x => x.Year).FirstOrDefault();
            var repo = new Report();

            repo.ClientId = current.ClientId;
            repo.Id = Guid.NewGuid();
            repo.UsedYear = current.Year;
            repo.WhealthTaxIndicator = CheckWealthTaxIndicator(current);
            repo.RealestateIndicator = RealEstateValue(clientMessages);
            repo.IncomeVolatility = CheckIncomeVolatiliy(current, loadData.LoadDataset().FirstOrDefault(x => x.Year == current.Year - 1));
            if (repo.RealestateIndicator)
            {
                repo.RealestateGrothPercent = GetPorcentage(current.RealEstatePropertyValue.GetValueOrDefault(), clientMessages.Where(x => x.Year >= current.Year - 3).OrderBy(x => x.Year).FirstOrDefault().RealEstatePropertyValue.GetValueOrDefault());
                repo.RealestateGrothValue = current.RealEstatePropertyValue.GetValueOrDefault() - clientMessages.OrderBy(x => x.Year).FirstOrDefault().RealEstatePropertyValue.GetValueOrDefault();
            }

            if (repo.IncomeVolatility)
            {
                repo.IncomeVariation = current.Income.GetValueOrDefault() - loadData.LoadDataset().FirstOrDefault(x => x.Year == current.Year - 1).Income.GetValueOrDefault();
                repo.IncomeCurrentYear = current.Income.GetValueOrDefault();
                repo.IncomeVariationLastYear = loadData.LoadDataset().FirstOrDefault(x => x.Year == current.Year - 1).Income.GetValueOrDefault();
            }

            if (repo.WhealthTaxIndicator)
            {
                var lastyear = loadData.LoadDataset().FirstOrDefault(x => x.Year == current.Year - 1);
                repo.WealthGrowth = (current.BankbalanceInternational.GetValueOrDefault() + current.BankBalanceNational.GetValueOrDefault() + current.StockInvestments.GetValueOrDefault()) - (lastyear.BankbalanceInternational.GetValueOrDefault() + lastyear.BankBalanceNational.GetValueOrDefault() + lastyear.StockInvestments.GetValueOrDefault());
            }
            var grebMessages = loadData.LoadDataset().Where(x => x.ClientId == current.ClientId && x.Year >= current.Year - 3);
            repo.MessagesUsed.AddRange(grebMessages.Select(x => $"{x.ClientId}-{x.Id}.json").ToList());

            repo.Wealth = current.BankbalanceInternational.GetValueOrDefault() + current.BankBalanceNational.GetValueOrDefault() + current.StockInvestments.GetValueOrDefault();

            return repo;


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
                if (valueBigestYear - client.RealEstatePropertyValue.GetValueOrDefault() > 0)
                {
                    FitInTheRole = GetPorcentage(valueBigestYear, client.RealEstatePropertyValue.GetValueOrDefault()) > 15.00;
                }
            }


            return FitInTheRole;
        }

        private bool CheckWealthTaxIndicator(Client client)
        {
            return (client.BankBalanceNational + client.BankbalanceInternational + client.StockInvestments > 200000.00);
        }


    }
}
