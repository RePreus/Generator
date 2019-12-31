using System;

namespace Generator.Identity.Models.Settings
{
    public class SiteSettings
    {
        public Uri IndexRedirectTo { get; set; }

        public Uri Issuer { get; set; }
    }
}