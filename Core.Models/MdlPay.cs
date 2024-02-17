﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using static Core.Models.SingleFileModel;

namespace Core.Models
{
    public class MdlPay
    {

    }

    public class MdlPayInput
    {
     //   [Required(ErrorMessage = "Required AccountNo!")]
      //  public string AccountNo { get; set; }


        [Required(ErrorMessage = "Required Amount")]
        public double Amount { get; set; }
    }

    public class intrans
    {
        public List<Mdltrans> trans { get; set; }
    }
    public class Mdltrans
    {

        [Required(ErrorMessage = "Required Code")]
        public int AccNo { get; set; }
        [Required(ErrorMessage = "Required Code")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Required Amount")]
        public double Amount { get; set; }
    }

        public class SingleFileModel : ReponseModel
    {
        //     [Required(ErrorMessage = "Please enter file name")]
        //  public string FileName { get; set; }

        //   [Required(ErrorMessage = "Please select file")]
        public IFormFile File { get; set; }
        public string Datetime { get; set; }
        public string UserName { get; set; }
        public string img { get; set; }
        public string Amt { get; set; }
    }
        public class ReponseModel
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public bool IsResponse { get; set; }
    }


    public class ResVerSlip : Response
    {
        public string Bank { get; set; }
        public string Amt { get; set; }
        public string Datetime { get; set; }

        public DateTime? CreatedTime { get; set; }

    }
    public class ResultUpload : Response
    {
        public string Ref { get; set; }
        public string BankName { get; set; }
        public string Amt { get; set; }
        public string Datetime { get; set; }
    }

    public class MdlDataRes : Response
    {
        public string Ref { get; set; }
        public string BankName { get; set; }
        public List<MdlLineoutput> mdlLineoutputs = new List<MdlLineoutput>();
    }


    public class MdlLineoutput
    {
        public string Code { get; set; }
        public Rectangle rect { get; set; }
        public string Char { get; set; }
    }
}