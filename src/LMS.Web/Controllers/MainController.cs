using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using LMS.Web.Infrastructure;
using Microsoft.Extensions.Options;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace LMS.Web.Controllers
{
    public class MainController : Controller
    {
        private IOptions<Settings> _settings;

        public MainController(IOptions<Settings> settings)
        {
            _settings = settings;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var path = _settings.Value.Wwwroot+ "\\"+_settings.Value.HtmlShell;
            var content = System.IO.File.ReadAllText(path);

            return Content(content, "text/html");
        }

        public IActionResult Settings()
        {
            var obj = _settings.Value;
            var dict = obj.GetType().GetProperties().Select(x=>x.Name + " = " + x.GetValue(obj)).ToList();

            return Content(String.Join("\n\r",dict));
        }
    }
}
