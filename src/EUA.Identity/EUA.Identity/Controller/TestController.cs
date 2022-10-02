using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EUA.Identity.Controller
{
    [Route("api/v1/[controller]/[action]")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Hello()
        {
            return Ok("Hello!");
        }
    }
}
