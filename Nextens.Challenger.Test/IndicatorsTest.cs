using Nextens.Challenger.Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nextens.Challenger.Test
{
    public class IndicatorsTest
    {
        [SetUp]
        public void Setup()
        {


        }

        /// <summary>
        /// Assignment
        //  You have been provided with a set of financial data.Using the data provided you must build a report for each
        //  unique client.The report will display:
        //• A Wealth Tax Indicator: If the Total Capital is larger than (200 000). Calculation: BankBalanceNational
        //+ BankbalanceInternational + StockInvestments.
        //• Real estate / property value growth indicator.If the RealEstatePropertyValue has increased by at
        //least 15% compared to the previous year for the last 3 years.Display total percentage gain over the 3
        //years and the total value
        //• Income volatility indicator: If the Income from the previous year is significantly, 50% difference,
        //higher or lower compared to the previous year, record the following.List the details of the difference
        //by displaying the year, previous year, Income and % change
        //• The report should be ordered by wealthiest customer first.Sum of all metrics
        //• Include tests
        //• Only display customers that have at least one valid key indicator to report on
        //Requirements
        //The main application logic must be written in C# .NET Core. The data provided cannot be altered/changed in
        //any way.We expect you to use industry best practices (for example, such as separation of concerns, clean
        //code). In the future we might expand the application with more indicators, it would be good if you include this
        //in your design.An application will need to present the reports so we can see it working.For example, a console
        //application, Web App, WPF, all the above, etc.
        /// </summary>

        [Test]
        public void WealthTaxIndicator()
        {

            //A Wealth Tax Indicator: If the Total Capital is larger than (200 000). Calculation: BankBalanceNational
            //+BankbalanceInternational + StockInvestments.

            Client client200k = new Client { ClientId = Guid.NewGuid(), BankbalanceInternational = 100000.01, BankBalanceNational = 100000.01, StockInvestments = 100000.01 };
            Assert.IsTrue(CheckWealthTaxIndicator(client200k), "Check with 200000");

            client200k.StockInvestments = 0.00;
            client200k.BankbalanceInternational = 0.00;
            Assert.IsFalse(CheckWealthTaxIndicator(client200k), "Check without 200000");

            client200k.BankBalanceNational = -2000001.01;
            Assert.IsFalse(CheckWealthTaxIndicator(client200k), "Check with negative values");
        }

        [Test]
        public void PropertyValueGrowthIndicator()
        {

            //Real estate / property value growth indicator.If the RealEstatePropertyValue has increased by at
            //least 15 % compared to the previous year for the last 3 years.Display total percentage gain over the 3
            //years and the total value
            var ClientId = Guid.NewGuid();
            Client client2020 = new Client { ClientId = ClientId, Year = 2020, RealEstatePropertyValue = 150000 };
            Client client2019 = new Client { ClientId = ClientId, Year = 2020, RealEstatePropertyValue = 190000 };
            Client client2018 = new Client { ClientId = ClientId, Year = 2020, RealEstatePropertyValue = 250000 };

            var lstClient = new List<Client>
            {
                client2020,
                client2019,
                client2018
            };


            Assert.IsTrue(RealEstateValue(lstClient), "Check with negative values");

        }

        [Test]
        public void CheckIncomeVolatitly()
        {
            //Income volatility indicator: If the Income from the previous year is significantly, 50 % difference,
            //higher or lower compared to the previous year, record the following. List the details of the difference
            //by displaying the year, previous year, Income and % change

            var clientId = Guid.NewGuid();
            Client client2020 = new Client { ClientId = clientId, Year = 2020, Income = 100000.00 };
            Client client2019 = new Client { ClientId = clientId, Year = 2019, Income = 30000.00 };

            Assert.IsTrue(CheckIncomeVolatiliy(client2020, client2019), "Check income client volatility.");


            clientId = Guid.NewGuid();
            client2020 = new Client { ClientId = clientId, Year = 2020, Income = 100000.00 };
            client2019 = new Client { ClientId = clientId, Year = 2019, Income = 650000.00 };

            Assert.IsFalse(CheckIncomeVolatiliy(client2020, client2019), "Check income client volatility.");

        }

        [Test]
        public void CheckPorcentage()
        {
            Assert.IsTrue(GetPorcentage(33.33, 100) == 33.33, "Check if the porcentage are right calculated.");
            Assert.IsTrue(GetPorcentage(-33.33, 100) == -33.33, "Check if the porcentage are right calculated.");
            Assert.IsTrue(GetPorcentage(11, 100) == 11.00, "Check if the porcentage are right calculated.");

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
