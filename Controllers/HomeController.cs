using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CS_Random_Passcode.Models;
using Session_Model.Models;
using Microsoft.AspNetCore.Http;

namespace CS_Random_Passcode.Controllers
{
    public class HomeController : Controller
    {

#region //_______________________________________________/Logger\_______________________________________________//
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
#endregion 
        //---------------------------------------------------------------------------------------------------//

#region //_______________________________________________/Index\_______________________________________________//
        public IActionResult Index()
        {
            
        #region //_____________________/Session Example\_____________________//
            // *Inside controller methods*
            // To store a string in session we use ".SetString"
            // The first string passed is the key and the second is the value we want to retrieve later
            HttpContext.Session.SetString("UserName", "Samantha");
            // To retrieve a string from session we use ".GetString"
            string LocalVariable = HttpContext.Session.GetString("UserName");
            
            // To store an int in session we use ".SetInt32"
            HttpContext.Session.SetInt32("UserAge", 28);
            // To retrieve an int from session we use ".GetInt32"
            int? IntVariable = HttpContext.Session.GetInt32("UserAge");
            // ViewBag.Count = HttpContext.Session.GetInt32("count");
        #endregion 
                //------------------------------------------------------//

        #region //_____________________/List Session\_____________________//
            List<object> NewList = new List<object>();
 
            HttpContext.Session.SetObjectAsJson("TheList", NewList);
            
            // Notice that we specify the type ( List ) on retrieval
            List<object> Retrieve = HttpContext.Session.GetObjectFromJson<List<object>>("TheList");

#endregion 
                //------------------------------------------------------//
        
        #region //_____________________/Random Passcode Generator\_____________________//
            if (HttpContext.Session.GetInt32("attempt") == null)
            {
                HttpContext.Session.SetInt32("attempt", 1);
            }
            else
            {
                int count = HttpContext.Session.GetInt32("attempt").GetValueOrDefault();
                HttpContext.Session.SetInt32("attempt", count + 1);
            }
            ViewBag.count = HttpContext.Session.GetInt32("attempt");
            Random rand = new Random();
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var newstring = new char[14];
            for (int i = 0; i < newstring.Length; i++)
            {
                newstring[i] = chars[rand.Next(0, 36)];
            }
            string newString = new String(newstring);
            ViewBag.password = newString;
        #endregion
                //------------------------------------------------------//

            return View();                                    
        }

#endregion 
        //---------------------------------------------------------------------------------------------------//

#region //_______________________________________________/Privacy\_______________________________________________//
        public IActionResult Privacy()
        {
            return View();
        }
#endregion 
        //---------------------------------------------------------------------------------------------------//

#region //_______________________________________________/Error\_______________________________________________//
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

#endregion 
        //---------------------------------------------------------------------------------------------------//

#region //_______________________________________________/TempData\_______________________________________________//
            // Other code
        // public IActionResult Method()
        // {
        //     TempData["Variable"] = "Hello World";
        //     return RedirectToAction("OtherMethod");
        // }
        // public IActionResult OtherMethod()
        // {
        //     Console.WriteLine(TempData["Variable"]);
        //     // writes "Hello World" if redirected to from Method()
        // }
#endregion
        //---------------------------------------------------------------------------------------------------//

    }
}
