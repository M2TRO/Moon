using Core.Interfaces;
using Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class ToolsxService : IToolsxService
    {
        public void ConverBase(SingleFileModel img64)
        {

        }

        public ResultUpload Upload(SingleFileModel input)
        {

            ResultUpload mdlDataRes = new ResultUpload();
            try
            {
                if (input.File != null)
                {
                    mdlDataRes.IsSuccess = true;

                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files");

                    //create folder if not exist
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);

                    //get file extension
                    FileInfo fileInfo = new FileInfo(input.File.FileName);
                    //  string fileName = model.FileName + fileInfo.Extension;

                    string fileNameWithPath = Path.Combine(path, fileInfo.Name);
                    byte[] dataArray = new byte[100000];
                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        input.File.CopyTo(stream);
                    }

                    UriBuilder _url = new UriBuilder();
                    _url.Scheme = "http";


                    //   _url.Host = "ec2-18-141-174-195.ap-southeast-1.compute.amazonaws.com:7005";
                    _url.Host = "127.0.0.1:7005";
                    _url.Path = "api/Upload?filename=" + input.File.FileName;
                    string urlquery = _url.ToString().Replace("[", "").Replace("]", "");
                    HttpWebRequest req = (HttpWebRequest)WebRequest.Create(urlquery);
                    string username = "admin";
                    string password = "admin";
                    string encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(username + ":" + password));
                    req.Headers.Add("Authorization", "Basic " + encoded);
                    req.PreAuthenticate = true;
                    string boundaryString = "----------" + DateTime.Now.Ticks.ToString("x");
                    req.ContentType = "multipart/form-data; boundary=" + boundaryString;
                    req.KeepAlive = true;
                    req.Method = "POST";
                    req.Timeout = Timeout.Infinite;

                    MemoryStream postDataStream = new MemoryStream();
                    StreamWriter postDataWriter = new StreamWriter(postDataStream);
                    string Name = input.File.FileName;
                    postDataWriter.Write("\r\n--" + boundaryString + "\r\n");
                    postDataWriter.Write("Content-Disposition: form-data;"
                    + "name=\"{0}\";"
                    + "filename=\"{1}\""
                    + "\r\nContent-Type: {2}\r\n\r\n",
                    "myFile" + "0",
                    Path.GetFileName(fileNameWithPath),
                    Path.GetExtension(fileNameWithPath));
                    postDataWriter.Flush();



                    FileStream fileStream = new FileStream(fileNameWithPath, FileMode.Open, FileAccess.Read);
                    byte[] buffer = new byte[1024];
                    int bytesRead = 0;
                    while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        postDataStream.Write(buffer, 0, bytesRead);
                    }
                    fileStream.Close();


                    postDataWriter.Write("\r\n--" + boundaryString + "--\r\n");
                    postDataWriter.Flush();

                    req.ContentLength = postDataStream.Length;


                    using (Stream s = req.GetRequestStream())
                    {
                        postDataStream.WriteTo(s);
                    }
                    postDataStream.Close();

                    var httpResponse = (HttpWebResponse)req.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {

                        var result = streamReader.ReadToEnd();

                        var json = JsonConvert.DeserializeObject<MdlDataRes>(result);
                        if (json.IsSuccess == true)
                        {
                            mdlDataRes.Datetime = json.mdlLineoutputs.Where(m => m.Code == "Datetime").FirstOrDefault().Char + "";
                            mdlDataRes.Amt = json.mdlLineoutputs.Where(m => m.Code == "Price").FirstOrDefault().Char + "";
                            mdlDataRes.Ref = json.Ref;
                            mdlDataRes.BankName = json.BankName;
                            mdlDataRes.IsSuccess = json.IsSuccess;
                        }
                        else
                        {
                            mdlDataRes.Ref = json.Ref;
                            mdlDataRes.IsSuccess = json.IsSuccess;
                            mdlDataRes.Message = json.Message;
                        }
                        //if (input.File.Length > 0)
                        //{
                        //    using (var ms = new MemoryStream())
                        //    {
                        //        input.File.CopyTo(ms);
                        //        var fileBytes = ms.ToArray();
                        //        string s = Convert.ToBase64String(fileBytes);
                        //        mdlDataRes.img = s;
                        //    }
                        //}


                        //      byte[] data = StreamExtensions.ReadAllBytes(fileStream.);

                        //    mdlDataRes.message = "File upload successfully";
                    }

                    //    mdlDataRes.Success = true;

                }
            }
            catch (Exception ex)
            {
                mdlDataRes.Message = "Upload File Error!!";
                mdlDataRes.IsSuccess = false;
            }
            return mdlDataRes;
        }
    }
}
