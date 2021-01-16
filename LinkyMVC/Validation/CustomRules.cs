using System;

namespace LinkyMVC.Validation
{
    public static class CustomRules
    {
        public static bool ValidURL(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out _);
        }
    }
}