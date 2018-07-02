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

       //// GET api/values
       //[HttpGet]
       //public IEnumerable<string> Get()
       // {
       //     //List<Riders> Riders = new List<Riders>();
       //     //Riders.Add(Models.Riders.DeathBenefit);


       //     double[] arr = ImmediateVariable.CalculateReturns(30, 65, 90, .05, .02, 30000, TaxStatus.roth, FilingStatus.Joint, 100000, Riders);
       //     for (int i = 0; i < 100; i++)
       //     {
       //         Console.WriteLine("i: " + i + " value: " + arr[i]);
       //     }
       //     FormModel myModel = new FormModel();
       //     myModel.CurrentAge = 30;
       //     myModel.RetireAge = 65;
       //     myModel.isMale = true;
       //     myModel.Income = 100000;
       //     myModel.FilingStatus = FilingStatus.Joint;
       //     myModel.Amount = 3000;
       //     myModel.TaxType = TaxStatus.roth;
       //     myModel.isDeferred = true;


       //     Setup s = new Setup(myModel);
       //     Data data = s.ReturnData();
       //     double[] arrF = data.Fixed.First();
       //     double[] arrV = data.Variable.First();
       //     double[] arrFI = data.FixedIndexed.First();
       //     double[] b = data.Brokerage.First();


       //     for (int i = 30; i < s.deathAge; i++)
       //     {
       //         Console.WriteLine("i= " + arrF[i]);
       //     }
       //     for (int i = 30; i < s.deathAge; i++)
       //     {
       //         Console.WriteLine("j= " + arrV[i]);
       //     }
       //     for (int i = 30; i < s.deathAge; i++)
       //     {
       //         Console.WriteLine("k= " + arrFI[i]);
       //     }
       //     for (int i = 30; i < s.deathAge; i++)
       //     {
       //         Console.WriteLine("f= " + b[i]);
       //     }
       //     DeferredFixed Def = new DeferredFixed();
       //     double[] Fixed = (Def.CalculateReturns(30, 65, 95, .05, .01, 2000, TaxStatus.roth, FilingStatus.Joint, 10000, Riders.DeathBenefit)).First();
       //     for (int i = 30; i <= 95; i++)
       //     {
       //         Console.WriteLine(i + " ewq= " + Fixed[i]);
       //     }
       //     return new string[] { "hi" };
       // }

        //POST api/values
        [HttpPost]
        public JsonResult Post([FromBody] FormModel value)
        {
            Setup s = new Setup(value);
            Data data = s.ReturnData();

            return Json(data); 
        }

    }
}
