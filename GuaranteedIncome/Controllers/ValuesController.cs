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
        //{
        //    FormModel myModel = new FormModel();
        //    myModel.CurrentAge = 61;
        //    myModel.RetireAge = 65;
        //    myModel.isMale = false;
        //    myModel.Income = 80000;
        //    myModel.FilingStatus = FilingStatus.Joint;
        //    myModel.Amount = 3000;
        //    myModel.TaxType = TaxStatus.roth;
        //    myModel.isDeferred = true;
        //    myModel.WithdrawalUntil = 20;
        //    myModel.Riders = new List<Riders>();
        //    Setup s = new Setup(myModel);
        //    Data data = s.ReturnData();


        //    double fix = data.Fixed;
        //    double[] arrV = data.Variable.First();
        //    double[] arrFI = data.FixedIndexed.First();
        //    double[] b = data.Brokerage.First();
        //    Console.WriteLine("Fixed amount" + fix);
        //    for (int i = 0; i < 20; i++)
        //    {
        //        Console.WriteLine("j= " + i + "   " + arrV[i]);
        //    }
        //    for (int i = 0; i < 20; i++)
        //    {
        //        Console.WriteLine("k= " + i + "   " + arrFI[i]);
        //    }
        //    for (int i = 0; i < 20; i++)
        //    {
        //        Console.WriteLine("f= " + i + "   " + b[i]);
        //    }
        //    Console.WriteLine("Brokerage median   " + data.BrokerageMedian);
        //    Console.WriteLine("FixedIndexed median   " + data.FixedIndexedMedian);
        //    Console.WriteLine("Variable median   " + data.VariableMedian);
        //    return new string[] { "hi" };
        //}

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
