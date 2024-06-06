using Microsoft.AspNetCore.Mvc;
using MVC_Project.Models.MenuModel;
using MVC_Project.Repository;
using Newtonsoft.Json;
using NuGet.Protocol;
using System.Diagnostics.CodeAnalysis;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MVC_Project.Controllers
{

    public class HRMController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DecryptionRepo _repo;
        private readonly GetDataRepo _Grepo;
        private readonly PostDataRepo _Prepo;
        private readonly string baseurl;
        private readonly string rootfolder;

        private readonly IConfiguration _configuration;


        public object Session { get; private set; }
        public string MainHeadID = "5";
        public string ResponseData = "";

        public HRMController(ILogger<HomeController> logger, DecryptionRepo repo, GetDataRepo grepo, PostDataRepo Prepo, IConfiguration configuration)
        {
            _logger = logger;
            _repo = repo;
            _Grepo = grepo;
            _Prepo=Prepo;

            _configuration = configuration;
            baseurl = _configuration.GetValue<string>("BasValues:BaseUrl");    //   uatapp
            rootfolder = _configuration.GetValue<string>("BasValues:root");    //   mebsapp

        }


        public IActionResult Index(string datas)
        {
            ViewData["baseurl"] = baseurl;
            ViewData["root"] = rootfolder;

            //string decryptedData = " ";
            //decryptedData = Convert.ToString(_repo.DecryptStringAES(datas));


            var empcode = HttpContext.Session.GetString("ecode");
            var empname = HttpContext.Session.GetString("EmpName");
            var branchname = HttpContext.Session.GetString("BrName");
            var UserId = HttpContext.Session.GetString("UserId");

            HttpContext.Session.SetString("headname", datas);

            MenuListModel model = new MenuListModel();
            model = (MenuListModel)_Grepo.GetMainMenuData(UserId, baseurl, MainHeadID);

            ViewData["EmpCode"] = UserId;
            ViewData["HeadName"] = datas;
            return View(model);
        }

        public IActionResult ONLINECOURSEAPPLICATION(string datas)
        {

            ViewData["baseurl"] = baseurl;
            ViewData["root"] = rootfolder;
            ViewData["HeadName"] = datas;

            ViewData["user"] = HttpContext.Session.GetString("ecode");
            var empcode = HttpContext.Session.GetString("ecode");
            var empname = HttpContext.Session.GetString("EmpName");
            var branchname = HttpContext.Session.GetString("BrName");
            var UserId = HttpContext.Session.GetString("UserId");
            var brID = HttpContext.Session.GetString("BrID");

            ViewData["BrID"] = brID;
            MenuListModel model = new MenuListModel();
            model = (MenuListModel)_Grepo.GetMainMenuData(UserId, baseurl, MainHeadID);

            ViewData["EmpCode"] = UserId;



            return View(model);

        }

        public IActionResult FIELDTOURAPPLICATION(string datas)
        {

            ViewData["baseurl"] = baseurl;
            ViewData["root"] = rootfolder;
            ViewData["HeadName"] = datas;

            ViewData["user"] = HttpContext.Session.GetString("ecode");
            var empcode = HttpContext.Session.GetString("ecode");
            var empname = HttpContext.Session.GetString("EmpName");
            var branchname = HttpContext.Session.GetString("BrName");
            var UserId = HttpContext.Session.GetString("UserId");
            var brID = HttpContext.Session.GetString("BrID");

            ViewData["BrID"] = brID;
            MenuListModel model = new MenuListModel();
            model = (MenuListModel)_Grepo.GetMainMenuData(UserId, baseurl, MainHeadID);

            ViewData["EmpCode"] = UserId;



            return View(model);

        }


        public IActionResult COURSEVERIFICATION(string datas)
        {

            ViewData["baseurl"] = baseurl;
            ViewData["root"] = rootfolder;
            ViewData["HeadName"] = datas;

            ViewData["user"] = HttpContext.Session.GetString("ecode");
            var empcode = HttpContext.Session.GetString("ecode");
            var empname = HttpContext.Session.GetString("EmpName");
            var branchname = HttpContext.Session.GetString("BrName");
            var UserId = HttpContext.Session.GetString("UserId");
            var brID = HttpContext.Session.GetString("BrID");

            ViewData["BrID"] = brID;
            MenuListModel model = new MenuListModel();
            model = (MenuListModel)_Grepo.GetMainMenuData(UserId, baseurl, MainHeadID);

            ViewData["EmpCode"] = UserId;



            return View(model);

        }


        public IActionResult PAGE123(string datas)
        {

            ViewData["baseurl"] = baseurl;
            ViewData["root"] = rootfolder;
            ViewData["HeadName"] = datas;

            ViewData["user"] = HttpContext.Session.GetString("ecode");
            var empcode = HttpContext.Session.GetString("ecode");
            var empname = HttpContext.Session.GetString("EmpName");
            var branchname = HttpContext.Session.GetString("BrName");
            var UserId = HttpContext.Session.GetString("UserId");
            var brID = HttpContext.Session.GetString("BrID");

            ViewData["BrID"] = brID;
            MenuListModel model = new MenuListModel();
            model = (MenuListModel)_Grepo.GetMainMenuData(UserId, baseurl, MainHeadID);

            ViewData["EmpCode"] = UserId;



            return View(model);

        }


        public string getAPIData(string datas)     //Get API Response
        {
            string[] DataArray = datas.Split('^');

            ViewData["baseurl"] = baseurl;
            ViewData["root"] = rootfolder;
            ViewData["HeadName"] = datas;


            string flag = DataArray[0];
            string indata = DataArray[1];
            var resData = _Grepo.GetInternalPageData(indata, flag, baseurl);
            // return resData.ToJson();
            resData = resData.Replace(@"{""RESP"":", @"");
            return resData;
        }

        public string getAPIData1(string datas)
        {
            string[] DataArray = datas.Split('^');
            ViewData["baseurl"] = baseurl;
            ViewData["root"] = rootfolder;
            ViewData["HeadName"] = datas;


            string flag = DataArray[0];
            string indata = DataArray[1];
            var resData = _Grepo.GetInternalPageData(indata, flag, baseurl);
            return resData;
        }

        //[HttpPost]
        public dynamic DocumentUpload(string datas)     //upload API Response
        {
            string[] DataArray = datas.Split('~');


            string query = DataArray[0];
            string code = DataArray[1];
            var response = _Prepo.UploadDocument(query, code, baseurl);
            return response.ToString();
                }


       [HttpPost]
        public dynamic postAPIData(string datas)
        {
            string[] DataArray = datas.Split('^');


            string flag = DataArray[0];
            string indata = DataArray[1];
            var response = _Prepo.PostInternalPageData(indata, flag, baseurl);
            return response.ToString();
        }

    }
}
