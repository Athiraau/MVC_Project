using Microsoft.AspNetCore.Mvc;
using MVC_Project.DTO;
using MVC_Project.Models.MenuModel;
using Newtonsoft.Json;
using NuGet.Protocol.Plugins;
using System.Drawing.Text;
using System.Net.NetworkInformation;
using System.Net;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MVC_Project.Repository
{
    public class PostDataRepo
    {
        private readonly empDto _dto;

        public PostDataRepo(empDto dto)
        {
            _dto = dto;
        }


      

                public string RemoveSpecialCharacters(string str)
                {
                    //-._~+/
                    //  return Regex.Replace(str, "[^a-zA-Z0-9_~+/-]+", "", RegexOptions.Compiled);

                    return Regex.Replace(str, "[^a-zA-Z0-9_~+/-{}]+", "", RegexOptions.Compiled);

                }

             
        public async Task<dynamic> PostInternalPageData(string indata, string flag, string baseurl)
        {
            string data = "";
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string content = JsonConvert.SerializeObject(new { p_pagevalue = indata, p_paravalue = "1", p_flag= flag});
                    var buffer = Encoding.UTF8.GetBytes(content);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType= new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                        

                    string url2 = baseurl + "smareportsapi/api/SMAModule/PostSMAData"; 
                    HttpResponseMessage response2 = await client.PostAsync(url2, byteContent);
                    if(response2.IsSuccessStatusCode)
                    {
                        data = response2.Content.ReadAsStringAsync().Result;
                        data = data.Replace("\"\"", "");
                        data = data.Replace("\'", "");
                        data = data.Replace(@"""RESP"":", @"");
                        
                    }

                    //response2.EnsureSuccessStatusCode();
                    //string responseBody2 = await response2.Content.ReadAsStringAsync();
                   // return responseBody2;
                   
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("\nException Caught!");
                    Console.WriteLine("MessAge :{0} ", e.Message);
                }
                data = RemoveSpecialCharacters(data);
              
                return data;
            }
               
           
       
            
        }


    }
}

