using Newtonsoft.Json;
using Serilog;
using SOP.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SOP.ComService
{
    public static class DataProvider
    {
        private static byte[] entropy = new byte[8] { 0x01, 0x02, 0x86, 0x3b, 0x0a, 0x49, 0x55, 0x8c };

        private static HttpClient _client;

        static DataProvider()
        {
            _client = new HttpClient();
        }

        public static async Task<MessageReport> TestAuth()
        {
            var report = new MessageReport();

            try
            {
                var byteArray = Encoding.UTF8.GetBytes($"{StaticFields.Username}:{StaticFields.Password}");

                var requestMsg = new HttpRequestMessage()
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(StaticFields.APIURL.TrimEnd('/') + "/testauth")
                };

                requestMsg.Headers.Authorization
                     = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                var responseMsg = await _client.SendAsync(requestMsg);

                if (responseMsg.IsSuccessStatusCode)
                {
                    report.Succeeded = true;
                }
            }
            catch (Exception ex)
            {
                Log.Fatal(ex.Message);
            }

            return report;
        }

        public static async Task<MessageReport> CheckLogin(string id, string pw)
        {
            var report = new MessageReport() { Succeeded = false };

            try
            {
                var loginModel = new LoginModel() { Username = id, Password = pw };

                var requestMsg = new HttpRequestMessage()
                {
                    Method = HttpMethod.Post,
                    Content = new StringContent(JsonConvert.SerializeObject(loginModel), Encoding.UTF8, "application/json"),
                    RequestUri = new Uri(StaticFields.APIURL.TrimEnd('/') + "/login")
                };

                var responseMsg = await _client.SendAsync(requestMsg);

                if (responseMsg.IsSuccessStatusCode)
                {
                    report.Succeeded = true;
                }
            }
            catch (Exception ex)
            {
                Log.Fatal(ex.Message);
            }

            return report;
        }

        public static async Task<MessageReport> PushRating(object ratingType)
        {
            var report = new MessageReport();

            try
            {
                var byteArray = Encoding.UTF8.GetBytes($"{StaticFields.Username}:{StaticFields.Password}");

                var requestMsg = new HttpRequestMessage()
                {
                    Method = HttpMethod.Post,
                    Content = new StringContent(JsonConvert.SerializeObject(ratingType)),
                    RequestUri = new Uri(StaticFields.APIURL.TrimEnd('/') + "/rating")
                };

                requestMsg.Headers.Authorization
                     = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                var responseMsg = await _client.SendAsync(requestMsg);

                if (responseMsg.IsSuccessStatusCode)
                {
                    report.Succeeded = true;
                }
            }
            catch (Exception ex)
            {
                Log.Fatal(ex.Message);
            }

            return report;
        }

        public static string Encrypt(this string text)
        {
            return Convert.ToBase64String(
                ProtectedData.Protect(
                    Encoding.UTF8.GetBytes(text), entropy, DataProtectionScope.LocalMachine));
        }

        public static string Decrypt(this string text)
        {
            return Encoding.UTF8.GetString(
                ProtectedData.Unprotect(
                     Convert.FromBase64String(text), entropy, DataProtectionScope.LocalMachine));
        }
    }
}
