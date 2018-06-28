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
            double[] arr = MonteCarlo.populateArray(.05, .05, false, 20000, 30);
            for (int i = 0; i < 1000L; i++)
            {
                Console.WriteLine("i: " + i + " value: " + arr[i]);
            }


            return new string[] { "hi" };
        }

        // POST api/values
        [HttpPost]
        public JsonResult Post([FromBody] FormModel value)
        {
           double[] arr= MonteCarlo.populateArray(.05, .01, false, 20000, 30);
            for(int i = 0; i < 1000L; i++)
            {
                Console.WriteLine("i: " + i + " value: " + arr[i]);
            }
            return null; 
        }

    }
}
