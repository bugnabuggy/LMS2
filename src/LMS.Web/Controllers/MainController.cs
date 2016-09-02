using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace LMS.Web.Controllers
{
    public class MainController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            var path = Directory.GetCurrentDirectory() + "\\wwwroot\\"+"shell.html";
            var content = System.IO.File.ReadAllText(path);

            return Content(content, "text/html");
        }
    }
}
