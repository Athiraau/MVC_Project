using Microsoft.AspNetCore.Mvc;
using MVC_Project.Models;
using System.Diagnostics;
using MVC_Project.Models.MenuModel;
using Newtonsoft.Json;
using System.Text.Encodings.Web;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.RegularExpressions;
using System.Data;
using MVC_Project.Repository;
using NuGet.Configuration;

namespace MVC_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DecryptionRepo _repo;
        private readonly GetDataRepo _Grepo;
        private readonly string baseurl;
        private readonly string rootfolder;

        private readonly IConfiguration _configuration;

        public object Session { get; private set; }
        public string MainHeadID = "1";

        public HomeController(ILogger<HomeController> logger, DecryptionRepo repo, GetDataRepo grepo, IConfiguration configuration)
        {
            _logger = logger;
            _repo = repo;
            _Grepo = grepo;
            _configuration = configuration;
            baseurl = _configuration.GetValue<string>("BasValues:BaseUrl");
            rootfolder = _configuration.GetValue<string>("BasValues:root");
        }

        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Logout(string datas)
        {
           HttpContext.Session.Clear();
            //HttpContext.Session.SetString("EmpName", null);
            //HttpContext.Session.SetString("ecode", null);
            //HttpContext.Session.SetString("BrName", null);
            //HttpContext.Session.SetString("UserId", null);
            //HttpContext.Session.SetString("SessionVal", null);
            //HttpContext.Session.SetString("BrID", null);
            //HttpContext.Session.SetString("headname", null);

             //model.M_NAME = "";
            return View();
           
        }
    }
}