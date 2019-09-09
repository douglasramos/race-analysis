using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RaceAnalysis.Application;

namespace PresenterWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RacesController : ControllerBase
    {
        private readonly IGetRace _getRace;

        public RacesController(IGetRace getRace)
        {
            _getRace = getRace;
        }

        // GET api/races/1
        [HttpGet("{id}")]
        public ActionResult<RaceModel> Get(string id)
        {
            return _getRace.Execute(id);
        }
    }
}
