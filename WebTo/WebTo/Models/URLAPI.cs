namespace WebTo.Models
{
    public class URLAPI
    {


        public string WarpPortalAPI { get; set; }
        public string PromptpayAPI { get; set; }
    }

    public enum RestApiType
    {
        Get,
        Post
    }
}
