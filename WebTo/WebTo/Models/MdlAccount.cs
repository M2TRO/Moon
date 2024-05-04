using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebTo.Models
{
    public class MdlAccount
    {
        [Required(ErrorMessage = "Required Account!")]
        public string AccTel { get; set; } = null!;

        [Required(ErrorMessage = "Required Phonenumber!")]
        public string AccPwd { get; set; } = null!;
    }



    public class MdlRegister
    {
        [Required(ErrorMessage = "Required Account!")]
   
        public string AccName { get; set; } = null!;

        [Required(ErrorMessage = "Required Password!")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Required Password!")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        public string ConfirmPassword { get; set; } = null!;

        [Required(ErrorMessage = "Required AccEmail!")]
        [DataType(DataType.EmailAddress,ErrorMessage = "Not email format !")]
        [EmailAddress]
        public string AccEmail { get; set; } = null!;

        [Required(ErrorMessage = "Required Phonenumber!")]
        public string AccTel { get; set; } = null!;
    }
    public class MdlResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
    }

    public class SaveRes : MdlResponse
    {
        public int Id { get; set; }

    }


    public class LogsMsgsm
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Sender { get; set; }
        public string? Msg { get; set; }
        public decimal? Amout { get; set; }
        public string? Detail { get; set; }
        public DateTime? CreatedTime { get; set; }

        public DateTime? Lastupdate { get; set; }
        public bool? Active { get; set; }
    }
    public class ResCustInfo: MdlResponse
    {
        public TblAccount tblAccount { get; set; }
        public List<TransBank> transBanks { get; set; }

        public List<TransVer>  vers { get; set; }

        public List<MtBank> mtBanks { get; set; }   
        public List<LogEvent> logEvents { get; set; }


    }
    public  class TransVer
    {
        public int Id { get; set; }
        public int? LogId { get; set; }
        public string? OrderId { get; set; }
        public int? TransId { get; set; }
        public string? BankCode { get; set; }
        public string? Amout { get; set; }
        public string? AccountRef { get; set; }
        public bool? State { get; set; }
        public string? StateName { get; set; }
        public DateTime? CreatedTime { get; set; }
    }
    public partial class LogEvent
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Addr { get; set; }
        public string? Detail { get; set; }
        public string? Remark { get; set; }
        public DateTime? CreatedTime { get; set; }
    }
    public partial class MtBank
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? DescEn { get; set; }
        public string? DescTh { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
    public  class TblAccount
    {
        public int Id { get; set; }
        public string? AccRef { get; set; }
        public string? AccName { get; set; }
        public string? AccPwd { get; set; }
        public string? AccEmail { get; set; }
        public string AccTel { get; set; } = null!;
        public bool? IsActive { get; set; }
        public DateTime? ModifyTime { get; set; }
        public DateTime? CreatedTime { get; set; }
    }

    public  class TransBank
    {
        public int Id { get; set; }
        public string AccRef { get; set; }

        [Required(ErrorMessage = "Required PromptpayNo.!")]
        public string PromNo { get; set; }
        public int? BankId { get; set; }
        public string BankCode { get; set; }
        public string Token { get; set; }
        public int? TypeId { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
    public class MdlGetBank
    {
        public int? BankId { get; set; }
    }

    public class ResTransaction:MdlResponse
    {
        public List<Transaction> transactions { get; set; }
     
    }
    public  class Transaction
    {
        public int Id { get; set; }
        public int? TransBankId { get; set; }
        public int? TransTypeId { get; set; }
        public string? AccRef { get; set; }
        public decimal? Amout { get; set; }
        public bool? Verfify { get; set; }
        public DateTime? ModifyDate { get; set; }
        public DateTime? CeatedDate { get; set; }
    }
    public class MdlPayInput
    {
        public int? TransBankId { get; set; }
        //[Required(ErrorMessage = "Required AccountNo!")]
        public string AccountNo { get; set; }
        [Required(ErrorMessage = "Required Amount")]
        public double Amount { get; set; }
    }

    public class MdlGenVer
    {

        [Required(ErrorMessage = "Required AccInput")]
        public string OrderId { get; set; }

        //[Required(ErrorMessage = "Required BankCode")]
        //public string BankCode { get; set; }

        [Required(ErrorMessage = "Required Amount")]
        public string Amount { get; set; }

   
        public IFormFile File { get; set; }
    }

    public class SingleFileModel 
    {
        //     [Required(ErrorMessage = "Please enter file name")]
        //  public string FileName { get; set; }

        //   [Required(ErrorMessage = "Please select file")]
        public IFormFile File { get; set; }

        //public string AccInput { get; set; }
        //public string BankCode { get; set; }
        public string Amount { get; set; }
        public string OrderId { get; set; }
        

    }

    public class MdlSaveTran
    {

        public string? Amount { get; set; }
        public string? OrderId { get; set; }
        public string? Bankcode { get; set; }
        public string? LogId { get; set; }
        public bool state {  get; set; }


    }

    public class ResultUpload : MdlResponse
    {
        public string Ref { get; set; }
        public string Bank { get; set; }
        public string Amt { get; set; }
        public string Datetime { get; set; }
        public bool VerifyAI { get; set; }
        public bool Verify { get; set; }
    }
}
