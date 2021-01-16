using System.ComponentModel.DataAnnotations;

namespace LinkyMVC.Models.InputModels
{
    public class LinkInputModel
    {
        /// <summary>
        /// Shortened link label
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// URL to redirect
        /// </summary>
        public string URL { get; set; }
    }
}