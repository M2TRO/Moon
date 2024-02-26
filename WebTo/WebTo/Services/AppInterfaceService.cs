using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Text;
using System.Web.Http;
using WebTo.Interfaces;
using WebTo.Models;

namespace WebTo.Services
{
    public class AppInterfaceService: IAppInterfaceService 
    {

        public AppInterfaceService() { }



        public object? ResApiFileContent(RestApiType restApiType, string url, object input, string JWT = null)
        {

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            string strjson = "";
            if (!string.IsNullOrEmpty(JWT))
            {
                string authorization = JWT;
                req.Headers["Authorization"] = "Bearer " + authorization;
            }
            req.Headers.Add("Content-Transfer-Encoding", "8bit");
            req.ContentType = "application/json";
            req.KeepAlive = true;
            req.Method = restApiType.ToString();
            req.Timeout = Timeout.Infinite;
            string datasent = JsonConvert.SerializeObject(input, Formatting.Indented);
            ASCIIEncoding encoding = new ASCIIEncoding();
            if (input != null)
            {

                byte[] byte1 = encoding.GetBytes(datasent);
                req.ContentLength = byte1.Length;

                var newStream = req.GetRequestStream();
                newStream.Write(byte1, 0, byte1.Length);

            }
            string msg = string.Empty;
            try
            {

                var httpResponse = (HttpWebResponse)req.GetResponse();

                return httpResponse;

            }
            catch (Exception ex)
            {
            }


            return null;
        }
        public object? RestApiController(RestApiType restApiType, string url, object input, string JWT = null)
        {

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            string strjson = "";
            if (!string.IsNullOrEmpty(JWT))
            {
                string authorization = JWT;
                req.Headers["Authorization"] = "Bearer " + authorization;
            }
            req.Headers.Add("Content-Transfer-Encoding", "8bit");
            req.ContentType = "application/json";
            req.KeepAlive = true;
            req.Method = restApiType.ToString();
            req.Timeout = Timeout.Infinite;
            string datasent = JsonConvert.SerializeObject(input, Formatting.Indented);
            ASCIIEncoding encoding = new ASCIIEncoding();
            if (input != null)
            {

                byte[] byte1 = encoding.GetBytes(datasent);
                req.ContentLength = byte1.Length;

                var newStream = req.GetRequestStream();
                newStream.Write(byte1, 0, byte1.Length);

            }
            string msg = string.Empty;
            try
            {

               var httpResponse = (HttpWebResponse)req.GetResponse();
            
                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        //  i++;


                        strjson = streamReader.ReadToEnd();




                    }
                }
                else
                {

                    throw new HttpResponseException(httpResponse.StatusCode);

                }

            }
            catch (Exception ex)
            {


                msg = ex.Message;
            }
      
    
            return JsonConvert.DeserializeObject<object>(strjson);




        }

       
    }
}
