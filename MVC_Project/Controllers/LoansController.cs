using Microsoft.AspNetCore.Mvc;
using MVC_Project.Models.MenuModel;
using MVC_Project.Models;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Drawing;
using MVC_Project.Repository;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Http;

namespace MVC_Project.Controllers
{
    public class LoansController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DecryptionRepo _repo;
        private readonly GetDataRepo _Grepo;
        private readonly string baseurl;
        private readonly string rootfolder;

    private readonly IConfiguration _configuration;


        public object Session { get; private set; }
        public string MainHeadID = "1";
        public string ResponseData = "";

        public LoansController(ILogger<HomeController> logger, DecryptionRepo repo, GetDataRepo grepo, IConfiguration configuration)
        {
            _logger = logger;
            _repo = repo;
            _Grepo = grepo;
            _configuration = configuration;
            baseurl = _configuration.GetValue<string>("BasValues:BaseUrl");    //   uatapp
            rootfolder = _configuration.GetValue<string>("BasValues:root");    //   mebsapp
            
        }

        public IActionResult LoginPage()
        {
            ViewData["baseurl"] = baseurl;

            return View();
        }

        public IActionResult GoldOverdraft(string datas)
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
            String flag = "";
                        
            return View(model);
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

        public string RemoveSpecialCharacters(string str)
        {
            //-._~+/
            //  return Regex.Replace(str, "[^a-zA-Z0-9_~+/-]+", "", RegexOptions.Compiled);

            return Regex.Replace(str, "[^a-zA-Z0-9_~+/-{}]+", "", RegexOptions.Compiled);

        }
      
        public IActionResult Privacy()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // --- Dashboard
        public IActionResult Dashboard(string session)
        {
            string decryptedData = " ";

            if (string.IsNullOrEmpty(session))
            {
                session = HttpContext.Session.GetString("SessionVal");
            }


            String[] resession = session.ToString().Split("&");
            session = resession[0];
            string result1 = _repo.FromHexToBase64(session);
            var strHeader = _repo.DecryptStringAES(result1);


            String[] arrStr;
            String[] arrStrCode;

            arrStr = strHeader.ToString().Split("|");
            arrStrCode = arrStr[2].ToString().Split("!");

            ViewData["baseurl"] = baseurl;
            HttpContext.Session.SetString("EmpName", arrStr[3]);
            HttpContext.Session.SetString("ecode", arrStr[2]);
            HttpContext.Session.SetString("BrName", arrStr[1]);
            HttpContext.Session.SetString("UserId", arrStrCode[0]);
            HttpContext.Session.SetString("SessionVal", session);
            HttpContext.Session.SetString("BrID", arrStr[0]);
           // HttpContext.Session.SetString("headname", "OTHERS");



            return View();
        }


        
        public IActionResult SMAReportHome(string datas)
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


        public IActionResult SMAREGION(string zoneid1)
        {
            string[] da = zoneid1.Split('~');

            ViewData["zoneid"] = da[1].ToString();
            ViewData["SelectedDate"] = da[0].ToString();

            ViewData["baseurl"] = baseurl;
            ViewData["root"] = rootfolder;


            //string baseurl = _configuration.GetConnectionString("Baseurl").ToString();




            var empcode = HttpContext.Session.GetString("ecode");
            var empname = HttpContext.Session.GetString("EmpName");
            var branchname = HttpContext.Session.GetString("BrName");
            var UserId = HttpContext.Session.GetString("UserId");


            MenuListModel model = new MenuListModel();
            model = (MenuListModel)_Grepo.GetMainMenuData(UserId, baseurl,MainHeadID);

            ViewData["EmpCode"] = UserId;


            return View(model);
        }

        public IActionResult SMAAREA(string indata)
        {

            string[] da = indata.Split('~');

            ViewData["Regionid"] = da[1];
            ViewData["SelectedDate"] = da[0].ToString();
            ViewData["baseurl"] = baseurl;
            ViewData["root"] = rootfolder;

            var empcode = HttpContext.Session.GetString("ecode");
            var empname = HttpContext.Session.GetString("EmpName");
            var branchname = HttpContext.Session.GetString("BrName");
            var UserId = HttpContext.Session.GetString("UserId");


            MenuListModel model = new MenuListModel();
            model = (MenuListModel)_Grepo.GetMainMenuData(UserId, baseurl,MainHeadID);

            ViewData["EmpCode"] = UserId;


            return View(model);
        }

        public IActionResult SMABRANCH(string data)
        {
            string[] da = data.Split('~');
            ViewData["Areaid"] = da[1];
            ViewData["SelectedDate"] = da[0].ToString();


            ViewData["baseurl"] = baseurl;
            ViewData["root"] = rootfolder;

            var empcode = HttpContext.Session.GetString("ecode");
            var empname = HttpContext.Session.GetString("EmpName");
            var branchname = HttpContext.Session.GetString("BrName");
            var UserId = HttpContext.Session.GetString("UserId");


            MenuListModel model = new MenuListModel();
            model = (MenuListModel)_Grepo.GetMainMenuData(UserId, baseurl,MainHeadID);

            ViewData["EmpCode"] = UserId;


            return View(model);
        }

        public IActionResult SMAPLEDGE(string data)
        {
                string[] da = data.Split('~');
                ViewData["Branchid"] = da[1];
                ViewData["SelectedDate"] = da[0].ToString();

                ViewData["baseurl"] = baseurl;
            ViewData["root"] = rootfolder;

            var empcode = HttpContext.Session.GetString("ecode");
            var empname = HttpContext.Session.GetString("EmpName");
            var branchname = HttpContext.Session.GetString("BrName");
            var UserId = HttpContext.Session.GetString("UserId");


            MenuListModel model = new MenuListModel();
            model = (MenuListModel)_Grepo.GetMainMenuData(UserId, baseurl,MainHeadID);

            ViewData["EmpCode"] = UserId;

            return View(model);
        }

        public IActionResult SMAERRORBALANCE(string datas)
        {
          
            ViewData["baseurl"] = baseurl;
            ViewData["root"] = rootfolder;
            ViewData["HeadName"] = datas;

            // string decryptedData = " ";
            //  decryptedData = Convert.ToString(_repo.DecryptStringAES(datas));
            //  decryptedData = "18906";

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

        public IActionResult SMAFINREPORT(string datas)
        {

            ViewData["baseurl"] = baseurl;
            ViewData["root"] = rootfolder;
            ViewData["HeadName"] = datas;

            // string decryptedData = " ";
            //  decryptedData = Convert.ToString(_repo.DecryptStringAES(datas));
            //  decryptedData = "18906";

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

        public IActionResult SMAPRODUCT(string datas)
        {

            ViewData["baseurl"] = baseurl;
            ViewData["root"] = rootfolder;
            ViewData["HeadName"] = datas;

            // string decryptedData = " ";
            //  decryptedData = Convert.ToString(_repo.DecryptStringAES(datas));
            //  decryptedData = "18906";

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

        public IActionResult SMA2REPORT(string datas)
        {

            string flag = datas;
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

        public string getAPIData(string datas)
        {
            string[] DataArray = datas.Split('~');


            string flag = DataArray[0];
           string  indata = DataArray[1];   
            var resData = _Grepo.GetInternalPageData(indata, flag, baseurl);
            return resData; 
        }
    }
}
