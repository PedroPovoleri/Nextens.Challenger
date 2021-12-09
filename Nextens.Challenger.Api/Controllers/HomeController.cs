using Microsoft.AspNetCore.Mvc;
using Nextens.Challenger.Business.Interface;
using Nextens.Challenger.Context.Interface;
using System;

namespace Nextens.Challenger.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ILoadData loadData;
        private readonly IMessagesBusiness messagesBusiness;
        private readonly IReportBusiness reportBusiness;

        public HomeController(ILoadData loadData, IMessagesBusiness messagesBusiness, IReportBusiness reportBusiness)
        {
            this.loadData = loadData;
            this.messagesBusiness = messagesBusiness;
            this.reportBusiness = reportBusiness;
        }

        [Route("/")]
        [HttpGet]
        public JsonResult Index()
        {
            return new JsonResult(loadData.LoadDataset());
        }


        [Route("/Report")]
        [HttpGet]
        public JsonResult Report()
        {
            return new JsonResult(reportBusiness.GetReports());
        }

        [Route("/Report/{clientId}")]
        [HttpGet]
        public JsonResult Report(Guid clientId)
        {
            return new JsonResult(reportBusiness.GetReportPerClient(clientId));
        }


        [Route("/Client/{clientId}")]
        [HttpGet]
        public JsonResult SearchForClient(Guid clientId)
        {
            return new JsonResult(messagesBusiness.GetMessagesFromClient(clientId));
        }

        [Route("/Message/{messageId}")]
        [HttpGet]
        public JsonResult SearchForMessage(Guid messageId)
        {
            return new JsonResult(messagesBusiness.GetMessageById(messageId));
        }
    }
}
