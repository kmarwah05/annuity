using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GuaranteedIncome.Models;

namespace GuaranteedIncome.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {

        // POST api/values
        [HttpPost]
        public JsonResult Post([FromBody] FormModel value)
        {
            return null; 
        }

    }
}
