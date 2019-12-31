using System;

namespace Generator.Identity.Models.Settings
{
    public class GoogleSettings
    {
        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string CallbackPath { get; set; }
    }
}