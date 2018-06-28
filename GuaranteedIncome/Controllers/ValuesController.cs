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
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {

            //double[] arr = ImmediateVariable.CalculateReturns(30, 65, 90, .05, .02, 30000, TaxStatus.roth, FilingStatus.Joint,100000);
            //for (int i = 0; i < 100; i++)
            //{
            //    Console.WriteLine("i: " + i + " value: " + arr[i]);
            //}


            return new string[] { "hi" };
        }

        // POST api/values
        [HttpPost]
        public JsonResult Post([FromBody] FormModel value)
        {
            Setup s = new Setup(value);
            Data data = s.ReturnData();

            return Json(data); 
        }

    }
}
