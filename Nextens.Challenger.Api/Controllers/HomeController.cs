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
        private readonly IMessagesBusiness messages;
        private readonly IReportBusiness reportBusiness;

        public HomeController(ILoadData loadData, IMessagesBusiness messages, IReportBusiness reportBusiness)
        {
            this.loadData = loadData;
            this.messages = messages;
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
            return new JsonResult(loadData.LoadDataset());
        }

        [Route("/SearchForClient")]
        [HttpGet]
        public JsonResult SearchForClient(Guid clientId)
        {
            return new JsonResult(messages.GetMessagesFromClient(clientId));
        }

        [Route("/SearchForMessage")]
        [HttpGet]
        public JsonResult SearchForMessage(Guid messageId)
        {
            return new JsonResult(messages.GetMessageById(messageId));
        }
    }
}
