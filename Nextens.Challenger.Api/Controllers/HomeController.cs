using Microsoft.AspNetCore.Mvc;
using Nextens.Challenger.Context.Interface;

namespace Nextens.Challenger.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ILoadData loadData;

        public HomeController(ILoadData loadData)
        {
            this.loadData = loadData;
        }

        [Route("/")]
        [HttpGet]
        public JsonResult Index()
        {
            return new JsonResult(loadData.LoadDataset());
        }

    }
}
