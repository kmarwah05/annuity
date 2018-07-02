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
            FormModel myModel = new FormModel();
            myModel.CurrentAge = 61;
            myModel.RetireAge = 65;
            myModel.isMale = false;
            myModel.Income = 80000;
            myModel.FilingStatus = FilingStatus.Joint;
            myModel.Amount = 1500;
            myModel.TaxType = TaxStatus.roth;
            myModel.isDeferred = true;
            myModel.WithdrawalUntil = 20;
            myModel.Riders = new List<Riders>();
            Setup s = new Setup(myModel);
            Data data = s.ReturnData();

            Console.WriteLine(s.age);
            Console.WriteLine(s.retireAge);
            Console.WriteLine(s.deathAge);
            Console.WriteLine(s.Gender);
            Console.WriteLine(s.income);
            Console.WriteLine(s.status);
            Console.WriteLine(s.amount);
            Console.WriteLine(s.TaxType);
            Console.WriteLine(s.isDeferred);
            Console.WriteLine(s.WithdrawalUntil);
            



            double[] arrF = data.Fixed;
            double[] arrV = data.Variable.First();
            double[] arrFI = data.FixedIndexed.First();
            double[] b = data.Brokerage.First();
            //ImmediateFixed Def = new ImmediateFixed();
            // double[] arrF = Def.CalculateReturns(61, 65, 85, .03, 0, 150000, TaxStatus.qualified, FilingStatus.Joint, 80000, new List<Riders>()).First();

            //for (int i = 30; i < 86; i++)
            //{
            //    Console.WriteLine("i= " + i + "  " + arrF[i]);
            //}
            //for (int i = 30; i < 86; i++)
            //{
            //    Console.WriteLine("j= " + i + "   " + arrV[i]);
            //}
            for (int i = 30; i < 86; i++)
            {
                Console.WriteLine("k= " + i + "   " + arrFI[i]);
            }
            //for (int i = 30; i < 86; i++)
            //{
            //    Console.WriteLine("f= " + i + "   " + b[i]);
            //}
            //DeferredFixed Def = new DeferredFixed();
            //double[] Fixed = (Def.CalculateReturns(30, 65, 95, .05, .01, 2000, TaxStatus.roth, FilingStatus.Joint, 10000)).First();
            //for (int i = 30; i <= 95; i++)
            //{
            //    Console.WriteLine(i + " ewq= " + Fixed[i]);
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
