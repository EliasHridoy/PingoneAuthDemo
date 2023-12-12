using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Configuration;

namespace PingoneAuthDemo.Services
{
   public class PingoneAuthServices
    {
        public async Task<string> Login()
        {
            try
            {
                var authPath = ConfigurationManager.AppSettings["PingOne:authPath"];
                var envId = ConfigurationManager.AppSettings["PingOne:envID"];
                var clientId = ConfigurationManager.AppSettings["PingOne:adminAppID"];

                string url = $"{authPath}/{envId}/as/authorize?response_type=token&client_id={clientId}&scope=openid&redirect_uri=http://localhost:8080/PingOne/index";
                return url;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}