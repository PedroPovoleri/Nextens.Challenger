using Nextens.Challenger.Business;
using Nextens.Challenger.Context;
using Nextens.Challenger.Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Nextens.Challenger.Test
{
    public class ReportTest
    {

        public LoadData loadData { get; set; }
        public ReportBusiness reportBusiness { get; set; }
        [SetUp]
        public void Setup()
        {
            loadData = new LoadData();
            reportBusiness = new ReportBusiness(loadData);

            List<Client> RealestateClient = new List<Client>();
            List<Client> WealthClient = new List<Client>();

            var realEstateClient = new Guid();
            var wealthClient = new Guid();




        }

        [Test]
        public void CheckIncomeReport()
        {

            List<Client> IncomeClient = new List<Client>();
            var guidIncomeClient = new Guid();

            var clientYear2020 = new Client { ClientId = guidIncomeClient, Income = 5000, BankbalanceInternational = 100000, BankBalanceNational = 100000, StockInvestments = 100000 };
            var clientYear2019 = new Client { ClientId = guidIncomeClient, Income = 1000, BankbalanceInternational = 100, BankBalanceNational = 100, StockInvestments = 10 };
            var clientYear2018 = new Client { ClientId = guidIncomeClient, Income = 500, BankbalanceInternational = 10001, BankBalanceNational = 1000, StockInvestments = 100 };

        }



    }
}
