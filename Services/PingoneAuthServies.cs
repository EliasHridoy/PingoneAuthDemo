using PingOneDemo.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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
        public async Task<string> GetAccessToken()
        {
            //var client = _httpClientFactory.CreateClient("PingOneApiClient");

            var authPath = ConfigurationManager.AppSettings["PingOne:authPath"];
            var clientId = ConfigurationManager.AppSettings["PingOne:adminAppID"];
            var clientSecret = ConfigurationManager.AppSettings["PingOne:adminAppSecret"];
            var envId = ConfigurationManager.AppSettings["PingOne:envID"];
            string tokenURL = $"{authPath}/{envId}/as/token";

            var basicAuth = "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));


            var client = new RestClient(tokenURL);
            //client.Timeout = -1;
            var request = new RestRequest("", Method.Post);
            request.AddHeader("Authorization", basicAuth);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("grant_type", "client_credentials");
            RestResponse response = client.Execute(request);
            //Console.WriteLine(response.Content
            if (response.IsSuccessful)
            {
                JsonDocument jsonResponse = JsonDocument.Parse(response.Content);
                var accessToken = jsonResponse.RootElement.GetProperty("access_token").GetString();
                return accessToken;
            }
            return "";
        }
        public async Task<string> Register(RegistrationViewModel model)
        {
            try
            {
                var apiPath = ConfigurationManager.AppSettings["PingOne:apiPath"];
                var envId = ConfigurationManager.AppSettings["PingOne:envID"];
                var popID = ConfigurationManager.AppSettings["PingOne:popID"];

                var client = new RestClient($"{apiPath}/environments/{envId}/users");
                string accessToken = await GetAccessToken();
                //string ssha512Pass = CalculateSHA512Hash(model.password);
                var requestModel = new RegistrationModel()
                {
                    email = model.email,
                    name = new Name { given = model.firstName, family = model.lastName },
                    population = new Population { id = popID },
                    username = model.email,//$"{model.firstName}_{DateTime.UtcNow.ToString("ddMMyyHHmm")}",
                    department = "User",
                    locales = new List<string> { "Dhaka", "Bangladesh" },
                    password = new Password { value = model.password, forceChange = false },
                };

                var request = new RestRequest("", Method.Post);
                request.AddHeader("Authorization", $"Bearer {accessToken}");
                request.AddHeader("content-type", "application/vnd.pingidentity.user.import+json");
                request.AddParameter("application/vnd.pingidentity.user.import+json", JsonSerializer.Serialize(requestModel), ParameterType.RequestBody);
                RestResponse response = client.Execute(request);
                return response.Content.ToString();

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}