namespace LinkyMVC.Models.InputModels
{
    public class LinkUpdateModel
    {
        /// <summary>
        /// Link ID
        /// </summary>
        public int Id { get; set; }

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