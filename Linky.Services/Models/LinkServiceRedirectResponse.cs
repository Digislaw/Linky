namespace Linky.Services.Models
{
    public class LinkServiceRedirectResponse
    {
        public bool Success { get; set; } = false;
        public string ErrorMessage { get; set; }
        public string URL { get; set; }
    }
}
