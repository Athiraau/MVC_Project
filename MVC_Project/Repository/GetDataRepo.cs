using Microsoft.AspNetCore.Mvc;
using MVC_Project.DTO;
using MVC_Project.Models.MenuModel;
using Newtonsoft.Json;
using NuGet.Protocol.Plugins;
using System.Drawing.Text;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace MVC_Project.Repository
{
    public class GetDataRepo
    {
        private readonly empDto _dto;

        public GetDataRepo(empDto dto)
        {
            _dto = dto;
        }


        public dynamic GetMainMenuData(string EMPCODE, string baseurl, string MainHeadID)
        {

            MenuListModel menu_ViewModel = new MenuListModel();
            List<MenuNameModel> _mN = new List<MenuNameModel>();
            List<SubMenuNameModel> _sN = new List<SubMenuNameModel>();

            menu_ViewModel.EmpCode = EMPCODE;


            using (var client = new HttpClient())
            {
                //string link = "https://uatapp.manappuram.net/MenuApi/api/MenuApi/GetMenuData/GETMAINMENU_MVC1/";

                string link = baseurl + "MenuApi/api/MenuApi/GetMenuData/GETMAINMENU_MVC1/";
                client.BaseAddress = new Uri(link + EMPCODE +"~"+ MainHeadID+ "/1");

                HttpResponseMessage result = client.GetAsync(client.BaseAddress).Result;

                if (result.IsSuccessStatusCode)
                {
                    string data = result.Content.ReadAsStringAsync().Result;
                    data = data.Replace(@"{""MenuResDto"":", @"");
                    data = data.Replace(@"}]}", @"}]");
                    data = data.Replace("\"\"", "\"");

                    _mN = JsonConvert.DeserializeObject<List<MenuNameModel>>(data);
                }
                menu_ViewModel.M_NAME = _mN;
             


              //  ModelState.Clear();
                return menu_ViewModel;
            }
        }


        public string RemoveSpecialCharacters(string str)
        {
            //-._~+/
            //  return Regex.Replace(str, "[^a-zA-Z0-9_~+/-]+", "", RegexOptions.Compiled);

            return Regex.Replace(str, "[^a-zA-Z0-9_~+/-{}]+", "", RegexOptions.Compiled);

        }

        public dynamic GetMainHeadData(string baseurl, string MainHeadID)
        {

            var responseData = " ";

            using (var client = new HttpClient())
            {

                //string link = "https://uatapp.manappuram.net/MenuApi/api/MenuApi/GetMenuData/GETMAINMENU_MVC1/";

                string link = baseurl + "MenuApi/api/MenuApi/GetMenuData/GET_MAINHEADNAME/";
                client.BaseAddress = new Uri(link + MainHeadID + "/1");

                HttpResponseMessage result = client.GetAsync(client.BaseAddress).Result;

                if (result.IsSuccessStatusCode)
                {
                    string data = result.Content.ReadAsStringAsync().Result;
                    data = data.Replace(@"{""MenuResDto"":", @"");
                    data = data.Replace(@"}]}", @"}]");
                    data = data.Replace("\"\"", "\"");

                    responseData = JsonConvert.DeserializeObject<dynamic>(data);
                }



                //  ModelState.Clear();
                return responseData;
            }
        }


     

    }
}
