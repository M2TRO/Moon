using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using static Core.Models.SingleFileModel;

namespace Core.Models
{
    public class MdlPay
    {

    }

    public class MdlGetBank
    {
        public int? BankId { get; set; }
    }
    public class MdlPayInput
    {
        public int? TransBankId { get; set; }

        [Required(ErrorMessage = "Required AccountNo!")]
        public string AccountNo { get; set; }

        [Required(ErrorMessage = "Required Amount")]
        public double Amount { get; set; }
    }

    public class intrans
    {
        public List<Mdltrans> trans { get; set; }
    }
    public class Mdltrans
    {

        [Required(ErrorMessage = "Required AccNo")]
        public int AccNo { get; set; }

        [Required(ErrorMessage = "Required Code")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Required Amount")]
        public double Amount { get; set; }
    }



    public class MdlGenVer 
    {

        //[Required(ErrorMessage = "Required AccInput")]
        //public string AccInput { get; set; }

        //[Required(ErrorMessage = "Required BankCode")]
        //public string BankCode { get; set; }

        [Required(ErrorMessage = "Required Amount")]
        public string Amount { get; set; }

        [Required(ErrorMessage = "Required Amount")]
        public string OrderId { get; set; }
    }
         public class SingleFileModel : ReponseModel
       {
        //     [Required(ErrorMessage = "Please enter file name")]
        //  public string FileName { get; set; }

        //   [Required(ErrorMessage = "Please select file")]


        public IFormFile File { get; set; }
        //public string AccInput { get; set; }
        public DateTime Datetime { get; set; }
        //public string BankCode { get; set; }
        //public string Amount { get; set; }
    }
        public class ReponseModel
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public bool IsResponse { get; set; }

    }
    public class MdlData
    {
        public string sender { get; set; }
        public string msg { get; set; }
        public string Code { get; set; }
    }
    public class MdlSaveTran
    {

        public  string? Amount { get; set; }
        public string? OrderId { get; set; }
        public string? Bankcode { get; set; }
        public string? LogId { get; set; }
        public bool state { get; set; }

    }
    public class Mdlaccept
    {
       public int TranId { get; set; }

    }

    public class ResVerSlip : Response
    {
        public string Bank { get; set; }
        public string Amt { get; set; }
        public string Datetime { get; set; }
        public bool VerifyAI { get; set; }
        public bool Verify { get; set; }
        public DateTime? CreatedTime { get; set; }

    }
    public class ResultUpload : Response
    {
        public string Ref { get; set; }
        public string BankName { get; set; }
        public string Amt { get; set; }
        public string Datetime { get; set; }
    }

    public class SaveRes : Response
    {
        public int Id { get; set; }
   
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