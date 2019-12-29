using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Generator.Identity.Models.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Generator.Identity.Controllers
{
    [Route("[controller]")]
    [AllowAnonymous]
    public class OriginController : Controller
    {
        private readonly AppSettings _appSettings;
        public OriginController(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        [HttpGet]
        public IActionResult Origin()
        {
            Console.WriteLine(_appSettings.Site.PublicOrigin.ToString());
            Console.WriteLine(_appSettings.Site.PublicOrigin);
            Console.WriteLine("!!!!!!!!!!!!!!!!!!!!");
            Console.WriteLine("!!!!!!!!!!!!!!!!!!!!");
            Console.WriteLine("!!!!!!!!!!!!!!!!!!!!");
            Console.WriteLine("!!!!!!!!!!!!!!!!!!!!");
            return Ok(_appSettings.Site.PublicOrigin.ToString());
        }
    }
}