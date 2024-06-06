using Microsoft.AspNetCore.Mvc;
using MVC_Project.Models.MenuModel;
using MVC_Project.Repository;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MVC_Project.Controllers
{
    public class OthersController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly DecryptionRepo _repo;
        private readonly GetDataRepo _Grepo;
        private readonly PostDataRepo _Prepo;

        private readonly string baseurl;
        private readonly string rootfolder;

        private readonly IConfiguration _configuration;
        public string MainHeadID = "7";

        public OthersController(ILogger<HomeController> logger, DecryptionRepo repo, GetDataRepo grepo, PostDataRepo Prepo, IConfiguration configuration)
        {
            _logger = logger;
            _repo = repo;
            _Grepo = grepo;
            _Prepo = Prepo;
            _configuration = configuration;
            baseurl = _configuration.GetValue<string>("BasValues:BaseUrl");
            rootfolder = _configuration.GetValue<string>("BasValues:root");
        }
        public object Session { get; private set; }
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

        public IActionResult DEVICEUPDATION(string datas)
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
        public string getAPIData(string datas)
        {
            string[] DataArray = datas.Split('^');


            string flag = DataArray[0];
            string indata = DataArray[1];
            var resData = _Grepo.GetInternalPageData(indata, flag, baseurl);
            return resData;
        }

       public dynamic  postAPIData(string datas)
        {
            string[] DataArray = datas.Split('^');


            string flag = DataArray[0];
            string indata = DataArray[1];
            var resData = _Prepo.PostInternalPageData(indata, flag, baseurl);
            return resData;
        }



    }
}