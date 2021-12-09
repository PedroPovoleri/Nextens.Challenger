using Nextens.Challenger.Model;
using System;
using System.Collections.Generic;

namespace Nextens.Challenger.Business.Interface
{
    public interface IReportBusiness
    {
        public List<Report> GetReports();

        public Report GetReportPerClient(Guid clientId);
    }
}
