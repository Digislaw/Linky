using System.ComponentModel.DataAnnotations;

namespace LinkyMVC.Models.InputModels
{
    public class LinkUpdateModel
    {
        /// <summary>
        /// Link ID
        /// </summary>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Shortened link label
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Label { get; set; }

        /// <summary>
        /// URL to redirect
        /// </summary>
        [Required]
        [MaxLength(256)]
        public string URL { get; set; }
    }
}